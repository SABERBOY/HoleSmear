using System;

public enum REASON
{
    //广告开关已关闭
    ERROR_AD_SWITCH_DISABLED = -4,

    //广告未准备好
    ERROR_CODE_NOT_READY = -3,

    //未初始化PlaceId
    ERROR_CODE_MISS_INIT_PLACE_ID = -2,

    //界面异常
    ERROR_CODE_VIEW_EXCEPTION = -1,

    //内部出现问题；例如，收到广告服务器的无效响应
    ERROR_CODE_INTERNAL_ERROR = 0,

    //广告请求无效；例如，广告单元 ID 不正确
    ERROR_CODE_INVALID_REQUEST = 1,

    //由于网络连接问题，广告请求失败
    ERROR_CODE_NETWORK_ERROR = 2,

    //广告请求成功，但由于缺少广告资源，未返回广告
    ERROR_CODE_NO_FILL = 3,
    ERROR_CODE_APP_ID_MISSING = 8,
    ERROR_CODE_MEDIATION_NO_FILL = 9
};

public interface LoadListener
{
    void onAdFailedToLoad(REASON reason, String message);

    void onAdLoaded();
}

public interface BannerListener : LoadListener
{
    void onAdClosed();

    void onAdOpened();

    void onAdImpression();
}

public interface ShowListener
{
    void onShow();
    void onClose();
    void onShowFailed(REASON reason, String message);
}

public interface RewardShowListener : ShowListener
{
    void onUserEarnedReward(int amount, String type);
}