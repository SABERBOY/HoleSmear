using System;

namespace Script.SDK
{
    public interface ISDK
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();

        /// <summary>
        /// 视频是否加载好
        /// </summary>
        /// <returns></returns>
        bool VideoLoaded();

        /// <summary>
        /// 插屏是否加载好
        /// </summary>
        /// <returns></returns>
        bool InterstitialLoaded();

        /// <summary>
        /// 显示激励视频
        /// </summary>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        void ShowVideo(Action success, Action fail);

        /// <summary>
        /// 显示插屏
        /// </summary>
        /// <param name="interactionAdCompleted"></param>
        /// <param name="hold"></param>
        void ShowInterstitialAd(Action interactionAdCompleted, Action hold);

        void OnApplicationPause(bool pause);

        void ShowBanner(bool show);

    }
}