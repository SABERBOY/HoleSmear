using System;
using System.Collections.Generic;
using AnyThinkAds.Api;
using UnityEngine;

namespace SDK
{
    public class TopOnSdk : ISDK, ATSDKInitListener
    {
        private VideoScenes mVideoScenes;
        private InterstitialScenes mInterstitialScenes;
        private BannerScenes mBannerScenes;
        private NativeBannerScene mNativeBannerScene;

        public void Init()
        {
            //设置开启Debug日志（强烈建议测试阶段开启，方便排查问题）
            ATSDKAPI.setLogDebug(false);
            //（必须配置）SDK的初始化
            ATSDKAPI.initSDK("a625ea9d410d16", "b1b44444ff8e3638c963fd14133a642d",
                this); //Use your own app_id & app_key here
        }

        public bool VideoLoaded()
        {
#if UNITY_EDITOR
            return true;
#endif
            return mVideoScenes != null && mVideoScenes.IsReady();
            // return this.mVideoScenes?IsReady();
        }

        public bool InterstitialLoaded()
        {
#if UNITY_EDITOR
            return true;
#endif
            return mInterstitialScenes != null && mInterstitialScenes.IsReady();
            // return true;
        }

        public void ShowVideo(Action success, Action fail)
        {
#if UNITY_EDITOR
            success?.Invoke();
            return;
#endif
            mVideoScenes.ShowVideo(success, fail);
        }

        public void ShowInterstitialAd(Action interactionAdCompleted, Action hold)
        {
#if UNITY_EDITOR
            interactionAdCompleted?.Invoke();
            return;
#endif
            mInterstitialScenes.ShowInterstitialAd(interactionAdCompleted, hold);
        }

        public void OnApplicationPause(bool pause)
        {
        }

        public void ShowBanner(bool show)
        {
            if (show)
            {
                if (mBannerScenes != null) mBannerScenes.showBannerAd();
            }
            else
            {
                if (mBannerScenes != null) mBannerScenes.reshowBannerAd();
            }
        }

        public void initSuccess()
        {
            Debug.Log("initSuccess");
            mBannerScenes = new BannerScenes();
            mInterstitialScenes = new InterstitialScenes();
            mVideoScenes = new VideoScenes();
            // mNativeBannerScene = new NativeBannerScene();
            this.ShowBanner(true);
            // ATSDKAPI.showGDPRAuth();
            //判断是否在欧盟地区
            ATSDKAPI.getUserLocation(new GetLocationListener());
        }


        //发布欧盟地区的开发者需使用以下授权代码，询问用户是否同意收集隐私数据
        private class GetLocationListener : ATGetUserLocationListener
        {
            public void didGetUserLocation(int location)
            {
                Debug.Log("Developer callback didGetUserLocation(): " + location);
                if (location == ATSDKAPI.kATUserLocationInEU && ATSDKAPI.getGDPRLevel() == ATSDKAPI.UNKNOWN)
                {
                    ATSDKAPI.showGDPRAuth();
                }
            }
        }

        public void initFail(string message)
        {
            Debug.Log($"initFail:{message}");
        }
    }

    public class NativeBannerScene : ATNativeBannerAdListener
    {
#if UNITY_ANDROID
        private static string mPlacementId_native_all = "b5aa1fa2cae775";

#elif UNITY_IOS || UNITY_IPHONE
		static string mPlacementId_native_all = "b5b0f555698607";
#else
        static string mPlacementId_native_all = "";

#endif
        public NativeBannerScene()
        {
            Start();
        }

        // Use this for initialization
        private void Start()
        {
            ATNativeBannerAd.Instance.setListener(this);
        }

        public void gotoMainScene()
        {
            Debug.Log("NativeBannerScene::gotoMainScene");
        }

        public void loadAd()
        {
            Debug.Log("NativeBannerScene::loadAd");
#if !UNITY_WEBGL
            ATNativeBannerAd.Instance.loadAd(mPlacementId_native_all, null);
#endif
        }

