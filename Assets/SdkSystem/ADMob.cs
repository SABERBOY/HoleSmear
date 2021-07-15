// #define ADMOB

#if UNITY_ANDROID
using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using GoogleMobileAds.Android;
using GoogleMobileAds.Api;
using SDK;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Script.SDK
{
    public class ADMob : ISDK
    {
        private List<ADMobConstructor> adMobConstructors = new List<ADMobConstructor>();

        private List<ADMobInterstitialAdConstructor> adMobInterstitialAdConstructors =
            new List<ADMobInterstitialAdConstructor>();

        private ADMobBannerConstructor adMobBannerConstructor = null;

        private List<string> rewardId = new List<string>()
        {
            "ca-app-pub-2270136017335510/9262363840"
        };

        private List<string> interstitialId = new List<string>()
        {
            "ca-app-pub-2270136017335510/3469135203"
        };

        //ca-app-pub-2898660159223218/4601696319//测试广告id
        //ca-app-pub-2898660159223218/9906472812
        //插屏广告ca-app-pub-2898660159223218/9067425051
        public void Init()
        {
            // MobileAds.Initialize("ca-app-pub-2898660159223218~4793268009");
            // var builder = new RequestConfiguration();
            // RequestConfiguration.Builder().setTestDeviceIds(Arrays.asList("83C0EECDFE32F56622BF2A7B4C6A0AEF")) to get test ads on this device.
            // MobileAdsClient.Instance.GetRequestConfiguration().ToBuilder()
            //     .SetTestDeviceIds(new List<string>() {"83C0EECDFE32F56622BF2A7B4C6A0AEF"});
            MobileAds.SetRequestConfiguration(MobileAds.GetRequestConfiguration().ToBuilder()
                .SetTestDeviceIds(new List<string>() {"83C0EECDFE32F56622BF2A7B4C6A0AEF"}).build());
            MobileAds.Initialize(initStatus =>
            {
                foreach (var s in this.rewardId)
                {
                    this.adMobConstructors.Add(new ADMobConstructor(s));
                }

                foreach (var s in interstitialId)
                {
                    this.adMobInterstitialAdConstructors.Add(new ADMobInterstitialAdConstructor(s));
                }

                this.adMobBannerConstructor = new ADMobBannerConstructor("ca-app-pub-2270136017335510/9155742009");

                Debug.Log(initStatus.getAdapterStatusMap() + "ADMob");
            });
        }

        public bool VideoLoaded()
        {
            ADMobConstructor a = null;
            foreach (ADMobConstructor adMobConstructor in this.adMobConstructors)
            {
                if (adMobConstructor.IsLoaded())
                {
                    a = adMobConstructor;
                    break;
                }
            }

            return (a != null && a.IsLoaded()) || this.InterstitialLoaded();
            // return this.InterstitialLoaded();
        }

        public bool InterstitialLoaded()
        {
            ADMobInterstitialAdConstructor a = null;
            foreach (var inter in this.adMobInterstitialAdConstructors)
            {
                if (inter.IsLoaded())
                {
                    a = inter;
                    break;
                }
            }

            return (a != null && a.IsLoaded()) || this.VideoLoaded();
        }


        public void ShowVideo(Action success, Action fail)
        {
            Debug.Log("展示视频");
#if UNITY_EDITOR
            success?.Invoke();
#elif UNITY_ANDROID
            ADMobConstructor a = null;
            foreach (ADMobConstructor adMobConstructor in this.adMobConstructors)
            {
                if (adMobConstructor.IsLoaded())
                {
                    adMobConstructor.success = success;
                    adMobConstructor.fail = fail;
                    a = adMobConstructor;
                    break;
                }
            }

            if (a != null && a.IsLoaded())
            {
                a.Show();
            }
            else
            {
                fail?.Invoke();
            }
#elif UNITY_IOS
#endif
        }

        public void ShowInterstitialAd(Action interactionAdCompleted, Action hold)
        {
            Debug.Log("展示插屏");
            ADMobInterstitialAdConstructor a = null;
            foreach (var inter in this.adMobInterstitialAdConstructors)
            {
                if (inter.IsLoaded())
                {
                    inter.complete = interactionAdCompleted;
                    a = inter;
                    break;
                }
            }

            if (a != null && a.IsLoaded())
            {
                hold?.Invoke();
                a.Show();
            }
        }

        public void OnApplicationPause(bool pause)
        {
        }
    }

    /// <summary>
    /// 激励视频广告构造器
    /// </summary>
    class ADMobConstructor : IUnityAdsListener
    {
        public Action success = null;
        public Action fail = null;
        private string id;
        private RewardedAd RewardedAd;
        private bool canGetTheReward = false;
        private bool isLoadingAd = true;
        private string mySurfacingId = "rewardedVideo";

        public ADMobConstructor(string id)
        {
            this.id = id;
            if (RemoteConfig.Instance.IsMagic)
            {
                Debug.Log("Unity:" + Advertisement.IsReady(mySurfacingId));
                Advertisement.AddListener(this);
            }
            else
            {
                this.RewardedAd = this.CreateAndLoadRewardedAd(this.id);
            }
        }

        public bool IsLoaded()
        {
            return RemoteConfig.Instance.IsMagic ? Advertisement.IsReady(mySurfacingId) : this.RewardedAd.IsLoaded();
        }

        public void Show()
        {
            if (RemoteConfig.Instance.IsMagic)
            {
                Advertisement.Show(mySurfacingId);
            }
            else
            {
                this.RewardedAd.Show();
            }
        }

        private RewardedAd CreateAndLoadRewardedAd(string adUnitId)
        {
            canGetTheReward = false;
            RewardedAd rewardedAd = new RewardedAd(adUnitId);
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;
            rewardedAd.OnAdFailedToLoad += HandleAdLoadFailed;
            rewardedAd.OnAdFailedToShow += HandleAdShowFailed;
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            rewardedAd.LoadAd(request);
            isLoadingAd = true;
            return rewardedAd;
        }

        private void HandleAdShowFailed(object sender, AdErrorEventArgs e)
        {
            Debug.Log($"{nameof(ADMobConstructor)}:{nameof(HandleAdShowFailed)}" + e);
            isLoadingAd = false;
            NativeConnect.Connect.StartCoroutine(LoadAdAgain(5));
        }

        private void HandleAdLoadFailed(object sender, AdErrorEventArgs e)
        {
            isLoadingAd = false;
            Debug.Log($"{nameof(ADMobConstructor)}:{nameof(HandleAdLoadFailed)}" + e);
            NativeConnect.Connect.StartCoroutine(LoadAdAgain(5));
        }

        private void HandleRewardedAdClosed(object sender, EventArgs e)
        {
            if (canGetTheReward)
            {
                this.success?.Invoke();
            }
            else
            {
                this.fail?.Invoke();
            }

            isLoadingAd = false;
            NativeConnect.Connect.StartCoroutine(LoadAdAgain(0));
            Debug.Log(e + "关闭广告");
        }

        IEnumerator LoadAdAgain(float f)
        {
            yield return new WaitForSeconds(f);
            if (!isLoadingAd)
            {
                this.RewardedAd = CreateAndLoadRewardedAd(this.id);
            }
        }

        private void HandleUserEarnedReward(object sender, GoogleMobileAds.Api.Reward e)
        {
            this.canGetTheReward = true;
            Debug.Log(e + "可以获取奖励");
        }

        private void HandleRewardedAdLoaded(object sender, EventArgs e)
        {
            // Debug.LogError(e);
        }

        public void OnUnityAdsReady(string placementId)
        {
            // Check if UnityAds ready before calling Show method:
            if (Advertisement.IsReady(mySurfacingId))
            {
                Advertisement.Show(mySurfacingId);
            }
            else
            {
                Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
            }
        }

        public void OnUnityAdsDidError(string message)
        {
            // Log the error.
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            // Optional actions to take when the end-users triggers an ad.
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
                this.success?.Invoke();
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
                this.fail?.Invoke();
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
                this.fail?.Invoke();
            }
        }
    }

    /// <summary>
    /// 插屏构造
    /// </summary>
    class ADMobInterstitialAdConstructor : IUnityAdsListener
    {
        public Action complete = null;
        private InterstitialAd interstitial;
        private string id;
        private string mySurfacingId = "rewardedVideo";

        public ADMobInterstitialAdConstructor(string id)
        {
            this.id = id;
            if (RemoteConfig.Instance.IsMagic)
            {
            }
            else
            {
                this.interstitial = CreateInterstitialAd(this.id);
            }
        }

        public bool IsLoaded()
        {
            return RemoteConfig.Instance.IsMagic ? Advertisement.IsReady(mySurfacingId) : this.interstitial.IsLoaded();
        }

        public void Show()
        {
            if (RemoteConfig.Instance.IsMagic)
            {
                Advertisement.Show(mySurfacingId);
            }
            else
            {
                this.interstitial.Show();
            }
        }

        private InterstitialAd CreateInterstitialAd(string adUnitId)
        {
            var interstitial = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            interstitial.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            interstitial.OnAdOpening += HandleOnAdOpened;
            // Called when the ad is closed.
            interstitial.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);
            return interstitial;
        }

        private void HandleOnAdLeavingApplication(object sender, EventArgs e)
        {
            Debug.Log(e);
        }

        private void HandleOnAdClosed(object sender, EventArgs e)
        {
            this.complete?.Invoke();
            NativeConnect.Connect.StartCoroutine(LoadAdAgain(0));
        }

        IEnumerator LoadAdAgain(float f)
        {
            yield return new WaitForSeconds(f);
            this.interstitial.Destroy();
            this.interstitial = this.CreateInterstitialAd(this.id);
        }

        private void HandleOnAdOpened(object sender, EventArgs e)
        {
        }

        private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            NativeConnect.Connect.StartCoroutine(LoadAdAgain(5));
        }

        private void HandleOnAdLoaded(object sender, EventArgs e)
        {
        }

        public void OnUnityAdsReady(string placementId)
        {
        }

        public void OnUnityAdsDidError(string message)
        {
        }

        public void OnUnityAdsDidStart(string placementId)
        {
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
        }
    }

    class ADMobBannerConstructor
    {
        private string id;
        private BannerView bannerView;

        public ADMobBannerConstructor(string id)
        {
            this.id = id;
            this.bannerView = new BannerView(this.id, AdSize.Banner, AdPosition.Bottom);
            // Called when an ad request has successfully loaded.
            this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            this.bannerView.OnAdOpening += this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            this.bannerView.OnAdClosed += this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the banner with the request.
            this.bannerView.LoadAd(request);
        }

        private void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.Message);
        }

        private void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        private void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
        }

        private void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }
    }
}
#endif