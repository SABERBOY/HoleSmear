using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClone : MonoBehaviour
{
    public int n = 1;
    public float dist = 2;
    public bool rand = true;


    // Use this for initialization
    private void Start()
    {
        if (transform.childCount == 1)
            Clone(transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    private void Clone(GameObject source)
    {
        GameObject element;
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        for (var k = 0; k < n; k++)
        {
            if (i == 0 && j == 0 && k == 0)
                element = source;
            else
                element = Instantiate(source, transform);
            element.transform.localRotation = Quaternion.identity;
            element.transform.localPosition = new Vector3((i - (n - 1) * 0.5f) * dist, (k - (n - 1) * 0.5f) * dist,
                (j - (n - 1) * 0.5f) * dist);
            var sc = rand ? Random.Range(0.5f, 1.5f) : 1;
            element.transform.localScale = sc * Vector3.one;
            var mesh = element.GetComponent<MeshFilter>().mesh;
            var uvw = mesh.uv;
            for (var i1 = 0; i1 < uvw.Length; i1++) uvw[i1] *= sc;
            mesh.uv = uvw;
        }
    }
}