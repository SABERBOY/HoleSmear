using UnityEngine;


namespace SDK
{
    public class MTGSDKRewardedVideo
    {


        private string m_adUnitId = "171500";


        public void Initialize()
        {

            string[] rewardVideoUnits = new string[1] { m_adUnitId };
            /*  Mintegral.loadRewardedVideoPluginsForAdUnits(rewardVideoUnits);

             MintegralManager.onRewardedVideoLoadSuccessEvent += onRewardedVideoLoadSuccessEvent;
             MintegralManager.onRewardedVideoLoadedEvent += onRewardedVideoLoadedEvent;
             MintegralManager.onRewardedVideoFailedEvent += onRewardedVideoFailedEvent;
             MintegralManager.onRewardedVideoShownFailedEvent += onRewardedVideoShownFailedEvent;
             MintegralManager.onRewardedVideoShownEvent += onRewardedVideoShownEvent;
             MintegralManager.onRewardedVideoClickedEvent += onRewardedVideoClickedEvent;
             MintegralManager.onRewardedVideoClosedEvent += onRewardedVideoClosedEvent;
             MintegralManager.onRewardedVideoPlayCompletedEvent += onRewardedVideoPlayCompletedEvent; */

            Request();
        }


        private void Request()
        {
            Debug.Log("请求激励视频广告: " + m_adUnitId);
            /* Mintegral.requestRewardedVideo(m_adUnitId); */
        }


        public void Show()
        {
            /* Mintegral.showRewardedVideo(m_adUnitId); */
        }


        public bool IsRewardAdLoaded
        {
            get
            {
                return false; /* Mintegral.isVideoReadyToPlay(m_adUnitId); */
            }
        }


        /// <summary>
        /// 广告加载成功的回调
        /// </summary>
        /// <param name="adUnitId"></param>
        void onRewardedVideoLoadSuccessEvent(string adUnitId)
        {
            Debug.Log("onRewardedVideoLoadSuccessEvent: " + adUnitId);
        }


        /// <summary>
        /// 广告缓存完成的回调
        /// </summary>
        /// <param name="adUnitId"></param>
        void onRewardedVideoLoadedEvent(string adUnitId)
        {
            Debug.Log("onRewardedVideoLoadedEvent: " + adUnitId);
        }

        /// <summary>
        /// 广告加载失败的回调
        /// </summary>
        /// <param name="errorMsg"></param>
        void onRewardedVideoFailedEvent(string errorMsg)
        {
            Debug.Log("onRewardedVideoFailedEvent: " + errorMsg);
        }

        /// <summary>
        /// 广告展示失败的回调
        /// </summary>
        /// <param name="adUnitId"></param>
        void onRewardedVideoShownFailedEvent(string adUnitId)
        {
            Debug.Log("onRewardedVideoShownFailedEvent: " + adUnitId);
        }

        /// <summary>
        /// 广告展示成功的回调
        /// </summary>
        void onRewardedVideoShownEvent()
        {
            Debug.Log("onRewardedVideoShownEvent");
        }

        /// <summary>
        /// 广告被点击的回调
        /// </summary>
        /// <param name="errorMsg"></param>
        void onRewardedVideoClickedEvent(string errorMsg)
        {
            Debug.Log("onRewardedVideoClickedEvent: " + errorMsg);
        }


        /// <summary>
        /// 广告关闭的回调，播放视频页面关闭以后，您需要判断MTGRewardData的对象的convert属性，来决定是否给用户奖励
        /// </summary>
        /// <param name="rewardData"></param>
        /* void onRewardedVideoClosedEvent(MintegralManager.MTGRewardData rewardData)
        {
            if (rewardData.converted)
            {
                NativeConnect.Connect.InvokeEvent("Video|True");
                Debug.Log("onRewardedVideoClosedEvent: " + rewardData.ToString());
            }
            else
            {
                NativeConnect.Connect.InvokeEvent("Video|False");
                Debug.Log("onRewardedVideoClosedEvent: No Reward");
            }

            Request();
        } */


        /// <summary>
        /// 视频播放完成的回调
        /// </summary>
        /// <param name="adUnitId"></param>
        void onRewardedVideoPlayCompletedEvent(string adUnitId)
        {
            Debug.Log("onRewardedVideoPlayCompletedEvent: " + adUnitId);
        }


        /// <summary>
        /// endcard落地页展示成功的回调
        /// </summary>
        /// <param name="adUnitId"></param>
        void onRewardedVideoEndCardShowSuccessEvent(string adUnitId)
        {
            Debug.Log("onRewardedVideoEndCardShowSuccessEvent: " + adUnitId);
        }

    }
}


