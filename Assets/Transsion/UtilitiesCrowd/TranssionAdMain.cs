#if TRANSSIONAD
using System;
using UnityEngine;

namespace Transsion.UtilitiesCrowd
{
    interface ITranssionAd
    {
        bool IsLoaded { get; }
        bool CanShow { get; }
        void Show(Action successAction, Action failAction);
        void Show(int startMargin, int endMargin, int bottomMargin);
        void Hide();
    }

    public class TranssionAdMain
    {
        private static FloatAd _floatAd;
        private static InterstitialAd _interstitialAd;
        private static RewardAd _rewardAd;
        private static BannerAd _bannerAd;


        public static bool RewardIsLoaded => _rewardAd?.IsLoaded ?? false;
        public static bool InterstitialIsLoaded => _interstitialAd?.IsLoaded ?? false;

        public static void Init()
        {
            // Debug.Log("TranssionAdMain Init");
            // AdHelper.init("83C0EECDFE32F56622BF2A7B4C6A0AEF");
            AdHelper.init(null);
            LoadFloat();
            // LoadInterstitial();
            // LoadReward();
            // _bannerAd = new BannerAd();
            CrowdGameAnalytics.EventAppStart();
        }

        public static void LoadFloat()
        {
            // Debug.Log("LoadFloat");
            _floatAd = new FloatAd();
        }

        public static void ShowFloat(int startMargin, int endMargin, int bottomMargin)
        {
            _floatAd?.Show(startMargin, endMargin, bottomMargin);
        }

        public static void HideFloat()
        {
            _floatAd?.Hide();
        }

        public static void LoadReward()
        {
            _rewardAd = new RewardAd();
        }

        public static void ShowReward(Action success, Action fail)
        {
            _rewardAd?.Show(success, fail);
        }

        public static void LoadInterstitial()
        {
            _interstitialAd = new InterstitialAd();
        }

        public static void ShowInterstitial(Action success, Action fail)
        {
            _interstitialAd?.Show(success, fail);
        }

        public static void ShowBanner()
        {
            _bannerAd.Show(0, 0, 0);
        }

        public static void CloseBanner()
        {
            _bannerAd.Hide();
        }
    }

    public class FloatAd : LoadListener, ShowListener, ITranssionAd
    {
        public FloatAd()
        {
            // Debug.Log(" FloatAd load   ");
            this.IsLoaded = false;
            AdHelper.loadFloat(this);
        }

        public void onAdFailedToLoad(REASON reason, string message)
        {
            // Debug.Log($"{nameof(FloatAd)} onAdFailedToLoad:{reason},{message}");
        }

        public void onAdLoaded()
        {
            this.IsLoaded = true;
            // Debug.Log($"{nameof(FloatAd)} onAdLoaded!");
        }

        public void onShow()
        {
            // Debug.Log($"{nameof(FloatAd)} onShow!");
        }

        public void onClose()
        {
            // Debug.Log($"{nameof(FloatAd)} onClose!");
            this.IsLoaded = false;
        }

        public void onShowFailed(REASON reason, string message)
        {
            // Debug.Log($"{nameof(FloatAd)} onShowFailed:{reason},{message}");
            this.IsLoaded = false;
        }

        public bool IsLoaded { get; private set; }
        public bool CanShow => this.IsLoaded;

        public void Show(Action successAction, Action failAction)
        {
        }

        public void Show(int startMargin, int endMargin, int bottomMargin)
        {
            // if (this.CanShow)
            AdHelper.showFloat(this, startMargin, endMargin, bottomMargin);
        }

        public void Hide()
        {
            AdHelper.closeFloat();
        }
    }

    public class InterstitialAd : LoadListener, ShowListener, ITranssionAd
    {
        private Action _success;
        private Action _fail;

        public InterstitialAd()
        {
            this.IsLoaded = false;
            AdHelper.loadInterstitial(this);
        }

        public void onAdFailedToLoad(REASON reason, string message)
        {
            this.IsLoaded = false;
            // Debug.Log($"{nameof(InterstitialAd)} onAdFailedToLoad:{reason},{message}");
        }

        public void onAdLoaded()
        {
            this.IsLoaded = true;
            // Debug.Log($"{nameof(InterstitialAd)} onAdLoaded!");
        }

        public void onShow()
        {
            // Debug.Log($"{nameof(InterstitialAd)} onShow!");
        }

        public void onClose()
        {
            this.IsLoaded = false;
            // Debug.Log($"{nameof(InterstitialAd)} onClose!");
            this._success?.Invoke();
        }

        public void onShowFailed(REASON reason, string message)
        {
            this.IsLoaded = false;
            // Debug.Log($"{nameof(InterstitialAd)} onShowFailed:{reason},{message}");
            this._fail?.Invoke();
        }

