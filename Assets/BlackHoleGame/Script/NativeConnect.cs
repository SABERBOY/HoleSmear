using System;
using System.Collections.Generic;
using DG.Tweening;
using SDK;
using UnityEngine;
#if UNITY_IOS
using System.Runtime.InteropServices; //引入此程序集可以调用IOS代码了
#endif

namespace BlackHoleGame.Script
{
    public class NativeConnect : Base
    {
        private void Start()
        {
        }

        #region 单例

        private static NativeConnect _connect;
        private bool inited = false;

        public static NativeConnect Connect
        {
            get
            {
                if (_connect == null)
                {
                    var go = new GameObject();
                    go.name = "NativeConnect";
                    _connect = go.AddComponent<NativeConnect>();
                    DontDestroyOnLoad(go);
                    // _connect.Init();
                }

                return _connect;
            }
        }

        private NativeConnect()
        {
        }

        #endregion

        #region 安卓类名

        /// <summary>
        ///     安卓sdk连接类名字
        /// </summary>
        private const string AndroidConnectClassName = "com.collapse.sdk.AdsManager";

        /// <summary>
        ///     安卓设备原生类
        /// </summary>
        private const string AndroidDeviceClassName = "com.topfun.androiddevicelib.UnityConnect";


        #region 初始化

        /// <summary>
        ///     初始化
        /// </summary>
        public void Init()
        {
            if (!inited)
            {
                inited = true;
                SdkSystem.Instance.Initialize();
            }
        }

        #endregion

        #endregion

        #region IOS函数

#if UNITY_IOS
	[DllImport("__Internal")]
	public static extern void setVibratorIOS();
#endif

        #endregion

        #region 事件回调

        /// <summary>
        ///     事件回调字典
        /// </summary>
        private readonly Dictionary<string, Action<string>> eventCallBackDic = new Dictionary<string, Action<string>>();

        /// <summary>
        ///     添加事件监听
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="callBack">事件回调</param>
        public void addListener(string key, Action<string> callBack)
        {
            if (eventCallBackDic.ContainsKey(key))
                eventCallBackDic[key] = callBack;
            else
                eventCallBackDic.Add(key, callBack);
        }

        /// <summary>
        ///     触发事件
        /// </summary>
        /// <param name="info">信息  模版：key|传递参数</param>
        public void InvokeEvent(string info)
        {
            var arr = info.Split('|');
            if (eventCallBackDic.ContainsKey(arr[0])) eventCallBackDic[arr[0]](arr.Length >= 2 ? arr[1] : "");
        }

        #endregion

        #region 参数获取

        #endregion

        #region 原生功能

        /// <summary>
        ///     显示Toast
        /// </summary>
        /// <param name="str">内容</param>
        public void showToast(string str)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		UnityCallAndroid.CallStaticFunction (AndroidDeviceClassName, "showToast", str);
#endif
        }

        /// <summary>
        ///     打印日志在Logcat
        /// </summary>
        /// <param name="str">内容.</param>
        public void debugLog(string str)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		UnityCallAndroid.CallStaticFunction (AndroidDeviceClassName, "debugLog", str);
#endif
        }

        /// <summary>
        ///     震动
        /// </summary>
        /// <param name="str">时间 毫秒</param>
        public void vibrate(string str)
        {
            //if (UserData.Manage.isShock)
            //{
            //	#if UNITY_ANDROID && !UNITY_EDITOR
            //	UnityCallAndroid.CallStaticFunction (AndroidDeviceClassName, "vibrate", str);
            //	#elif UNITY_IOS
            //	setVibratorIOS();
            //	#endif
            //}
        }

        #endregion

        #region SDK事件

        #endregion

        #region 震动

        public void Shock()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			UnityCallAndroid.CallStaticFunction (AndroidDeviceClassName, "vibrate", "30");
#elif UNITY_IOS
			setVibratorIOS();
