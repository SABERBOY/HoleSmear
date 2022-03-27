using System;
using System.IO;
using UnityEditor.Android;
using UnityEngine;

namespace Transsion.Editor
{
    public class AndroidPostBuildProcessor : IPostGenerateGradleAndroidProject
    {
        private readonly string configPath = "Assets/Transsion/Resources/";

        public int callbackOrder
        {
            get { return 999; }
        }

        void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path)
        {
#if !TRANSSIONAD
            return;
#endif
            Debug.Log("Bulid path : " + path);

            path = path.Replace("unityLibrary", "launcher");
            var googleConfig = path + "/google-services.json";
            File.Delete(googleConfig);
            File.Copy($"{configPath}google-services.json", googleConfig);

            String directoryPath = path + "/src/main/assets";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var transsionConfig = directoryPath + "/game_sdk_config.json";
            File.Delete(transsionConfig);
            File.Copy($"{configPath}game_sdk_config.json", transsionConfig);
            var transsionGameConfig = directoryPath + "/game_config.json";
            File.Delete(transsionGameConfig);
            File.Copy($"{configPath}game_config.json", transsionGameConfig);
        }
    }
}