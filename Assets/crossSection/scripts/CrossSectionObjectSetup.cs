using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CrossSectionObjectSetup : MonoBehaviour
{
    public Color sectionColor = Color.red;

    private List<Material> matList;
    private List<Material> clipMatList;
    private Renderer[] renderers;
    private Dictionary<Renderer, int[]> matDict;

    public bool accurateBounds = true;

    [HideInInspector] public Bounds bounds;


    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        makeSectionMaterials();

        var pxyz = (Planar_xyzClippingSection)FindObjectOfType(typeof(Planar_xyzClippingSection));
        // this can freeze the app - in case of high poly meshes - for a moment we need it for pxyz object only
        if (pxyz) calculateBounds();
    }

    private void Start()
    {
    }


    private void makeSectionMaterials()
    {
        matList = new List<Material>();
        clipMatList = new List<Material>();
        matDict = new Dictionary<Renderer, int[]>();
        foreach (var rend in renderers)
        {
            var mats = rend.sharedMaterials;
            var idx = new int[mats.Length];
            for (var j = 0; j < mats.Length; j++)
            {
                var i = matList.IndexOf(mats[j]);
                if (i == -1)
                {
                    matList.Add(mats[j]);
                    i = matList.Count - 1;
                }

                idx[j] = i;
            }

            matDict.Add(rend, idx);
        }

        foreach (var mat in matList)
        {
            var shaderName = mat.shader.name;
            Debug.Log(shaderName);
            if (shaderName.Length > 13)
                if (shaderName.Substring(0, 13) == "CrossSection/")
                {
                    clipMatList.Add(mat);
                    continue;
                }

            var substitute = new Material(mat);
            //substitute.name = "subst_" + substitute.name;
            shaderName = shaderName.Replace("Legacy Shaders/", "").Replace("(", "").Replace(")", "");
            Shader replacementShader = null;

            if (replacementShader == null) replacementShader = Shader.Find("CrossSection/" + shaderName);
            if (replacementShader == null)
            {
                if (shaderName.Contains("Transparent/VertexLit"))
                    replacementShader = Shader.Find("CrossSection/Transparent/Specular");
                else if (shaderName.Contains("Transparent"))
                    replacementShader = Shader.Find("CrossSection/Transparent/Diffuse");
                else
                    replacementShader = Shader.Find("CrossSection/Diffuse");
            }

            substitute.shader = replacementShader;
            substitute.SetColor("_SectionColor", sectionColor);

            clipMatList.Add(substitute);
        }

        foreach (var rend in renderers)
        {
            var idx = matDict[rend];
            var mats = new Material[idx.Length];
            for (var i = 0; i < idx.Length; i++) mats[i] = clipMatList[idx[i]];
            rend.materials = mats;
        }
    }


    private void calculateBounds()
    {
        if (accurateBounds)
        {
            bounds = calculateMeshBounds();
        }
        else
        {
            bounds = renderers[0].bounds;

            for (var i = 1; i < renderers.Length; i++)
                bounds.Encapsulate(renderers[i].bounds);
            /*          This gives the accurate results only when the objects are not rotated or rotated by multiplication of 90 degrees.
                            A general way to get accurate results would be to iterate through all the mesh points, and find their positions range in the world space.
                            But this can take long in case of complex meshes*/
        }
    }

    private Bounds calculateMeshBounds()
    {
        var accurateBounds = new Bounds();
        var meshes = GetComponentsInChildren<MeshFilter>();
        for (var i = 0; i < meshes.Length; i++)
        {
            var ms = meshes[i].mesh;
            var vc = ms.vertexCount;
            for (var j = 0; j < vc; j++)
                if (i == 0 && j == 0)
                    accurateBounds = new Bounds(meshes[i].transform.TransformPoint(ms.vertices[j]), Vector3.zero);
                else
                    accurateBounds.Encapsulate(meshes[i].transform.TransformPoint(ms.vertices[j]));
        }

        return accurateBounds;
    }

    private void OnApplicationQuit()
    {
        Shader.DisableKeyword("CLIP_PLANE");
    }

    public Bounds GetBounds()
    {
        return bounds;
    }
}