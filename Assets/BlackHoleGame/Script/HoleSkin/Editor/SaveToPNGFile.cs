using System.IO;
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

        public static void SpriteToPNG(Sprite sprite, string path)
        {
            var tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.ARGB32, false);
            var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                (int)sprite.textureRect.y,
                (int)sprite.textureRect.width,
                (int)sprite.textureRect.height);
            tex.SetPixels(pixels);
            tex.Apply();
            var bytes = tex.EncodeToPNG();
            System.IO.File.WriteAllBytes(path, bytes);
            AssetDatabase.ImportAsset(path);
            Debug.Log($"Saved texture: {tex.width}x{tex.height} - " + path);
        }

        // select sprite and save to png

        // [MenuItem("Assets/PS")]
        public static void SpriteToPNG()
        {
            var sprite = (Selection.activeObject as GameObject)?.GetComponent<SpriteRenderer>().sprite;
            if (sprite != null)
            {
                var path = EditorUtility.SaveFilePanel("Save Sprite to PNG", "", sprite.name, "png");
                SpriteToPNG(sprite, path);
            }
        }

        public static string GetSelectedPathOrFallback()
        {
            string path = "Assets";

            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }

            return path;
        }
    }
}