        public void showAd()
        {
#if !UNITY_WEBGL
            Debug.Log("NativeBannerScene::showAd");
            Debug.Log("Screen Width : " + Screen.width + ", Screen dpi: " + Screen.dpi);
            var arpuRect = new ATRect(0, 100, 414, 200);
            ATNativeBannerAd.Instance.showAd(mPlacementId_native_all, arpuRect,
                new Dictionary<string, string>
                {
                    { ATNativeBannerAdShowingExtra.kATNativeBannerAdShowingExtraBackgroundColor, "#FFFFFF" },
                    { ATNativeBannerAdShowingExtra.kATNativeBannerAdShowingExtraTitleColor, "#FF0000" }
                });
#endif
        }

        public void removeAd()
        {
#if !UNITY_WEBGL
            Debug.Log("NativeBannerScene::removeAd");
            ATNativeBannerAd.Instance.removeAd(mPlacementId_native_all);
#endif
        }

        public void adReady()
        {
#if !UNITY_WEBGL
            Debug.Log("ATNativeBannerAdListener Developer NativeBannerScene::adReady:" +
                      (ATNativeBannerAd.Instance.adReady(mPlacementId_native_all) ? "yes" : "no"));
#endif
        }

        public void onAdLoaded(string placementId)
        {
            Debug.Log("ATNativeBannerAdListener Developer onAdLoaded------:" + placementId);
        }

        public void onAdLoadFail(string placementId, string code, string message)
        {
            Debug.Log("ATNativeBannerAdListener Developer onAdLoadFail------:" + placementId + ", " + code + ", " +
                      message);
        }

        public void onAdImpressed(string placementId, ATCallbackInfo callbackInfo)
        {
            Debug.Log("ATNativeBannerAdListener Developer onAdImpressed------:" + placementId + "->" +
                      JsonUtility.ToJson(callbackInfo.toDictionary()));
        }

        public void onAdClicked(string placementId, ATCallbackInfo callbackInfo)
        {
            Debug.Log("ATNativeBannerAdListener Developer onAdClicked------:" + placementId + "->" +
                      JsonUtility.ToJson(callbackInfo.toDictionary()));
        }

        public void onAdAutoRefresh(string placementId, ATCallbackInfo callbackInfo)
        {
            Debug.Log("ATNativeBannerAdListener Developer onAdAutoRefresh------:" + placementId + "->" +
                      JsonUtility.ToJson(callbackInfo.toDictionary()));
        }

        public void onAdAutoRefreshFailure(string placementId, string code, string message)
        {
            Debug.Log("ATNativeBannerAdListener Developer onAdAutoRefreshFailure------:" + placementId);
        }

        public void onAdCloseButtonClicked(string placementId)
        {
            Debug.Log("ATNativeBannerAdListener Developer onAdCloseButtonClicked------:" + placementId);
        }
    }

    public class VideoScenes
    {
#if UNITY_ANDROID
        private static string mPlacementId_rewardvideo_all = "b625eabbfcd7ca";
        // static string showingScenario = "f5e71c46d1a28f";
#elif UNITY_IOS || UNITY_IPHONE
    static string mPlacementId_rewardvideo_all = "b5b44a0f115321";//"b5b44a0f115321";
    static string showingScenario = "f5e54970dc84e6";
#else
        static string mPlacementId_rewardvideo_all = "";
#endif

        private ATRewardedVideo rewardedVideo;

        public VideoScenes()
        {
            Start();
        }

