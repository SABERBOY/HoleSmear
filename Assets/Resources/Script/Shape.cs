using System.Collections.Generic;
using UnityEngine;

public class Shape : Base
{
    void Start()
    {
    }
    public static List<GameObject> shapeList = new List<GameObject>();
    public static List<GameObject> dieShapeList = new List<GameObject>();

    public static Shape CreateShape(Vector3 pos, Vector3 scale, Vector3 eul, string shapeStar, string colorID,
        string ID, string name)
    {
        var go = Instantiate(Resources.Load<GameObject>("Prefabs/" + shapeStar));
        go.transform.position = pos;
        go.transform.localScale = scale;
        go.transform.eulerAngles = eul;
        var gos = go.GetComponentsInChildren<Shape>();
        go.name = name;
        var shape = go.GetComponent<Shape>();
        for (var i = 0; i < gos.Length; i++)
        {
            gos[i].transform.parent = null;
            gos[i].gameObject.layer = 4;
            if (!colorID.Equals("-1")) gos[i].mr.material = Resources.Load<Material>("Material/" + colorID);
            if (ID.Equals("0"))
            {
                gos[i].tag = "Shape";
                shapeList.Add(gos[i].gameObject);
            }
            else if (ID.Equals("1"))
            {
                gos[i].tag = "DieShape";
                dieShapeList.Add(gos[i].gameObject);
            }

            gos[i].col.isTrigger = false;
            gos[i].rig.useGravity = true;
            gos[i].rig.isKinematic = true;
        }

        return shape;
    }
}