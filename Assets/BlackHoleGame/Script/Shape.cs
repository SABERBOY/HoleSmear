using System.Collections.Generic;
using UnityEngine;

namespace BlackHoleGame.Script
{
    public class Shape : Base
    {
        private void Start()
        {
        }

        public static List<GameObject> shapeList = new List<GameObject>();
        public static List<GameObject> dieShapeList = new List<GameObject>();

        public static Shape CreateShape(Vector3 pos, Vector3 scale, Vector3 eul, string shapeStar, string colorID,
            string ID, string name)
        {
            var go = Instantiate(Resources.Load<GameObject>("Prefabs/" + shapeStar), GameController.instance.GameMain);
            go.transform.position = pos;
            go.transform.localScale = scale;
            go.transform.eulerAngles = eul;
            var gos = go.GetComponentsInChildren<Shape>();
            go.name = name;
            var shape = go.GetComponent<Shape>();
            foreach (var t in gos)
            {
                t.transform.parent = GameController.instance.GameMain;
                t.gameObject.layer = 4;
                if (!colorID.Equals("-1")) t.mr.material = Resources.Load<Material>("Material/" + colorID);
                if (ID.Equals("0"))
                {
                    t.tag = "Shape";
                    shapeList.Add(t.gameObject);
                }
                else if (ID.Equals("1"))
                {
                    t.tag = "DieShape";
                    dieShapeList.Add(t.gameObject);
                }

                t.col.isTrigger = false;
                t.rig.useGravity = true;
                t.rig.isKinematic = true;
            }

            return shape;
        }
    }
}