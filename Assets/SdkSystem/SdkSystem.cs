﻿using System;
using System.Collections.Generic;
#if ADMOB
using Firebase;
#endif
using SDK;
using UnityEngine.Analytics;

namespace SDK
{
    /// <summary>
    /// sdk管理
    /// </summary>
    public class SdkSystem
    {
        public static SdkSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SdkSystem();
                }

                return _instance;
            }
        }

        private static SdkSystem _instance = null;
        private ISDK _sdk;

        private SdkSystem()
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
#if UNITY_EDITOR
            // this._sdk = new ADMob();
            // this._sdk.Init();
#elif UNITY_ANDROID
            this._sdk = new TopOnSdk();
            this._sdk.Init();
#elif UNITY_IOS
            // this._sdk = new IOSSDK();
            // this._sdk.Init();
#endif
        }


        /// <summary>
        /// 激励视频是否加载成功
        /// </summary>
        /// <returns></returns>
        public bool IsRewardAdLoaded()
        {
            var adLoaded = this._sdk.VideoLoaded();
            AnalyticsEvent.Custom("Reward", new Dictionary<string, object> { { "IsRewardAdLoaded", adLoaded } });
            return adLoaded;
        }


        /// <summary>
        /// 插屏是否加载成功
        /// </summary>
        /// <returns></returns>
        public bool IsInterstitialLoaded()
        {
            var adLoaded = this._sdk.InterstitialLoaded();
            AnalyticsEvent.Custom("Interstitial",
                new Dictionary<string, object> { { "IsInterstitialLoaded", adLoaded } });
            return this._sdk.InterstitialLoaded();
        }


        /// <summary>
        /// 播放激励视频
        /// </summary>
        public void ShowRewardVideoAd(Action success, Action fail)
        {
            AnalyticsEvent.AdStart(false, AdvertisingNetwork.UnityAds, "ShowRewardVideoAd");
            /* tGSDKController.ShowRewardVideoAd(); */
            this._sdk.ShowVideo((() =>
            {
                success?.Invoke();
                AnalyticsEvent.AdComplete(true, AdvertisingNetwork.UnityAds, "ShowRewardVideoAd");
            }), (() =>
            {
                fail?.Invoke();
                AnalyticsEvent.AdComplete(false, AdvertisingNetwork.UnityAds, "ShowRewardVideoAd");
            }));
        }


        /// <summary>
        /// 播放插屏广告
        /// </summary>
        public void ShowInterstitial(Action interactionAdCompleted, Action hold)
        {
            /* tGSDKController.ShowRewardVideoAd(); */
            AnalyticsEvent.AdStart(false, AdvertisingNetwork.UnityAds, "ShowInterstitial");
            this._sdk.ShowInterstitialAd((() =>
            {
                interactionAdCompleted?.Invoke();
                AnalyticsEvent.AdStart(true, AdvertisingNetwork.UnityAds, "ShowInterstitial");
            }), (() =>
            {
                hold?.Invoke();
                AnalyticsEvent.AdStart(false, AdvertisingNetwork.UnityAds, "ShowInterstitial");
            }));
        }

        public void ShowFloatingWindow(bool show, int hight)
        {
            if (this._sdk is ADMob sdk)
            {
                sdk?.ShowFloatingWindow(show, hight);
            }
        }

        public void ShowBanner(bool active)
        {
            this._sdk.ShowBanner(active);
        }
    }
}