using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    private const string InterActive = "InterActive";
    private const string InterstitialVideo = "InterstitialVideo";
    private const string Interstitial = "Interstitial";
    private const string RewardVideo = "RewardVideo";
    private const string OfferWall = "OfferWall";
    private const string AppWall = "AppWall";
    private const string Native = "Native";

    public const string MTGSDKAppIDForAndroid = "92762";
    public const string MTGSDKApiKeyForAndroid = "936dcbdd57fe235fd7cf61c2e93da3c4";

    private static string[] _adTypeList =
    {
        InterActive,
        InterstitialVideo,
        Interstitial,
        RewardVideo,
        OfferWall,
        AppWall,
        Native
    };

    private static readonly Dictionary<string, string[]> _adUnitDict = new Dictionary<string, string[]>
    {
        { InterActive, new[] { "48127" } },
        { InterstitialVideo, new[] { "35811" } },
        { Interstitial, new[] { "21312" } },
        { RewardVideo, new[] { "21310", "30043" } },
        { OfferWall, new[] { "21311" } },
        { AppWall, new[] { "21308" } },
        { Native, new[] { "21306", "1611993839047594_1614040148842963" } }
    };

    private string[] _AdUnits;
    private int _selectedToggleIndex;
    private int GDPR_OFF = 0;

    private readonly int GDPR_ON = 1;

    private static bool IsAdUnitArrayNullOrEmpty(string[] adUnitArray)
    {
        return adUnitArray == null || adUnitArray.Length == 0;
    }

    private void Awake()
    {
        // init MTGSDK
        initMTGSDK();
        // init Ads
        initAllAds();
    }

    private void Start()
    {
        /* Mintegral.requestRewardedVideo(_adUnitDict[RewardVideo][Random.Range(0, 2)]);
        Mintegral.requestInterstitialAd(_adUnitDict[Interstitial][0]); */
    }

    private void initMTGSDK()
    {
        /* Mintegral.setConsentStatusInfoType(GDPR_ON);
        mtgLog("userPrivateInfo ConsentStatus : " + Mintegral.getConsentStatusInfoType());
        Mintegral.initMTGSDK(MTGSDKAppIDForAndroid, MTGSDKApiKeyForAndroid); */
    }

    private void initAllAds()
    {
        //Interstitial
        /* var interstitialUnits = _adUnitDict.ContainsKey(Interstitial) ? _adUnitDict[Interstitial] : null;
        var interstitialAdInfos = new MTGInterstitialInfo[interstitialUnits.Length];
        for (var i = 0; i < interstitialUnits.Length; i++)
        {
            var unit = interstitialUnits[i];
            MTGInterstitialInfo info;
            info.adUnitId = unit;
            info.adCategory = MTGAdCategory.MTGAD_CATEGORY_ALL;
            interstitialAdInfos[i] = info;
        }

        Mintegral.loadInterstitialPluginsForAdUnits(interstitialAdInfos);
        //RewardVideo
        var rewardVideoUnits = _adUnitDict.ContainsKey(RewardVideo) ? _adUnitDict[RewardVideo] : null;
        Mintegral.loadRewardedVideoPluginsForAdUnits(rewardVideoUnits); */
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public static void ShowRewardVideo() //展示激励视频
    {
        /* var index = Random.Range(0, 2);
        if (Mintegral.isVideoReadyToPlay(_adUnitDict[RewardVideo][index]))
            Mintegral.showRewardedVideo(_adUnitDict[RewardVideo][index]); */
    }

    public static void ShowInterstitial() //插屏广告
    {
        /* Mintegral.showInterstitialAd(_adUnitDict[Interstitial][0]); */
    }

    private void mtgLog(string log)
    {
        Debug.Log("Mintegral: " + log + "\n" + "------------------------------");
    }

    #region 激励视频回调

    //广告加载成功的回调
    private void onRewardedVideoLoadSuccessEvent(string adUnitId)
    {
        mtgLog("onRewardedVideoLoadSuccessEvent: " + adUnitId);
    }

    //广告缓存完成的回调
    private void onRewardedVideoLoadedEvent(string adUnitId)
    {
        mtgLog("onRewardedVideoLoadedEvent: " + adUnitId);
    }

    //广告加载失败的回调
    private void onRewardedVideoFailedEvent(string errorMsg)
    {
        mtgLog("onRewardedVideoFailedEvent: " + errorMsg);
    }

    //广告展示失败的回调
    private void onRewardedVideoShownFailedEvent(string adUnitId)
    {
        mtgLog("onRewardedVideoShownFailedEvent: " + adUnitId);
    }

    //广告展示成功的回调
    private void onRewardedVideoShownEvent()
    {
        mtgLog("onRewardedVideoShownEvent");
    }

    //广告被点击的回调
    private void onRewardedVideoClickedEvent(string errorMsg)
    {
        mtgLog("onRewardedVideoClickedEvent: " + errorMsg);
    }

    //视频播放完成的回调
    private void onRewardedVideoPlayCompleted()
    {
        mtgLog("onRewardedVideoPlayCompleted");
    }

    //endcard落地页展示成功的回调
    private void onRewardedVideoEndCardShowSuccess()
    {
        mtgLog("onRewardedVideoEndCardShowSuccess");
    }

    //广告关闭的回调，播放视频页面关闭以后，您需要判断MTGRewardData的对象的convert属性，来决定是否给用户奖励。    
    /*  private void onRewardedVideoClosedEvent(MintegralManager.MTGRewardData rewardData)
     {
         if (rewardData.converted)
             mtgLog("onRewardedVideoClosedEvent: " + rewardData);
         else
             mtgLog("onRewardedVideoClosedEvent: No Reward");
     } */

    #endregion
}