using UnityEngine;
using System.Collections;

public class EnableShadersKeyword : MonoBehaviour
{
    public string kwd = "CLIP_PLANE";

    // Use this for initialization
    private void Start()
    {
        var mats = GetComponent<Renderer>().materials;
        foreach (var m in mats) m.EnableKeyword(kwd);
    }
}