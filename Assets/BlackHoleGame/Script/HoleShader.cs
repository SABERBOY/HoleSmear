﻿using System.Collections;
using UnityEngine;

namespace BlackHoleGame.Script
{
    public class HoleShader : Base
    {
        private static HoleShader _instance;
        public static Vector3 startPos = SceneData.holeStartPos;
        public Vector3[] AxisDir;
        private GameObject go;

        public Vector3[] hitPoints;
        private int i;

        private readonly int n = 1;
        public float[] radiuses;
        private static readonly int HitCount = Shader.PropertyToID("_hitCount");
        private static readonly int SectionColor = Shader.PropertyToID("_SectionColor");

        public static HoleShader instance => GameController.instance.GameMap.GetComponentInChildren<HoleShader>();

        private void Awake()
        {
            //instance = this;
            mr.material.SetColor(SectionColor, mr.material.color * 0.9f);
        }

        private void Start()
        {
            hitPoints = new Vector3[n];
            AxisDir = new Vector3[n];
            radiuses = new float[n];
            Shader.SetGlobalInt(HitCount, 0);
            //Renderer[] allrenderers = gameObject.GetComponentsInChildren<Renderer>();
            //foreach (Renderer r in allrenderers)
            //{
            //	//Material[] mats = r.sharedMaterials;
            //	//foreach (Material m in mats) if (m.shader.name.Substring(0, 13) == "CrossSection/") m.DisableKeyword("CLIP_PLANE");
            //}
            //StartCoroutine("IEShaderMove");
        }

        public void StartShader()
        {
            StartCoroutine(nameof(IEShaderMove));
        }

        public void StopShader()
        {
            StopCoroutine(nameof(IEShaderMove));
            Shader.DisableKeyword("CLIP_TUBES");
        }

        private IEnumerator IEShaderMove()
        {
            while (true)
            {
                Shader.EnableKeyword("CLIP_TUBES");
                //if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
                startPos = Hole.instance.pos;
                hitPoints[i % n] = startPos;
                AxisDir[i % n] = Vector3.down;
                radiuses[i % n] = SceneData.holeSize;
                if (go == null)
                {
                    go = Hole.instance.gameObject;
                    go.transform.position = startPos;
                }

                //go.transform.localScale = new Vector3(SceneData.holeSize / 2, SceneData.holeSize / 2, 1);
                Shader.SetGlobalVector("_hitPoint" + i % n, hitPoints[i % n]);
                Shader.SetGlobalVector("_AxisDir" + i % n, AxisDir[i % n]);
                Shader.SetGlobalFloat("_Rad" + i % n, radiuses[i % n]);
                i++;
                Shader.SetGlobalInt(HitCount, Mathf.Min(i, n));
                yield return new WaitForSeconds(0.01f);
            }
        }

        private void Update()
        {
        }

        private void OnApplicationQuit()
        {
            Shader.DisableKeyword("CLIP_TUBES");
        }
    }
}