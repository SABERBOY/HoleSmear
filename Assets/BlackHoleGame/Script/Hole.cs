using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class Hole : Base
{
    public static Hole instance;
    private Coroutine attract;
    public ParticleSystem lizi;
    public Mesh mesh;
    private Transform plane;
    private Vector3 startPos2;
    private Vector3 startPos3;

    private void Awake()
    {
        instance = this;
        // plane = GameObject.Find("Plane").transform;
        enabled = false;
    }

    private void OnEnable()
    {
        pos = SceneData.holeStartPos;
        StartSet();
    }

    private void Start()
    {
        // NativeConnect.Connect.showBanner();
        //StartCoroutine("IEShockTime");
    }

    /// <summary>
    ///     游戏开始设置
    /// </summary>
    private void StartSet()
    {
        lizi.gameObject.SetActive(true);
        lizi.Stop();
        if (attract == null) attract = StartCoroutine("IEAttract");
        var a = PlayerPrefs.GetString(SceneData.level);
        if (string.IsNullOrEmpty(a))
        {
            DataController.sceneNum = 0;
        }
        else
        {
            var b = int.Parse(a);
            DataController.sceneNum = b - b % 3;
            UI.lvTextLeft.text = (DataController.sceneNum / 3 + 1).ToString();
            UI.lvTextRight.text = (DataController.sceneNum / 3 + 2).ToString();
        }

        //StartMove();
        UI.lvPanel.SetActive(true);
    }

    /// <summary>
    ///     移动协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEMove()
    {
        while (true)
        {
            Move();
            LiZiMove();
            gameCon.Win();
            yield return new WaitForSeconds(0.01f);
        }
    }

    /// <summary>
    ///     吸引协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEAttract()
    {
        while (true)
        {
            Attract();
            yield return new WaitForSeconds(0.01f);
        }
    }

    /// <summary>
    ///     开始移动协程
    /// </summary>
    public void StartMove()
    {
        StartCoroutine("IEMove");
    }

    /// <summary>
    ///     停止移动协程
    /// </summary>
    public void StopMove()
    {
        StopCoroutine("IEMove");
    }

    /// <summary>
    ///     重置坐标
    /// </summary>
    public void ResetPos()
    {
        HoleShader.startPos = SceneData.holeStartPos;
        pos = SceneData.holeStartPos;
    }

    /// <summary>
    ///     黑洞吸引
    /// </summary>
    private void Attract()
    {
        Collider[] cols;
        Rigidbody rig;
        cols = Physics.OverlapCapsule(transform.position + Vector3.down * 10, transform.position + Vector3.up * 10,
            SceneData.holeSize * 1.05f, LayerMask.GetMask("Water"));
        foreach (var item in cols)
        {
            rig = item.GetComponent<Rigidbody>();
            if (rig != null)
            {
                item.isTrigger = false;
                rig.useGravity = true;
                rig.isKinematic = false;
                rig.AddForce(new Vector3(transform.position.x - item.transform.position.x, -100,
                    transform.position.z - item.transform.position.z));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireMesh(mesh, transform.position, new Quaternion(0, 0, 0, 1), new Vector3(2.625f, 10, 2.625f));
    }

    private void LiZiMove()
    {
        var liziPos = lizi.transform.position;
        lizi.transform.position = new Vector3(liziPos.x, pos.z / 8 - 3, liziPos.z);
    }

    private void Move()
    {
#if UNITY_EDITOR
        var movePos3 = Vector3.zero;
        if (Input.GetMouseButton(0) && startPos3 != Vector3.zero)
            movePos3 = Input.mousePosition - startPos3;
        startPos3 = Input.mousePosition;
        var x_WillMoveTo1 = pos.x + movePos3.x * SceneData.holeMoveSpeed;
        var z_WillMoveTo1 = pos.z - plane.position.z + movePos3.y * SceneData.holeMoveSpeed;
        if (x_WillMoveTo1 < plane.lossyScale.x * 0.5 - SceneData.holeSize &&
            x_WillMoveTo1 > -plane.lossyScale.x * 0.5 + SceneData.holeSize)
            pos += Vector3.right * movePos3.x * SceneData.holeMoveSpeed;
        if (z_WillMoveTo1 < plane.lossyScale.y * 0.5 - SceneData.holeSize &&
            z_WillMoveTo1 > -plane.lossyScale.y * 0.5 + SceneData.holeSize)
            pos += Vector3.forward * movePos3.y * SceneData.holeMoveSpeed;
#endif
#if UNITY_ANDROID
        Vector2 move = Vector3.zero;
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved) move = Input.GetTouch(0).deltaPosition;
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
            }
        }

        var x_WillMoveTo2 = pos.x + move.x * SceneData.holeMoveSpeed;
        var z_WillMoveTo2 = pos.z - plane.position.z + move.y * SceneData.holeMoveSpeed;
        if (x_WillMoveTo2 < plane.lossyScale.x * 0.5 - SceneData.holeSize &&
            x_WillMoveTo2 > -plane.lossyScale.x * 0.5 + SceneData.holeSize)
            pos += Vector3.right * move.x * SceneData.holeMoveSpeed;
        if (z_WillMoveTo2 < plane.lossyScale.y * 0.5 - SceneData.holeSize &&
            z_WillMoveTo2 > -plane.lossyScale.y * 0.5 + SceneData.holeSize)
            pos += Vector3.forward * move.y * SceneData.holeMoveSpeed;
#endif
    }

    private void OnTriggerEnter(Collider col)
    {
        var name = col.name;
        if (col.tag.Equals("DieShape") && gameCon.isWin == false && gameCon.isDie == false) gameCon.isDie = true;
        if (col.tag.Equals("Shape")) Shape.shapeList.Remove(col.gameObject);
        var color = col.GetComponent<MeshRenderer>().material;
        lizi.GetComponent<ParticleSystemRenderer>().material = color;
        lizi.Play();
        if (col.transform.parent != null && col.name.Contains("Diamond"))
            Destroy(col.transform.parent.gameObject);
        else
            Destroy(col.gameObject);
        if (UI.shockSwitch.isOn && !gameCon.isWin) NativeConnect.Connect.Shock();
    }

    /// <summary>
    /// set plane transform
    /// </summary>
    /// <param name="_plane"></param>
    public void SetPlaner([NotNull] Transform _plane)
    {
        this.plane = _plane ? _plane : throw new ArgumentNullException(nameof(_plane));
    }
}