        public bool IsLoaded { get; private set; }
        public bool CanShow => this.IsLoaded;

        public void Show(Action successAction, Action failAction)
        {
            if (!this.CanShow) return;
            this._success = successAction;
            this._fail = failAction;
            AdHelper.showInterstitial(this);
        }

        public void Show(int startMargin, int endMargin, int bottomMargin)
        {
        }

        public void Hide()
        {
        }
    }

    public class RewardAd : LoadListener, RewardShowListener, ITranssionAd
    {
        private Action _success;
        private Action _fail;

        public RewardAd()
        {
            this.IsLoaded = false;
            AdHelper.loadReward(this);
        }

        public void onAdFailedToLoad(REASON reason, string message)
        {
            this.IsLoaded = false;
            // Debug.Log($"{nameof(RewardAd)} onAdFailedToLoad:{reason},{message}");
        }

        public void onAdLoaded()
        {
            this.IsLoaded = true;
            // Debug.Log($"{nameof(RewardAd)} onAdLoaded!");
        }

        public void onShow()
        {
            // Debug.Log($"{nameof(RewardAd)} onShow!");
        }

        public void onClose()
        {
            // Debug.Log($"{nameof(RewardAd)} onClose!");
        }

        public void onShowFailed(REASON reason, string message)
        {
            this.IsLoaded = false;
            // Debug.Log($"{nameof(RewardAd)} onShowFailed:{reason},{message}");
            this._fail?.Invoke();
        }

        public void onUserEarnedReward(int amount, string type)
        {
            // Debug.Log($"{nameof(RewardAd)} onUserEarnedReward:{amount},{type}");
            this._success?.Invoke();
            this.IsLoaded = false;
        }

        public bool IsLoaded { get; private set; }
        public bool CanShow => this.IsLoaded;

        public void Show(Action successAction, Action failAction)
        {
            if (!this.CanShow) return;
            this._success = successAction;
            this._fail = failAction;
            AdHelper.showReward(this);
        }

        public void Show(int startMargin, int endMargin, int bottomMargin)
        {
        }

        public void Hide()
        {
        }
    }


    public class BannerAd : ITranssionAd, BannerListener
    {
        public bool IsLoaded { get; }
        public bool CanShow { get; }

        public void Show(Action successAction, Action failAction)
        {
        }

        public void Show(int startMargin, int endMargin, int bottomMargin)
        {
            AdHelper.showBanner(this, startMargin, endMargin, bottomMargin);
        }

        public void Hide()
        {
            AdHelper.closeBanner();
        }

        public void onAdFailedToLoad(REASON reason, string message)
        {
        }

        public void onAdLoaded()
        {
        }

        public void onAdClosed()
        {
        }

        public void onAdOpened()
        {
        }

        public void onAdImpression()
        {
        }
    }

    class TranssionGameAnalytics : IGameAnalytics
    {
        private static readonly string GameAnalyticsClass = "com.transsion.game.analytics.GameAnalytics";
        private static AndroidJavaClass _androidJavaClass;

        private static AndroidJavaClass AndroidJavaClass
        {
            get
            {
                if (_androidJavaClass == null)
                {
                    _androidJavaClass = new AndroidJavaClass(GameAnalyticsClass);
                }

                return _androidJavaClass;
            }
        }

        public void Track(params object[] args)
        {
#if UNITY_ANDROID
            if (args.Length >= 3)
            {
                AndroidJavaClass.CallStatic("tracker", args[0], args[1], args[2]);
            }
            else
            {
                switch (args.Length)
                {
                    case 2:
                        AndroidJavaClass.CallStatic("tracker", args[0], args[1], string.Empty);
                        break;
                    case 1:
                        AndroidJavaClass.CallStatic("tracker", args[0], string.Empty, string.Empty);
                        break;
                    default:
                        Debug.LogWarning($"args is empty!!");
                        break;
                }
            }
#endif
        }
    }

    class TranssionIapManager
    {
        private static readonly string UnityPayClass = "com.unity3d.player.UnityPay";
        private static readonly string UnityPlayerClass = "com.unity3d.player.UnityPlayer";

        public static void StartPurchase(string productId, Action success = null, Action fail = null)
        {
            // Debug.Log($"{productId}");
#if UNITY_EDITOR
            success?.Invoke();
#elif UNITY_ANDROID && !UNITY_EDITOR
            var unityPay = new AndroidJavaClass(UnityPayClass);
            var unityPlayer = new AndroidJavaClass(UnityPlayerClass);
            unityPay.CallStatic("GetPayConfig");
            unityPay.CallStatic("StartPurchase", unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"), productId, new AndroidJavaRunnable(success), new AndroidJavaRunnable(fail));
#endif
        }
    }
}
#endif