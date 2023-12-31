﻿//The purpose of this script is to scale the capped section prefab to fit the model GameObject 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CappedSectionFitter : MonoBehaviour
{
    public GameObject model;

    private enum Mode
    {
        box,
        corner
    };

    private Mode sectionMode;

    // Use this for initialization
    private void Start()
    {
        if (gameObject.GetComponent<CappedSectionBox>()) sectionMode = Mode.box;
        if (gameObject.GetComponent<CappedSectionCorner>()) sectionMode = Mode.corner;
        if (model)
        {
            transform.rotation = model.transform.rotation;

            var bounds = GetBounds(model);
            Debug.Log(bounds.ToString());

            var scale = 1f;
            if (sectionMode == Mode.box) scale = 1.0f;
            if (sectionMode == Mode.corner) scale = 0.5f;

            var clearance = 0.01f * Vector3.one;

            transform.localScale = Vector3.one;

            transform.localScale = scale * bounds.size + clearance;

            transform.position = bounds.center;
        }
    }

    private Bounds GetBounds(GameObject go)
    {
        var allRenderers = go.GetComponentsInChildren<Renderer>();
        var quat = go.transform.rotation; //object axis AABB
        if (allRenderers[0].isPartOfStaticBatch) quat = Quaternion.Euler(0f, 0f, 0f); //world axis


        var bounds = new Bounds();
        if (allRenderers[0].isPartOfStaticBatch)
        {
            bounds = allRenderers[0].bounds;
            for (var i = 1; i < allRenderers.Length; i++) bounds.Encapsulate(allRenderers[i].bounds);
            return bounds;
        }

        go.transform.rotation = Quaternion.identity;
        var meshes = go.GetComponentsInChildren<MeshFilter>();
        for (var i = 0; i < meshes.Length; i++)
        {
            var ms = meshes[i].sharedMesh;
            var vc = ms.vertexCount;
            for (var j = 0; j < vc; j++)
                if (i == 0 && j == 0)
                    bounds = new Bounds(meshes[i].transform.TransformPoint(ms.vertices[j]), Vector3.zero);
                else
                    bounds.Encapsulate(meshes[i].transform.TransformPoint(ms.vertices[j]));
        }

        var localCentre = go.transform.InverseTransformPoint(bounds.center);
        go.transform.rotation = quat;
        bounds.center = go.transform.TransformPoint(localCentre);
        return bounds;
    }
}