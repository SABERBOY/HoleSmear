using System;
using DefaultNamespace;
using Script.SDK;
using Transsion.UtilitiesCrowd;
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
#if UNITY_ANDROID&& !UNITY_EDITOR
            // AndroidJavaClass clsUnityPlayer = new AndroidJavaClass(unityPlayerClassName);
            // AndroidJavaObject objActivity = clsUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            // unityPlayerActivity = objActivity;
            // AndroidJavaObject adUnityPlayer = new AndroidJavaObject(unityAdPlayerActivityClassName);
            // Debug.Log(" :" + adUnityPlayer);
            // adUnityPlayer.Call("Init", unityPlayerActivity);
            // unityAdPlayerActivity = adUnityPlayer;
#endif
            this.Init();
        }

        public void Init()
        {
            Debug.Log($"{nameof(TranssionSDK)} Init");
            TranssionAdMain.Init();
            this.ShowBanner(true);
#if UNITY_EDITOR
#else
            // if (this.unityAdPlayerActivity != null)
            // {
            //     this.unityAdPlayerActivity.Call("LoadInterstitial");
            //     this.unityAdPlayerActivity.Call("LoadReward");
            //     this.unityAdPlayerActivity.Call("LoadFloat");
            //     this.ShowBanner(true);
            //     // Debug.Log(this.unityAdPlayerActivity.Call<bool>("isRewardLoaded"));
            //     // Debug.Log(this.unityAdPlayerActivity.Call<bool>("isInterstitialLoaded"));
            // }
#endif
        }

        public bool VideoLoaded()
        {
            return TranssionAdMain.RewardIsLoaded;
/*#if UNITY_EDITOR
            return true;
#else
            return this.unityAdPlayerActivity != null && this.unityAdPlayerActivity.Call<bool>("isRewardLoaded");
#endif*/
        }

        public bool InterstitialLoaded()
        {
            return TranssionAdMain.InterstitialIsLoaded;
/*#if UNITY_EDITOR
            return true;
#else
            return this.unityAdPlayerActivity != null && this.unityAdPlayerActivity.Call<bool>("isInterstitialLoaded");
#endif*/
        }

        public void ShowVideo(Action success, Action fail)
        {
            TranssionAdMain.ShowReward(success, fail);
            /*Debug.Log(nameof(ShowVideo));
#if UNITY_EDITOR
            success?.Invoke();
#else
            if (this.unityAdPlayerActivity != null)
            {
                this.unityAdPlayerActivity.Call("ShowReward");
                RemoteConfig.Instance.RewardFailAction = fail;
                RemoteConfig.Instance.RewardSuccessAction = success;
            }
#endif*/
        }

        public void ShowInterstitialAd(Action interactionAdCompleted, Action hold)
        {
            TranssionAdMain.ShowInterstitial(interactionAdCompleted, hold);
            /*Debug.Log(nameof(ShowInterstitialAd));
#if UNITY_EDITOR
            interactionAdCompleted?.Invoke();
#else
            if (this.unityAdPlayerActivity != null)
            {
                this.unityAdPlayerActivity.Call("ShowInterstitial");
            }
#endif*/
        }

        public void OnApplicationPause(bool pause)
        {
        }

        public void ShowBanner(bool show)
        {
            /*Debug.Log(nameof(ShowBanner));
            if (this.unityAdPlayerActivity != null)
            {
                this.unityAdPlayerActivity.Call("ShowBanner");
            }*/
            if (show)
            {
                TranssionAdMain.ShowBanner();
            }
            else
            {
                TranssionAdMain.CloseBanner();
            }
        }

        public void ShowFloatingWindow(bool show, int startMargin=0, int endMargin=0, int bottomMargin=0)
        {
            /*Debug.Log(nameof(ShowFloatingWindow));
            var androidJavaObject = this.unityAdPlayerActivity;
            androidJavaObject?.Call("SetFloatActive", show, hight);*/
            if (show)
            {
                TranssionAdMain.ShowFloat(startMargin, endMargin, bottomMargin);
            }
            else
            {
                TranssionAdMain.HideFloat();
            }
        }
    }
}