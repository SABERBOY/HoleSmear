using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlaneSection : MonoBehaviour
{
    private void Start()
    {
        Shader.EnableKeyword("CLIP_PLANE");
    }


    private void OnEnable()
    {
        Shader.EnableKeyword("CLIP_PLANE");
        //Shader.EnableKeyword("CLIP_PLANE");
    }

    private void OnDisable()
    {
        Shader.DisableKeyword("CLIP_PLANE");
        //Shader.DisableKeyword("CLIP_PLANE");
    }

    private void OnApplicationQuit()
    {
        //disable clipping so we could see the materials and objects in editor properly
        Shader.DisableKeyword("CLIP_PLANE");
    }
}