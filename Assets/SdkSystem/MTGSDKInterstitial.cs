using UnityEngine;


namespace SDK
{
    public class MTGSDKInterstitial
    {

        private string m_adUnitId = "171504";


        public void Initialize()
        {

            MTGInterstitialVideoInfo[] interstitialVideoAdInfos = new MTGInterstitialVideoInfo[1];
            MTGInterstitialVideoInfo info;

            info.adUnitId = m_adUnitId;
            interstitialVideoAdInfos[0] = info;
            Mintegral.loadInterstitialVideoPluginsForAdUnits(interstitialVideoAdInfos);//载入InterstitialVideo类


            MintegralManager.onInterstitialVideoLoadSuccessEvent += onInterstitialVideoLoadSuccessEvent;
            MintegralManager.onInterstitialVideoLoadedEvent += onInterstitialVideoLoadedEvent;
            MintegralManager.onInterstitialVideoFailedEvent += onInterstitialVideoFailedEvent;
            MintegralManager.onInterstitialVideoShownFailedEvent += onInterstitialVideoShownFailedEvent;
            MintegralManager.onInterstitialVideoShownEvent += onInterstitialVideoShownEvent;

            Request();
        }


        private void Request()
        {
            Mintegral.requestInterstitialVideoAd(m_adUnitId);

        }

        public void Show()
        {
            //展示Interstitial Video
            Mintegral.showInterstitialVideoAd(m_adUnitId);

        }


        public bool IsInterstitialLoaded
        {
            get
            {
                return Mintegral.isVideoReadyToPlay(m_adUnitId);
            }
        }


        //广告加载成功的回调
        void onInterstitialVideoLoadSuccessEvent(string adUnitId)
        {
            Debug.Log("onInterstitialVideoLoadSuccessEvent: " + adUnitId);
        }
        //广告缓存完成的回调
        void onInterstitialVideoLoadedEvent(string adUnitId)
        {
            Debug.Log("onInterstitialVideoLoadedEvent: " + adUnitId);
        }
        //广告加载失败的回调
        void onInterstitialVideoFailedEvent(string errorMsg)
        {
            Debug.Log("onInterstitialVideoFailedEvent: " + errorMsg);
        }
        //展示成功的回调
        void onInterstitialVideoShownEvent(string errorMsg)
        {
            Debug.Log("onInterstitialVideoShownEvent: " + errorMsg);
        }
        //展示失败的回调
        void onInterstitialVideoShownFailedEvent(string adUnitId)
        {
            Debug.Log("onInterstitialVideoShownFailedEvent: " + adUnitId);
        }
        //广告被点击的回调
        void onInterstitialVideoClickedEvent(string adUnitId)
        {
            Debug.Log("onInterstitialVideoClickedEvent: " + adUnitId);
        }
        //插屏视频播放完成的回调
        void onInterstitialVideoPlayCompleted()
        {
            Debug.Log("onInterstitialVideoPlayCompleted");
        }
        //endcard落地页展示成功的回调
        void onInterstitialVideoEndCardShowSuccess()
        {
            Debug.Log("onInterstitialVideoEndCardShowSuccess");
        }
        //广告关闭的回调   
        void onInterstitialVideoDismissedEvent(string errorMsg)
        {
            Debug.Log("onInterstitialVideoDismissedEvent: " + errorMsg);

            Request();
        }
    }
}


