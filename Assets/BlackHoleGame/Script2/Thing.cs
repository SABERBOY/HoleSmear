using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : Base
{
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        pos += Vector3.back * Time.deltaTime * 40;
    }

    public static Thing CreateThing(int num, Vector3 pos)
    {
        var go = Instantiate(Resources.Load<GameObject>("Prefabs/" + num));
        var thing = go.AddComponent<Thing>();
        thing.pos = pos;
        thing.col.isTrigger = false;
        thing.rig.useGravity = true;
        return thing;
    }
}