using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToColor : MonoBehaviour
{
    private Material m;

    public Color emissionColor;

    // Use this for initialization
    private void Start()
    {
        m = GetComponent<Renderer>().material;
    }

    public void SetColor(float sc)
    {
        var a = Mathf.Clamp01(-2.5f * sc + 1.25f);
        m.SetColor("_EmissionColor", a * emissionColor);
    }
}