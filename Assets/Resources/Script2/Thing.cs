using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : Base
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos += Vector3.back * Time.deltaTime*40;
    }

    public static Thing CreateThing(int num,Vector3 pos)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/"+num));
        Thing thing = go.AddComponent<Thing>();
        thing.pos = pos;
        thing.col.isTrigger = false;
        thing.rig.useGravity = true;
        return thing;
    }
}
