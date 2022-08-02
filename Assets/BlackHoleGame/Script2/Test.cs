using System.Collections;
using System.Collections.Generic;
using BlackHoleGame.Script;
using UnityEngine;

public class Test : Base
{
    private Vector3[] poss = new Vector3[] { new Vector3(2, 0, 40), new Vector3(-2, 0, 40) };
    private int[] nums = new int[] { 1, 1002 };

    private void Start()
    {
        StartCoroutine("IECreate");
    }

    private IEnumerator IECreate()
    {
        while (true)
        {
            var a = Random.Range(0, 2);
            var b = Random.Range(0, 2);
            Thing.CreateThing(nums[a], poss[b]);
            yield return new WaitForSeconds(2f);
        }
    }

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
                //item.isTrigger = false;
                //rig.useGravity = true;
                //rig.isKinematic = false;
                rig.AddForce(new Vector3(0, -100, 0));
        }
    }

    public Mesh mesh;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireMesh(mesh, transform.position, new Quaternion(0, 0, 0, 1), new Vector3(4.4f, 10, 4.4f));
    }

    private void Update()
    {
        Attract();
        if (Input.GetMouseButtonDown(0)) pos = new Vector3(-pos.x, pos.y, pos.z);
    }

    private void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
    }
}