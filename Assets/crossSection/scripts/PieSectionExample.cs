using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PieSectionExample : MonoBehaviour
{
    //private Material sMat;
    private Vector3 normal1;
    private Vector3 normal2;

    public float angle = 60f;

    // Use this for initialization
    private void Start()
    {
        Shader.DisableKeyword("CLIP_PLANE");
        Shader.EnableKeyword("CLIP_TWO_PLANES");
        //we have declared: "material.EnableKeyword("CLIP_PLANE");" on all the crossSectionStandard derived materials - in the CrossSectionStdShaderGUI editor script - so we have to switch it off
        var allrenderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var r in allrenderers)
        {
            var mats = r.sharedMaterials;
            foreach (var m in mats)
                if (m.shader.name.Substring(0, 13) == "CrossSection/")
                    m.DisableKeyword("CLIP_PLANE");
        }

        normal1 = transform.right;
        normal2 = Quaternion.AngleAxis(angle, transform.up) * normal1;
        Shader.SetGlobalVector("_SectionPoint", transform.position);
        Shader.SetGlobalVector("_SectionPlane", transform.right);
        Shader.SetGlobalVector("_SectionPlane2", normal2);
    }

    // Update is called once per frame
    private void Update()
    {
        angle = (angle + 0.5f) % 360f;
        normal2 = Quaternion.AngleAxis(angle, transform.up) * transform.right;
        Shader.SetGlobalVector("_SectionPlane2", normal2);

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            var coll = gameObject.GetComponent<Collider>();
            if (coll.Raycast(ray, out hit, 10000f)) StartCoroutine(drag());
        }
    }

    private void OnEnable()
    {
        Shader.EnableKeyword("CLIP_TWO_PLANES");
        //Shader.EnableKeyword("CLIP_PLANE");
    }

    private void OnDisable()
    {
        Shader.DisableKeyword("CLIP_TWO_PLANES");
        //Shader.DisableKeyword("CLIP_PLANE");
    }

    private void OnApplicationQuit()
    {
        //disable clipping so we could see the materials and objects in editor properly
        Shader.DisableKeyword("CLIP_TWO_PLANES");
        Shader.DisableKeyword("CLIP_PLANE");
    }


    private IEnumerator drag()
    {
        var cameraDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        var startPoint =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        var startNormal = normal1;
        var translation = Vector3.zero;
        Camera.main.GetComponent<maxCamera>().enabled = false;
        while (Input.GetMouseButton(0))
        {
            translation =
                Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance)) - startPoint;
            normal1 = Quaternion.AngleAxis(50f * Vector3.Dot(translation, transform.up), transform.up) * startNormal;
            Shader.SetGlobalVector("_SectionPlane", normal1);
            yield return null;
        }

        Camera.main.GetComponent<maxCamera>().enabled = true;
    }
}