#endif
        }

        #endregion

        #region 支付去广告

        public void Pay()
        {
            //#if TEST

            //#elif UNITY_ANDROID && !UNITY_EDITOR
            //    UnityCallAndroid.CallStaticFunction (AndroidConnectClassName, "pay", "");
            //#endif
        }

        #endregion

        #region 广告

        public void showBanner(bool active = true)
        {
            SdkSystem.Instance.ShowBanner(active);
        }

        /// <summary>
        ///     插屏状态
        /// </summary>
        public bool InterstitialState
        {
            get
            {
#if UNITY_EDITOR
                return true;
#elif UNITY_ANDROID && USE_SDK && !UNITY_EDITOR
		 return  SdkSystem.Instance.IsInterstitialLoaded();
#else
            return SdkSystem.Instance.IsInterstitialLoaded();;
#endif
            }
        }


        /// <summary>
        ///     显示插屏
        /// </summary>
        /// <param name="callBackBlock">插屏回调</param>
        public void showBlock(Action<string> callBackBlock)
        {
            if (callBackBlock != null) addListener("Block", callBackBlock);

#if UNITY_EDITOR
            InvokeEvent("Block");
#elif UNITY_ANDROID && USE_SDK && !UNITY_EDITOR
        InvokeEvent("Block");
		// SdkSystem.Instance.ShowInterstitial();
        SdkSystem.Instance.ShowInterstitial((() => callBackBlock(String.Empty)), (() => callBackBlock(String.Empty)));
#else
        InvokeEvent("Block");
#endif
        }

        /// <summary>
        ///     激励视频状态
        /// </summary>
        public bool VideoState
        {
            get
            {
#if UNITY_EDITOR
                return true;
#elif UNITY_ANDROID && USE_SDK && !UNITY_EDITOR
		  return SdkSystem.Instance.IsRewardAdLoaded();
#else
            return SdkSystem.Instance.IsRewardAdLoaded();;
#endif
            }
        }

        /// <summary>
        ///     显示激励视频广告
        /// </summary>
        /// <param name="callBackVideo">广告回调</param>
        public void showVideo(string str, Action<string> callBackVideo)
        {
            if (callBackVideo != null) addListener("Video", callBackVideo);
#if UNITY_EDITOR
            InvokeEvent("Video|True");
#elif UNITY_ANDROID && USE_SDK && !UNITY_EDITOR
        SdkSystem.Instance.ShowRewardVideoAd((() => callBackVideo("True")), (() => callBackVideo("False")));
        // SdkSystem.Instance.ShowInterstitial((() => callBackVideo("True")), (() => callBackVideo("False")));
        // SdkSystem.Instance.ShowInterstitial((() => callBackBlock(String.Empty)), (() => callBackBlock(String.Empty)));
#else
        InvokeEvent("Video|True");
#endif
        }

        #endregion

        public void postEvent(string category, string action, string label)
        {
#if UNITY_ANDROID && NOX_SDK && !UNITY_EDITOR
#endif
        }

        private void OnApplicationPause(bool focus)
        {
        }

        private bool bo;

        /// <summary>
        ///     视频显示失败动画
        /// </summary>
        public void ShowVideoHints(string str)
        {
            if (bo) return;
            bo = true;
            anim.videoHintsText.enabled = true;
            Tweener a = anim.videoHintsText.transform.DOScale(Vector3.zero, 3f);
            a.SetEase(Ease.InExpo);
            a.OnComplete(delegate
            {
                anim.videoHintsText.enabled = false;
                anim.videoHintsText.transform.localScale = Vector3.one;
                bo = false;
            });
        }

        public void ShowFloatingWindow(bool show, RectTransform rectTrans = null)
        {
            var adPos = Vector2.zero;
            if (rectTrans)
            {
                var cam = rectTrans.root.GetComponentInChildren<Canvas>().worldCamera;
                var corners = new Vector3[4];
                rectTrans.GetLocalCorners(corners);
                var left_top =
                    RectTransformUtility.WorldToScreenPoint(cam,
                        rectTrans.localToWorldMatrix.MultiplyPoint(corners[1]));
                var right_bottom =
                    RectTransformUtility.WorldToScreenPoint(cam,
                        rectTrans.localToWorldMatrix.MultiplyPoint(corners[3]));
                var adSize = new Vector2(right_bottom.x - left_top.x, left_top.y - right_bottom.y);
                adPos = left_top;
            }

            var hight = 400;
            if (adPos.y != 0) hight = (int)(Screen.height - adPos.y);
            SdkSystem.Instance.ShowFloatingWindow(show, hight);
        }
    }
}