using UnityEngine;


namespace SDK
{
    public class MTGSDKController
    {


        private string MTGSDKAppID = "121407";
        private string MTGSDKAppKey = "bbbccac24584842c57f2c638867d5c01";

        private MTGSDKRewardedVideo rewardedVideo;
        private MTGSDKInterstitial interstitial;

        public void Initialize()
        {
            //设置是否获取用户信息的开关,此方法需要在SDK初始化之前调用。
            //1为允许，0为拒绝
            Mintegral.setConsentStatusInfoType(0);
            Debug.Log("userPrivateInfo ConsentStatus : " + Mintegral.getConsentStatusInfoType());

            Mintegral.initMTGSDK(MTGSDKAppID, MTGSDKAppKey);


            rewardedVideo = new MTGSDKRewardedVideo();
            rewardedVideo.Initialize();

            interstitial = new MTGSDKInterstitial();
            interstitial.Initialize();
        }

        public bool IsRewardAdLoaded()
        {
            return rewardedVideo.IsRewardAdLoaded;
        }

        public bool IsInterstitialLoaded()
        {
            return interstitial.IsInterstitialLoaded;
        }


        public void ShowRewardVideoAd()
        {
            rewardedVideo.Show();
        }

        public void ShowInterstitial()
        {
            interstitial.Show();
        }
    }
}


