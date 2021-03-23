using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Base
{
    Vector3[] poss = new Vector3[] { new Vector3(2, 0, 40), new Vector3(-2, 0, 40) };
    int[] nums = new int[] { 1, 1002 };
    void Start()
    {
        StartCoroutine("IECreate");
    }
    IEnumerator IECreate()
    {
        while (true)
        {
            int a = Random.Range(0, 2);
            int b = Random.Range(0, 2);
            Thing.CreateThing(nums[a], poss[b]);
            yield return new WaitForSeconds(2f);
        }
    }
    void Attract()
    {
        Collider[] cols;
        Rigidbody rig;
        cols = Physics.OverlapCapsule(transform.position + Vector3.down * 10, transform.position + Vector3.up * 10, SceneData.holeSize * 1.05f, LayerMask.GetMask("Water"));
        foreach (Collider item in cols)
        {
            rig = item.GetComponent<Rigidbody>();
            if (rig != null)
            {
                //item.isTrigger = false;
                //rig.useGravity = true;
                //rig.isKinematic = false;
                rig.AddForce(new Vector3(0, -100, 0));
            }
        }
    }
    public Mesh mesh;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireMesh(mesh, transform.position, new Quaternion(0, 0, 0, 1), new Vector3(4.4f, 10, 4.4f));
    }
    void Update()
    {
        Attract();
        if (Input.GetMouseButtonDown(0))
        {
            pos = new Vector3(-pos.x, pos.y, pos.z);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
    }
}
