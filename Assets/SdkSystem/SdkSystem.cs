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


        private SdkSystem() { }


        private MTGSDKController tGSDKController;
        private FireBaseContorl fireBaseContorl;


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {

            fireBaseContorl = new FireBaseContorl();
            fireBaseContorl.Initialize();

            tGSDKController = new MTGSDKController();
            tGSDKController.Initialize();
        }


        /// <summary>
        /// 激励视频是否加载成功
        /// </summary>
        /// <returns></returns>
        public bool IsRewardAdLoaded()
        {
            return tGSDKController.IsRewardAdLoaded();
        }


        /// <summary>
        /// 插屏是否加载成功
        /// </summary>
        /// <returns></returns>
        public bool IsInterstitialLoaded()
        {
            return tGSDKController.IsInterstitialLoaded();
        }


        /// <summary>
        /// 播放激励视频
        /// </summary>
        public void ShowRewardVideoAd()
        {
            tGSDKController.ShowRewardVideoAd();
        }


        /// <summary>
        /// 播放插屏广告
        /// </summary>
        public void ShowInterstitial()
        {
            tGSDKController.ShowRewardVideoAd();
        }

    }
}


