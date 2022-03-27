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
        public Action RewardSuccessAction = null;
        public Action RewardFailAction = null;
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
            // ConfigManager.FetchCompleted += ApplyRemoteSettings;

            // Set the user’s unique ID:
            ConfigManager.SetCustomUserID("HoleSenearGooglePlay");

            // Set the environment ID:
            ConfigManager.SetEnvironmentID("02be422f-7b37-4595-b2d5-c8d0b6791682");

            // Fetch configuration setting from the remote service:
            ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
            AnalyticsEvent.GameStart(new Dictionary<string, object> { { "PS", "PS" } });
            // Invoke(nameof(ShowInterstitialAd), 3);
            this.OnInitializationComplete();
        }

        private string _adUnitId = "bannerAndroid";
#if UNITY_ADS // If Unity Ads is supported...
        private BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
#endif
        public void ShowInterstitialAd()
        {
            // Check if UnityAds ready before calling Show method:
            /*Debug.Log("Ready:"+Advertisement.IsReady());
            if (Advertisement.IsReady("video")) {
                // Advertisement.Show("video");
                Advertisement.Show("video");
                // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
            }
            else {
                Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
            }*/
#if UNITY_ADS // If Unity Ads is supported...
            Advertisement.Banner.SetPosition(_bannerPosition);
#endif
            LoadBanner();
        }

        public void LoadBanner()
        {
#if UNITY_ADS // If Unity Ads is supported...
            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = (() =>
                {
                    BannerOptions options1 = new BannerOptions
                    {
                        clickCallback = (() => { }),
                        hideCallback = (() => { }),
                        showCallback = (() => { })
                    };
                    Advertisement.Banner.Show(_adUnitId, options1);
                }),
                errorCallback = (message => { Debug.Log(message); })
            };
            // Load the Ad Unit with banner content:
            Advertisement.Banner.Load(_adUnitId, options);
#endif
        }

        void ApplyRemoteSettings(ConfigResponse configResponse)
        {
            // You will implement this in the final step.
            switch (configResponse.requestOrigin)
            {
                case ConfigOrigin.Default:
                    // Debug.Log ("No settings loaded this session; using default values.");
                    // Debug.Log(ConfigManager.appConfig);
                    NativeConnect.Connect.Init();
                    break;
                case ConfigOrigin.Cached:
                    // Debug.Log ("No settings loaded this session; using cached values from a previous session.");
                    NativeConnect.Connect.Init();
                    break;
                case ConfigOrigin.Remote:
                    /*this.isMagic = ConfigManager.appConfig.GetBool("IsMagic");
                    Debug.Log($"remote:{ConfigManager.appConfig.config.ToString()}");
                    if (this.isMagic)
                    {
                        if (Advertisement.isSupported)
                        {
                            Advertisement.Initialize("4221351", true);
                            this.OnInitializationComplete();
                        }
                    }
                    else
                    {
                        NativeConnect.Connect.Init();
                    }*/

                    // ShowInterstitialAd();
                    break;
            }

            // NativeConnect.Connect.showBanner();
        }

        public void OnInitializationComplete()
        {
            // Debug.Log("OnInitializationComplete");
            NativeConnect.Connect.Init();
        }

        public void RewardSuccess()
        {
            this.RewardSuccessAction?.Invoke();
            this.RewardSuccessAction = null;
        }

        public void RewardFail()
        {
            this.RewardFailAction?.Invoke();
            this.RewardFailAction = null;
        }
    }
}