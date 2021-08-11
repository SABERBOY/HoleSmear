using System;
using DefaultNamespace;
using Script.SDK;
using UnityEngine;

namespace SDK
{
    public class TranssionSDK : ISDK
    {
        private static string unityPlayerClassName = "com.unity3d.player.UnityPlayer";
        private static string unityAdPlayerActivityClassName = "com.unity3d.player.UnityAdPlayerActivity";
        private AndroidJavaObject unityPlayerActivity = null;
        private AndroidJavaObject unityAdPlayerActivity = null;


        public TranssionSDK()
        {
            AndroidJavaClass clsUnityPlayer = new AndroidJavaClass(unityPlayerClassName);
            AndroidJavaObject objActivity = clsUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            unityPlayerActivity = objActivity;
            AndroidJavaObject adUnityPlayer = new AndroidJavaObject(unityAdPlayerActivityClassName);
            Debug.Log(" :" + adUnityPlayer);
            adUnityPlayer.Call("Init", unityPlayerActivity);
            unityAdPlayerActivity = adUnityPlayer;
            this.Init();
        }

        public void Init()
        {
            if (this.unityAdPlayerActivity != null)
            {
                this.unityAdPlayerActivity.Call("LoadInterstitial");
                this.unityAdPlayerActivity.Call("LoadReward");
                this.ShowBanner(true);
                Debug.Log(this.unityAdPlayerActivity.Call<bool>("isRewardLoaded"));
                Debug.Log(this.unityAdPlayerActivity.Call<bool>("isInterstitialLoaded"));
            }
        }

        public bool VideoLoaded()
        {
            return this.unityAdPlayerActivity != null && this.unityAdPlayerActivity.Call<bool>("isRewardLoaded");
        }

        public bool InterstitialLoaded()
        {
            return this.unityAdPlayerActivity != null && this.unityAdPlayerActivity.Call<bool>("isInterstitialLoaded");
        }

        public void ShowVideo(Action success, Action fail)
        {
            if (this.unityAdPlayerActivity != null)
            {
                this.unityAdPlayerActivity.Call("ShowReward");
                RemoteConfig.Instance.RewardFailAction = fail;
                RemoteConfig.Instance.RewardSuccessAction = success;
            }
        }

        public void ShowInterstitialAd(Action interactionAdCompleted, Action hold)
        {
            if (this.unityAdPlayerActivity != null)
            {
                this.unityAdPlayerActivity.Call("ShowInterstitial");
            }
        }

        public void OnApplicationPause(bool pause)
        {
        }

        public void ShowBanner(bool show)
        {
            if (this.unityAdPlayerActivity != null)
            {
                this.unityAdPlayerActivity.Call("ShowBanner");
            }
        }

        public void ShowFloatingWindow(bool show)
        {
            this.unityAdPlayerActivity.Call("ShowBanner",show);
        }
    }
}