        // Use this for initialization
        private void Start()
        {
            loadVideo();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void gotoMainScenes()
        {
            Debug.Log("Developer gotoMainScenes");
            // AnyThinkAds.Demo.ATManager.printLogI ("gotoMainScenes....");
            // SceneManager.LoadScene ("demoMainScenes");
        }

        public void gotoNativeScenes()
        {
            Debug.Log(" Developer gotoNativeScenes");
            // AnyThinkAds.Demo.ATManager.printLogI ("gotoNativeScenes....");
            // SceneManager.LoadScene ("nativeMainScenes");
        }


        private static ATCallbackListener callbackListener;

        public void loadVideo()
        {
            if (callbackListener == null)
                callbackListener = new ATCallbackListener();
            // Debug.Log("Developer init video....placementId:" + mPlacementId_rewardvideo_all);
            // ATRewardedVideo.Instance.setListener(callbackListener);

            // ATSDKAPI.setCustomDataForPlacementID(
            //     new Dictionary<string, string> { { "placement_custom_key", "placement_custom" } },
            //     mPlacementId_rewardvideo_all);
            //
            // Dictionary<string, string> jsonmap = new Dictionary<string, string>();
            // jsonmap.Add(ATConst.USERID_KEY, "test_user_id");
            // jsonmap.Add(ATConst.USER_EXTRA_DATA, "test_user_extra_data");


            // ATRewardedVideo.Instance.loadVideoAd(mPlacementId_rewardvideo_all, jsonmap);

#if !UNITY_WEBGL
            // ATRewardedAutoVideo.Instance.setListener(callbackListener);
            ATRewardedAutoVideo.Instance.client.onAdLoadEvent += callbackListener.onAdLoad;
            ATRewardedAutoVideo.Instance.client.onAdLoadFailureEvent += callbackListener.onAdLoadFail;
            ATRewardedAutoVideo.Instance.client.onAdVideoStartEvent += callbackListener.onAdVideoStartEvent;
            ATRewardedAutoVideo.Instance.client.onAdVideoEndEvent += callbackListener.onAdVideoEndEvent;
            ATRewardedAutoVideo.Instance.client.onAdVideoFailureEvent += callbackListener.onAdVideoPlayFail;
            ATRewardedAutoVideo.Instance.client.onAdClickEvent += callbackListener.onAdClick;
            ATRewardedAutoVideo.Instance.client.onRewardEvent += callbackListener.onReward;

            ATRewardedAutoVideo.Instance.client.onAdVideoCloseEvent += callbackListener.onAdVideoClosedEvent;
            ATRewardedAutoVideo.Instance.addAutoLoadAdPlacementID(new[] { mPlacementId_rewardvideo_all });
#endif
        }

        public void ShowVideo(Action onSuccess, Action onFail)
        {
            if (IsReady())
            {
                // Debug.Log("Developer show video....");

                /*Dictionary<string, string> jsonmap = new Dictionary<string, string>();
                // jsonmap.Add(AnyThinkAds.Api.ATConst.SCENARIO, showingScenario);
                callbackListener.failCallback = onFail;
                callbackListener.successCallback = onSuccess;
                ATRewardedVideo.Instance.showAd(mPlacementId_rewardvideo_all, jsonmap);*/
#if !UNITY_WEBGL
                callbackListener.failCallback = onFail;
                callbackListener.successCallback = onSuccess;
                ATRewardedAutoVideo.Instance.showAutoAd(mPlacementId_rewardvideo_all);
#endif
            }
            else
            {
                // Debug.Log("Developer show video fail....");
                onFail?.Invoke();
            }
        }

        public bool IsReady()
        {
            // Debug.Log ("Developer isReady ?....");
            /*bool b = ATRewardedVideo.Instance.hasAdReady(mPlacementId_rewardvideo_all);
            Debug.Log("Developer isReady video...." + b);

            string adStatus = ATRewardedVideo.Instance.checkAdStatus(mPlacementId_rewardvideo_all);
            Debug.Log("Developer checkAdStatus video...." + adStatus);
            return b;*/
#if !UNITY_WEBGL
            return ATRewardedAutoVideo.Instance.autoLoadRewardedVideoReadyForPlacementID(mPlacementId_rewardvideo_all);
#endif
            return true;
        }

        private class ATCallbackListener : ATRewardedVideoListener
        {
            public Action successCallback;
            public Action failCallback;

            public void onRewardedVideoAdLoaded(string placementId)
            {
                /*Debug.Log("ATCallbackListener Developer onRewardedVideoAdLoaded------");*/
            }

            public void onRewardedVideoAdLoadFail(string placementId, string code, string message)
            {
                /*Debug.Log("ATCallbackListener Developer onRewardedVideoAdLoadFail------:code" + code + "--message:" +
                          message);*/
            }

            public void onRewardedVideoAdPlayStart(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("ATCallbackListener Developer onRewardedVideoAdPlayStart------" + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void onRewardedVideoAdPlayEnd(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("ATCallbackListener Developer onRewardedVideoAdPlayEnd------" + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void onRewardedVideoAdPlayFail(string placementId, string code, string message)
            {
                failCallback?.Invoke();
                Debug.Log("ATCallbackListener Developer onRewardedVideoAdPlayFail------code:" + code + "---message:" +
                          message);
            }

            public void onRewardedVideoAdPlayClosed(string placementId, bool isReward, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("ATCallbackListener Developer onRewardedVideoAdPlayClosed------isReward:" + isReward + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void onRewardedVideoAdPlayClicked(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("ATCallbackListener Developer onRewardVideoAdPlayClicked------" + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void onReward(string placementId, ATCallbackInfo callbackInfo)
            {
                successCallback?.Invoke();
                Debug.Log("ATCallbackListener Developer onReward------" + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));
            }

            public void startLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("ATCallbackListener Developer startLoadingADSource------" + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void finishLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("ATCallbackListener Developer finishLoadingADSource------" + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void failToLoadADSource(string placementId, ATCallbackInfo callbackInfo, string code, string message)
            {
                Debug.Log("ATCallbackListener Developer failToLoadADSource------code:" + code + "---message:" +
                          message);
            }

            public void startBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("ATCallbackListener Developer startBiddingADSource------" + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void finishBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("ATCallbackListener Developer finishBiddingADSource------" + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void failBiddingADSource(string placementId, ATCallbackInfo callbackInfo, string code,
                string message)
            {
                /*Debug.Log(
                    "ATCallbackListener Developer failBiddingADSource------code:" + code + "---message:" + message);*/
            }

            public void onAdLoad(object sender, ATAdEventArgs e)
            {
                /*Debug.Log("onAdLoad:" + e.placementId);*/
            }

            public void onAdLoadFail(object sender, ATAdErrorEventArgs e)
            {
            }

            public void onAdVideoStartEvent(object sender, ATAdEventArgs e)
            {
            }

            public void onReward(object sender, ATAdEventArgs e)
            {
                this.onReward("", e.callbackInfo);
            }

            public void onAdVideoEndEvent(object sender, ATAdEventArgs e)
            {
            }

            public void onAdClick(object sender, ATAdEventArgs e)
            {
            }

            public void onAdVideoPlayFail(object sender, ATAdErrorEventArgs e)
            {
                this.onRewardedVideoAdPlayFail(e.placementId, e.errorCode, e.errorMessage);
            }

            public void onAdVideoClosedEvent(object sender, ATAdRewardEventArgs e)
            {
            }
        }
    }

    public class InterstitialScenes
    {
#if UNITY_ANDROID
        private static string mPlacementId_interstitial_all = "b625eabb0d4c3b";
        // static string showingScenario = "f5e71c49060ab3";

#elif UNITY_IOS || UNITY_IPHONE
    static string mPlacementId_interstitial_all = "b5bacad26a752a";
    static string showingScenario = "f5e549727efc49";
#else
        static string mPlacementId_interstitial_all = "";

#endif

        public InterstitialScenes()
        {
            Start();
        }

        // Use this for initialization
        private void Start()
        {
            loadInterstitialAd();
        }

        private static InterstitalCallback callback;

        public void loadInterstitialAd()
        {
            if (callback == null)
                callback = new InterstitalCallback();
            // ATInterstitialAd.Instance.setListener(callback);

            // Dictionary<string, object> jsonmap = new Dictionary<string, object>();
            // jsonmap.Add(AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL,
            //     AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL_NO);
            // //jsonmap.Add(AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL, AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL_YES);
            //
            // ATInterstitialAd.Instance.loadInterstitialAd(mPlacementId_interstitial_all, jsonmap);
#if !UNITY_WEBGL
            // ATInterstitialAutoAd.Instance.setListener(callback);
            ATInterstitialAutoAd.Instance.client.onAdLoadEvent += callback.onAdLoad;
            ATInterstitialAutoAd.Instance.client.onAdLoadFailureEvent += callback.onAdLoadFail;
            ATInterstitialAutoAd.Instance.client.onAdShowEvent += callback.onShow;
            ATInterstitialAutoAd.Instance.client.onAdClickEvent += callback.onAdClick;
            ATInterstitialAutoAd.Instance.client.onAdCloseEvent += callback.onAdClose;
            ATInterstitialAutoAd.Instance.client.onAdShowFailureEvent += callback.onAdShowFail;
            ATInterstitialAutoAd.Instance.client.onAdVideoStartEvent += callback.startVideoPlayback;
            ATInterstitialAutoAd.Instance.client.onAdVideoEndEvent += callback.endVideoPlayback;
            ATInterstitialAutoAd.Instance.client.onAdVideoFailureEvent += callback.failVideoPlayback;
            ATInterstitialAutoAd.Instance.addAutoLoadAdPlacementID(new[] { mPlacementId_interstitial_all });
#endif
        }

        public bool IsReady()
        {
            /*bool b = ATInterstitialAd.Instance.hasInterstitialAdReady(mPlacementId_interstitial_all);
            Debug.Log("Developer isReady interstitial...." + b);

            string adStatus = ATInterstitialAd.Instance.checkAdStatus(mPlacementId_interstitial_all);
            Debug.Log("Developer checkAdStatus interstitial...." + adStatus);
            return b;*/
#if !UNITY_WEBGL
            return ATInterstitialAutoAd.Instance.autoLoadInterstitialAdReadyForPlacementID(
                mPlacementId_interstitial_all);
#endif
            return true;
        }

        public void ShowInterstitialAd(Action showSuccess, Action showFailed)
        {
            if (IsReady())
            {
#if !UNITY_WEBGL
                var jsonmap = new Dictionary<string, string>();
                // jsonmap.Add(AnyThinkAds.Api.ATConst.SCENARIO, showingScenario);
                callback.successCallback = showSuccess;
                callback.failCallback = showFailed;
                // ATInterstitialAd.Instance.showInterstitialAd(mPlacementId_interstitial_all, jsonmap);
                ATInterstitialAutoAd.Instance.showAutoAd(mPlacementId_interstitial_all,
                    jsonmap);
#endif
            }
            else
            {
                showFailed?.Invoke();
            }
        }

        private class InterstitalCallback : ATInterstitialAdListener
        {
            public Action successCallback;
            public Action failCallback;

            public void onInterstitialAdClick(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("InterstitalCallback Developer callback onInterstitialAdClick :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void onInterstitialAdClose(string placementId, ATCallbackInfo callbackInfo)
            {
                successCallback?.Invoke();
                Debug.Log("InterstitalCallback Developer callback onInterstitialAdClose :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));
            }

            public void onInterstitialAdEndPlayingVideo(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("InterstitalCallback Developer callback onInterstitialAdEndPlayingVideo :" + placementId +
                          "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void onInterstitialAdFailedToPlayVideo(string placementId, string code, string message)
            {
                /*Debug.Log("InterstitalCallback Developer callback onInterstitialAdFailedToPlayVideo :" + placementId +
                          "--code:" + code +
                          "--msg:" + message);*/
            }

            public void startLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("InterstitalCallback Developer callback startLoadingADSource :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void finishLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("InterstitalCallback Developer callback finishLoadingADSource :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void failToLoadADSource(string placementId, ATCallbackInfo callbackInfo, string code, string message)
            {
                /*Debug.Log("InterstitalCallback Developer callback failToLoadADSource :" + placementId + "--code:" +
                          code + "--msg:" +
                          message);*/
            }

            public void startBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("InterstitalCallback Developer callback startBiddingADSource :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void finishBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("InterstitalCallback Developer callback finishBiddingADSource :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void failBiddingADSource(string placementId, ATCallbackInfo callbackInfo, string code,
                string message)
            {
                /*Debug.Log("InterstitalCallback Developer callback failBiddingADSource :" + placementId + "--code:" +
                          code + "--msg:" +
                          message);*/
            }

            public void onInterstitialAdLoad(string placementId)
            {
                /*Debug.Log("InterstitalCallback Developer callback onInterstitialAdLoad :" + placementId);*/
            }

            public void onInterstitialAdLoadFail(string placementId, string code, string message)
            {
                /*Debug.Log("InterstitalCallback Developer callback onInterstitialAdLoadFail :" + placementId +
                          "--code:" + code + "--msg:" +
                          message);*/
            }

            public void onInterstitialAdShow(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("InterstitalCallback Developer callback onInterstitialAdShow :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void onInterstitialAdStartPlayingVideo(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("InterstitalCallback Developer callback onInterstitialAdStartPlayingVideo :" + placementId +
                          "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void onInterstitialAdFailedToShow(string placementId)
            {
                failCallback?.Invoke();
                Debug.Log("InterstitalCallback Developer callback onInterstitialAdFailedToShow :" + placementId);
            }

            public void onAdLoad(object sender, ATAdEventArgs e)
            {
            }

            public void onAdLoadFail(object sender, ATAdErrorEventArgs e)
            {
            }

            public void onShow(object sender, ATAdEventArgs e)
            {
            }

            public void onAdClick(object sender, ATAdEventArgs e)
            {
            }

            public void onAdClose(object sender, ATAdEventArgs e)
            {
                this.onInterstitialAdClose(e.placementId, e.callbackInfo);
            }

            public void onAdShowFail(object sender, ATAdErrorEventArgs e)
            {
                this.onInterstitialAdFailedToShow(e.placementId);
            }

            public void startVideoPlayback(object sender, ATAdEventArgs e)
            {
            }

            public void endVideoPlayback(object sender, ATAdEventArgs e)
            {
            }

            public void failVideoPlayback(object sender, ATAdErrorEventArgs e)
            {
            }
        }
    }

    public class BannerScenes
    {
#if UNITY_ANDROID
        private static string mPlacementId_banner_all = "b625eaaeeaf7e7";
        // static string showingScenario = "f600e6039e152c";

#elif UNITY_IOS || UNITY_IPHONE
	static string mPlacementId_banner_all = "b5bacaccb61c29";
    //static string mPlacementId_banner_all = "b5bacaccb61c29";
    static string showingScenario = "";
#else
        static string mPlacementId_banner_all = "";
#endif

        private int screenWidth;

        public BannerScenes()
        {
            Start();
        }

        // Use this for initialization
        private void Start()
        {
            screenWidth = Screen.width;
            loadBannerAd();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void gotoMainScenes()
        {
            Debug.Log("Developer gotoMainScenes");
            // AnyThinkAds.Demo.ATManager.printLogI ("Developer gotoMainScenes....");
            // SceneManager.LoadScene ("demoMainScenes");
        }

        private static BannerCallback bannerCallback;

        public void loadBannerAd()
        {
            if (bannerCallback == null)
            {
                bannerCallback = new BannerCallback();
                // ATBannerAd.Instance.setListener(bannerCallback);
            }

            var jsonmap = new Dictionary<string, object>();


#if UNITY_ANDROID
            var bannerSize = new ATSize(960, 150, true);
            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraBannerAdSizeStruct, bannerSize);
            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveWidth, bannerSize.width);
            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveOrientation,
                ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveOrientationPortrait);
#elif UNITY_IOS || UNITY_IPHONE
            ATSize bannerSize = new ATSize(320, 50, false);
            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraBannerAdSizeStruct, bannerSize);
            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveWidth, bannerSize.width);
            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveOrientation, ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveOrientationPortrait);

#endif
#if !UNITY_WEBGL
            ATBannerAd.Instance.loadBannerAd(mPlacementId_banner_all, jsonmap);
#endif
        }

        public void showBannerAd()
        {
#if !UNITY_WEBGL
            removeBannerAd();
            // Debug.Log("Developer is banner ready....");
            var adStatus = ATBannerAd.Instance.checkAdStatus(mPlacementId_banner_all);
            // Debug.Log("Developer checkAdStatus banner...." + adStatus);


            // ATRect arpuRect = new ATRect(0,50, this.screenWidth, 300, true);
            // ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all, arpuRect);
            // ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all, ATBannerAdLoadingExtra.kATBannerAdShowingPisitionBottom);
            ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all,
                ATBannerAdLoadingExtra.kATBannerAdShowingPisitionBottom);


            //show with scenario
            //        Dictionary<string, string> jsonmap = new Dictionary<string, string>();
            //        jsonmap.Add(AnyThinkAds.Api.ATConst.SCENARIO, showingScenario);
            //        //ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all, arpuRect, jsonmap);
            //        ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all, ATBannerAdLoadingExtra.kATBannerAdShowingPisitionTop, jsonmap);
#endif
        }

        public void removeBannerAd()
        {
#if !UNITY_WEBGL
            ATBannerAd.Instance.cleanBannerAd(mPlacementId_banner_all);
#endif
        }

        public void hideBannerAd()
        {
#if !UNITY_WEBGL
            ATBannerAd.Instance.hideBannerAd(mPlacementId_banner_all);
#endif
        }

        /*
         * Use this method when you want to reshow a banner that is previously hidden(by calling hideBannerAd)
        */
        public void reshowBannerAd()
        {
#if !UNITY_WEBGL
            ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all);
#endif
        }

        private class BannerCallback : ATBannerAdListener
        {
            public void onAdAutoRefresh(string placementId, ATCallbackInfo callbackInfo)
            {
                Debug.Log("BannerCallback Developer callback onAdAutoRefresh :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));
            }

            public void onAdAutoRefreshFail(string placementId, string code, string message)
            {
                Debug.Log("BannerCallback Developer callback onAdAutoRefreshFail : " + placementId + "--code:" + code +
                          "--msg:" +
                          message);
            }

            public void onAdClick(string placementId, ATCallbackInfo callbackInfo)
            {
                Debug.Log("BannerCallback Developer callback onAdClick :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));
            }

            public void onAdClose(string placementId)
            {
                Debug.Log("BannerCallback Developer callback onAdClose :" + placementId);
            }

            public void onAdCloseButtonTapped(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("BannerCallback Developer callback onAdCloseButtonTapped :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void startLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                Debug.Log("BannerCallback Developer callback startLoadingADSource :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));
            }

            public void finishLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                Debug.Log("BannerCallback Developer callback finishLoadingADSource :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));
            }

            public void failToLoadADSource(string placementId, ATCallbackInfo callbackInfo, string code, string message)
            {
                Debug.Log("BannerCallback Developer callback failToLoadADSource :" + placementId + "--code:" + code +
                          "--msg:" +
                          message);
            }

            public void startBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("BannerCallback Developer callback startBiddingADSource :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void finishBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("BannerCallback Developer callback finishBiddingADSource :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void failBiddingADSource(string placementId, ATCallbackInfo callbackInfo, string code,
                string message)
            {
                /*Debug.Log("BannerCallback Developer callback failBiddingADSource :" + placementId + "--code:" + code +
                          "--msg:" +
                          message);*/
            }

            public void onAdImpress(string placementId, ATCallbackInfo callbackInfo)
            {
                /*Debug.Log("BannerCallback Developer callback onAdImpress :" + placementId + "->" +
                          JsonUtility.ToJson(callbackInfo.toDictionary()));*/
            }

            public void onAdLoad(string placementId)
            {
                Debug.Log("BannerCallback Developer callback onAdLoad :" + placementId);
            }

            public void onAdLoadFail(string placementId, string code, string message)
            {
                Debug.Log("BannerCallback Developer callback onAdLoadFail : : " + placementId + "--code:" + code +
                          "--msg:" + message);
            }
        }
    }
}