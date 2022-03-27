using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AdHelper
{
    private static AdHelper _instance;
    private static AndroidJavaObject _androidObject;

    //left 3  right 5  center 1
    public enum Horizon
    {
        HORIZ_MODEL_LEFT = 3,
        HORIZ_MODEL_RIGHT = 5,
        HORIZ_MODEL_CENTER = 1
    }

    public enum AdType
    {
        AD_BANNER = 1,
        AD_INTERSTITIAL = 2,
        AD_REWARD = 3,
        AD_FLOAT = 4,
    }

    public LoadListener[] loadListeners = new LoadListener[4];
    public ShowListener[] showListeners = new ShowListener[4];

    public static AdHelper getInstance()
    {
        if (_instance == null)
        {
            _instance = new AdHelper();
        }

        return _instance;
    }

    private AdHelper()
    {
    }

    /**
     * 尽量早的在游戏脚本的Start()函数里面调用初始化函数
     * 参数说明：
     * deviceId： 设备ID，当传入的设备ID不为空，则请求的是测试广告，用于调试。当游戏上线，设备ID传空，则请求正式广告
     */
    public static void init(String deviceId)
    {
        AdHelper adHelper = AdHelper.getInstance();
        adHelper.InitAndroidBridge(deviceId);
    }
    private void InitAndroidBridge(String deviceId)
    {
        if (_androidObject == null)
        {
            _androidObject = new AndroidJavaObject("com.transsion.unityapi.AndroidLib");

            //这里的init是在Android端定义的init方法
            _androidObject.Call("init",
                deviceId //**设置设备id，则请求测试广告数据**
              );

            GameObject BuildModeController = new GameObject("PayAdObject");
            BuildModeController.AddComponent<AndroidLibCallback>();
        }
    }

    // public void TestInAndroid()
    // {
    //     Debug.Log("TestInAndroid");
    //     _androidObject.Call("testInAndroid", "This is a Unity Content!");
    // }
    //
    public static void TestCallAndroid2Unity()
    {
        Debug.Log("TestCallAndroid2Unity");
        _androidObject.Call("testCallback");
    }
    //
    // public void CallSetVibrator()
    // {
    //     _androidObject.Call("testSetVibrator");
    // }


    /**
     * 显示横幅广告
     * loadListener 广告加载过程中的侦听
     * displayListener 广告显示过程的侦听
     * startMargin 广告展示布局的左边空隙
     * endMargin 广告展示布局的右边空隙
     * bottomMargin 广告展示布局的底部空隙
     * horizMode 广告展示布局的横向对齐模式 
     */
    public static void showBanner(BannerListener bannerListener, int startMargin, int endMargin, int bottomMargin, Horizon horizMode = Horizon.HORIZ_MODEL_CENTER)
    {
        _instance.loadListeners[(int)AdType.AD_BANNER - 1] = bannerListener;
        _androidObject.Call("showBanner", startMargin, endMargin, bottomMargin, (int)horizMode);
    }   
    
    /**
     * 关闭横幅广告
     */
    public static void closeBanner()
    {
        _androidObject.Call("closeBanner");
    }

    /**
     * 显示插屏广告
     * listener 广告显示过程的侦听
     * showWhenLoaded 仅在广告预加载完成时展示广告，如果未加载完成，则跳过此次展示时机
     */
    public static void showInterstitial(ShowListener listener, bool showWhenLoaded = true)
    {
        _instance.showListeners[(int)AdType.AD_INTERSTITIAL - 1] = listener;
        _androidObject.Call("showInterstitial", showWhenLoaded);
    }

    /**
     * 显示激励视频广告
     * listener 广告显示过程的侦听
     * showWhenLoaded 仅在广告预加载完成时展示广告，如果未加载完成，则跳过此次展示时机
     */
    public static void showReward(RewardShowListener listener, bool showWhenLoaded = true)
    {
        _instance.showListeners[(int)AdType.AD_REWARD - 1] = listener;
        _androidObject.Call("showReward", showWhenLoaded);
    }

    /**
     * 显示浮窗广告
     * displayListener 广告显示过程的侦听
     * startMargin 广告展示布局的左边空隙
     * endMargin 广告展示布局的右边空隙
     * bottomMargin 广告展示布局的底部空隙
     * horizMode 广告展示布局的横向对齐模式
     * showWhenLoaded 仅在广告预加载完成时展示广告，如果未加载完成，则跳过此次展示时机
     */
    public static void showFloat(ShowListener listener, int startMargin, int endMargin, int bottomMargin, Horizon horizMode = Horizon.HORIZ_MODEL_CENTER, bool showWhenLoaded = true)
    {
        _instance.showListeners[(int)AdType.AD_FLOAT - 1] = listener;
        _androidObject.Call("showFloat", startMargin, endMargin, bottomMargin, (int)horizMode, showWhenLoaded);
    }

    /**
     * 关闭浮窗广告
     */
    public static void closeFloat()
    {
        _androidObject.Call("closeFloat");
    }    
    
    public static void finish()
    {
        _androidObject.Call("finish");
    }

    /**
     * 预加载浮窗广告
     *listener 预加载过程中一些状态或者结果的回调
     */
    public static void loadFloat(LoadListener listener)
    {    
        _androidObject.Call("loadAd", (int)AdType.AD_FLOAT);
        _instance.loadListeners[(int)AdType.AD_FLOAT - 1] = listener;
    }

    /**
     * 预加载插屏广告
     *listener 预加载过程中一些状态或者结果的回调
     */
    public static void loadInterstitial(LoadListener listener)
    {
        _androidObject.Call("loadAd", (int)AdType.AD_INTERSTITIAL);
        _instance.loadListeners[(int)AdType.AD_INTERSTITIAL - 1] = listener;
    }

    /**
     * 预加载激励视频广告
     *listener 预加载过程中一些状态或者结果的回调
     */
    public static void loadReward(LoadListener listener)
    {
        _androidObject.Call("loadAd", (int)AdType.AD_REWARD);
        _instance.loadListeners[(int)AdType.AD_REWARD - 1] = listener;
    }
}
