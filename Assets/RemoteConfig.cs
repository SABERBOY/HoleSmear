using System;
using Unity.RemoteConfig;
using UnityEngine;

namespace DefaultNamespace
{
    public class RemoteConfig : MonoBehaviour
    {
        public struct userAttributes {
            // Optionally declare variables for any custom user attributes:
            public bool expansionFlag;
        }

        public struct appAttributes {
            // Optionally declare variables for any custom app attributes:
            public int level;
            public int score;
            public string appVersion;
        }
        private void Awake()
        {
            // Add a listener to apply settings when successfully retrieved:
            ConfigManager.FetchCompleted += ApplyRemoteSettings;

            // Set the user’s unique ID:
            // ConfigManager.SetCustomUserID("HoleSmear");

            // Set the environment ID:
            // ConfigManager.SetEnvironmentID("54059310-2308-4793-9b96-be3556b70153");

            // Fetch configuration setting from the remote service:
            ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        }

        void ApplyRemoteSettings(ConfigResponse configResponse)
        {
            // You will implement this in the final step.
            switch (configResponse.requestOrigin) {
                case ConfigOrigin.Default:
                    // Debug.Log ("No settings loaded this session; using default values.");
                    // Debug.Log(ConfigManager.appConfig);
                    break;
                case ConfigOrigin.Cached:
                    // Debug.Log ("No settings loaded this session; using cached values from a previous session.");
                    break;
                case ConfigOrigin.Remote:
                    // Debug.Log ("New settings loaded this session; update values accordingly.");
                    // Debug.Log(ConfigManager.appConfig.GetBool ("IsMagic"));
                    // enemyVolume = ConfigManager.appConfig.GetInt ("enemyVolume");
                    // enemyHealth = ConfigManager.appConfig.GetInt ("enemyHealth");
                    // enemyDamage = ConfigManager.appConfig.GetFloat ("enemyDamage");
                    // assignmentId = ConfigManager.appConfig.assignmentID;
                    break;
            }
        }
    }
}