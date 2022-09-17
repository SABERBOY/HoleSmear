package com.unity3d.player;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.res.Configuration;
import android.os.Bundle;
import android.util.Log;
import android.view.ViewGroup;
import android.widget.FrameLayout;

import com.anythink.core.api.ATAdConst;
import com.anythink.core.api.ATAdInfo;
import com.anythink.core.api.ATMediationRequestInfo;
import com.anythink.core.api.ATNetworkConfirmInfo;
import com.anythink.core.api.AdError;
import com.anythink.splashad.api.ATSplashAd;
import com.anythink.splashad.api.ATSplashAdExtraInfo;
import com.anythink.splashad.api.ATSplashAdListener;
import com.anythink.splashad.api.ATSplashExListener;
import com.unity3d.player.R;
import com.unity3d.player.UnityPlayerActivity;

import java.util.HashMap;
import java.util.Map;

public class SplashAdShowActivity extends Activity implements ATSplashExListener {

    private static final String TAG = SplashAdShowActivity.class.getSimpleName();

    ATSplashAd splashAd;
    FrameLayout container;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.splash_ad_show);

        String placementId = UnityApp.mPlacementId_splash_all;//Placement ID
        container = findViewById(R.id.splash_ad_container);
        ViewGroup.LayoutParams layoutParams = container.getLayoutParams();
        Configuration cf = getResources().getConfiguration();

        int ori = cf.orientation;

        /**You should set size to the layout param.**/
        if (ori == Configuration.ORIENTATION_LANDSCAPE) {
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_SENSOR_LANDSCAPE);
            layoutParams.width = (int) (getResources().getDisplayMetrics().widthPixels * 0.9);
            layoutParams.height = getResources().getDisplayMetrics().heightPixels;
        } else if (ori == Configuration.ORIENTATION_PORTRAIT) {
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_SENSOR_PORTRAIT);
            layoutParams.width = getResources().getDisplayMetrics().widthPixels;
            layoutParams.height = (int) (getResources().getDisplayMetrics().heightPixels * 0.85);
        } else {
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_SENSOR_PORTRAIT);
            layoutParams.width = getResources().getDisplayMetrics().widthPixels;
            layoutParams.height = (int) (getResources().getDisplayMetrics().heightPixels * 0.85);
        }

        ATMediationRequestInfo atMediationRequestInfo = null;

//        atMediationRequestInfo = new MintegralATRequestInfo("100947", "ef13ef712aeb0f6eb3d698c4c08add96", "210169", "276803");
//        atMediationRequestInfo.setAdSourceId("71606");
//
//        atMediationRequestInfo = new GDTATRequestInfo("1101152570", "8863364436303842593");
//        atMediationRequestInfo.setAdSourceId("71602");
//
//        atMediationRequestInfo = new TTATRequestInfo("5020321", "820321537", false);
//        atMediationRequestInfo.setAdSourceId("71600");

//        atMediationRequestInfo = new SigmobiATRequestInfo("1282", "27531c7c64157934", "e04d1ac9231");
//        atMediationRequestInfo.setAdSourceId("71608");
//
//        atMediationRequestInfo = new BaiduATRequestInfo("e866cfb0", "2058622");
//        atMediationRequestInfo.setAdSourceId("71609");

//        atMediationRequestInfo  = new KSATRequestInfo("501400010", "5014000234");
//        atMediationRequestInfo.setAdSourceId("88377");

        String defaultConfig = "";

        //设置首次开屏广告广告源，请从TopOn后台兜底开屏广告源导出配置
        defaultConfig = "{\"unit_id\":2199529,\"ad_type\":-1,\"nw_firm_id\":2,\"adapter_class\":\"com.anythink.network.admob.AdmobATSplashAdapter\",\"content\":\"{\\\"unit_id\\\":\\\"ca-app-pub-2270136017335510\\\\\\/9965237627\\\",\\\"orientation\\\":\\\"1\\\",\\\"app_id\\\":\\\"ca-app-pub-2270136017335510~2820148306\\\"}\"}";
        Activity self = this;
        splashAd = new ATSplashAd(this, placementId, new ATSplashAdListener() {
            @Override
            public void onAdLoaded(boolean b) {
                splashAd.show(self, container);
            }

            @Override
            public void onAdLoadTimeout() {
                jumpToMainActivity();

            }

            @Override
            public void onNoAdError(AdError adError) {
                jumpToMainActivity();

            }

            @Override
            public void onAdShow(ATAdInfo atAdInfo) {

            }

            @Override
            public void onAdClick(ATAdInfo atAdInfo) {

            }

            @Override
            public void onAdDismiss(ATAdInfo atAdInfo, ATSplashAdExtraInfo atSplashAdExtraInfo) {
                jumpToMainActivity();
            }
        }, 5000, defaultConfig);

        Map<String, Object> localMap = new HashMap<>();
        localMap.put(ATAdConst.KEY.AD_WIDTH, layoutParams.width);
        localMap.put(ATAdConst.KEY.AD_HEIGHT, layoutParams.height);


        splashAd.setLocalExtra(localMap);

        if (splashAd.isAdReady()) {
            Log.i(TAG, "SplashAd is ready to show.");
            splashAd.show(this, container);
        } else {
            Log.i(TAG, "SplashAd isn't ready to show, start to request.");
            splashAd.loadAd();
        }


        ATSplashAd.checkSplashDefaultConfigList(this, placementId, null);
    }

    @Override
    public void onDeeplinkCallback(ATAdInfo adInfo, boolean isSuccess) {
        Log.i(TAG, "onDeeplinkCallback:" + adInfo.toString() + "--status:" + isSuccess);
    }

    @Override
    public void onDownloadConfirm(Context context, ATAdInfo atAdInfo, ATNetworkConfirmInfo atNetworkConfirmInfo) {

    }

    //    @Override
    public void onAdLoaded() {
        Log.i(TAG, "onAdLoaded---------");
        splashAd.show(this, container);
    }

    @Override
    public void onAdLoaded(boolean b) {
        Log.i(TAG, "onAdLoaded---------");
        splashAd.show(this, container);
    }

    @Override
    public void onAdLoadTimeout() {

    }

    @Override
    public void onNoAdError(AdError adError) {
        Log.i(TAG, "onNoAdError---------:" + adError.getFullErrorInfo());
        jumpToMainActivity();
    }

    @Override
    public void onAdShow(ATAdInfo entity) {
        Log.i(TAG, "onAdShow:\n" + entity.toString());
    }

    @Override
    public void onAdClick(ATAdInfo entity) {
        Log.i(TAG, "onAdClick:\n" + entity.toString());
    }

    @Override
    public void onAdDismiss(ATAdInfo atAdInfo, ATSplashAdExtraInfo atSplashAdExtraInfo) {
        Log.i(TAG, "onAdDismiss:\n" + atAdInfo.toString());
        jumpToMainActivity();
    }

    //    @Override
    public void onAdDismiss(ATAdInfo entity) {
        Log.i(TAG, "onAdDismiss:\n" + entity.toString());
        jumpToMainActivity();
    }

    boolean hasHandleJump = false;

    public void jumpToMainActivity() {
        if (!hasHandleJump) {
            hasHandleJump = true;

            Intent intent = new Intent(SplashAdShowActivity.this, UnityPlayerActivity.class);
            startActivity(intent);
        }

    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        if (splashAd != null) {
            splashAd.onDestory();
        }

    }

}
