using UnityEditor;
using UnityEngine;

namespace BlackHoleGame.Script
{
    public class SaveToPNGFile
    {
        static void SaveRenderTexture(RenderTexture rt, string path)
        {
            RenderTexture.active = rt;
            Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            RenderTexture.active = null;
            var bytes = tex.EncodeToPNG();
            System.IO.File.WriteAllBytes(path, bytes);
            AssetDatabase.ImportAsset(path);
            // Debug.Log($"Saved texture: {rt.width}x{rt.height} - " + path);
        }

        [MenuItem("Assets/Take Screenshot", true)]
        public static bool TakeScreenshotValidation() =>
            Selection.activeGameObject && Selection.activeGameObject.GetComponent<Camera>();

        [MenuItem("Assets/Take Screenshot")]
        public static void TakeScreenshot()
        {
            var camera = Selection.activeGameObject.GetComponent<Camera>();
            var index = camera.transform.parent.GetChild(1).GetChild(0).name;
            var prev = camera.targetTexture;
            var rt = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
            camera.targetTexture = rt;
            camera.Render();
            SaveRenderTexture(rt, Application.dataPath + $"/../{index}.png");
            camera.targetTexture = prev;
            Object.DestroyImmediate(rt);
        }
    }
}