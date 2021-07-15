using System;
using System.Collections.Generic;
using Unity.RemoteConfig;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;

namespace DefaultNamespace
{
    public class RemoteConfig : MonoBehaviour
    {
        private static RemoteConfig _instance = null;
        [NonSerialized] private bool isMagic = false;

        public bool IsMagic => isMagic;

        public static RemoteConfig Instance => _instance;

        public struct userAttributes
        {
            // Optionally declare variables for any custom user attributes:
            public bool expansionFlag;
        }

        public struct appAttributes
        {
            // Optionally declare variables for any custom app attributes:
            public int level;
            public int score;
            public string appVersion;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
            // Add a listener to apply settings when successfully retrieved:
            ConfigManager.FetchCompleted += ApplyRemoteSettings;

            // Set the user’s unique ID:
            ConfigManager.SetCustomUserID("HoleSmear");

            // Set the environment ID:
            ConfigManager.SetEnvironmentID("54059310-2308-4793-9b96-be3556b70153");

            // Fetch configuration setting from the remote service:
            ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
            AnalyticsEvent.GameStart(new Dictionary<string, object> {{"PS", "PS"}});
            if (Advertisement.isSupported)
            {
                Advertisement.Initialize("3199470", true);
            }
            Invoke(nameof(ShowInterstitialAd),3);
        }

        public void ShowInterstitialAd() {
            // Check if UnityAds ready before calling Show method:
            Debug.Log("Ready:"+Advertisement.IsReady());
            if (Advertisement.IsReady("video")) {
                // Advertisement.Show("video");
                Advertisement.Show("video");
                // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
            }
            else {
                Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
            }
        }

        void ApplyRemoteSettings(ConfigResponse configResponse)
        {
            // You will implement this in the final step.
            switch (configResponse.requestOrigin)
            {
                case ConfigOrigin.Default:
                    // Debug.Log ("No settings loaded this session; using default values.");
                    // Debug.Log(ConfigManager.appConfig);
                    break;
                case ConfigOrigin.Cached:
                    // Debug.Log ("No settings loaded this session; using cached values from a previous session.");
                    break;
                case ConfigOrigin.Remote:
                    this.isMagic = ConfigManager.appConfig.GetBool("IsMagic");
                    // Debug.Log ("New settings loaded this session; update values accordingly.");
                    Debug.Log("isMagic:" + this.isMagic);
                    // enemyVolume = ConfigManager.appConfig.GetInt ("enemyVolume");
                    // enemyHealth = ConfigManager.appConfig.GetInt ("enemyHealth");
                    // enemyDamage = ConfigManager.appConfig.GetFloat ("enemyDamage");
                    // assignmentId = ConfigManager.appConfig.assignmentID;
                    break;
            }
        }
    }
}