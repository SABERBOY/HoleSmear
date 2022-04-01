package com.unity3d.player;

import android.app.Application;
import android.content.Context;
import android.util.Log;
import androidx.annotation.NonNull;
import androidx.multidex.MultiDex;

import com.transsion.core.CoreUtil;
import com.transsion.game.analytics.Constants;
import com.transsion.game.analytics.GameAnalytics;
import com.transsion.gamead.AdHelper;
import com.transsion.gamead.AdInitializer;
import com.transsion.gamead.GameAdLoadListener;
/* import com.transsion.gamepay.core.PayInitializer;
import com.transsion.gamepay.core.PayParams;
import com.transsion.gamepay.core.SupplementCallback;
import com.transsion.gamepay.core.bean.OrderInfo; */
import com.transsion.unityapi.AndroidLib;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.messaging.FirebaseMessaging;

import java.util.Collections;

public class UnityApp extends Application {
    private static final String TAG = "UnityApp";

    @Override
    protected void attachBaseContext(Context base) {
        super.attachBaseContext(base);
        MultiDex.install(this);
    }

    @Override
    public void onCreate() {
        super.onCreate();
        CoreUtil.init(this);
        AdInitializer.init(
                new AdInitializer.Builder(this)
                        // 开启debuggable，设置测试环境，展示测试广告
                        // 请在正式上线环境下，设为false,并且setEnv接口传参数为release
                        .setDebuggable(false).setEnv("release")
//         .setDebuggable(true).setEnv("test")
        // 开启debug，展示的是admob的测试服务器广告
        // 如果你需要调试线上真实广告，你需要把debuggable设为false
        // 并输入你的设备ID，如何获取设备ID见 第6点广告测试的问题QA
        // 请在正式上线的环境下，删除本行代码
        // .setTestDeviceIds(Collections.singletonList("E71EADD6F50D12BFA760EF996AC7ED5B"))
        // 广告开关，不传默认开启
        // 2022/02/18 20:38:56.407 4000 4000 Info Ads Use
        // RequestConfiguration.Builder().setTestDeviceIds(Arrays.asList("E71EADD6F50D12BFA760EF996AC7ED5B"))
        // to get test ads on this device.

        // .setTotalSwitch(true)
        );

        // 开屏广告预加载，如果需要展示开屏广告，必需在Application的初始化中调用广告的初始化，并同时预加载开屏广告
        // 如下第一个参数5，表示如果5秒内开屏广告准备好，则自动展示开屏广告，否则不会展示开屏广告,下次热启动会再次展示
        // 有闪屏或启动动画的游戏可以设置等待时间稍长，视闪屏和动画时长而定
        AdHelper.showAppOpen(5, new GameAdLoadListener() {

            @Override
            public void onAdLoaded() {
                Log.d(TAG, "onAdLoaded() called");
            }

            @Override
            public void onAdFailedToLoad(int code, String message) {
                Log.d(TAG, "onAdFailedToLoad() called with: code = [" + code + "], message = [" + message + "]");
            }
        });
        boolean debuggable = false;
        /* PayInitializer.init(
                new PayInitializer.Builder(this)
                        // sdk内部需要用到主线程 ，你可以传入你全局统一构建的Handler , //否则内部默认创建一个新的handler
                        // .setMainThreadHandler(handler)
                        // 如果你需要开启测试环境 ，测试环境影响服务器接口地址
                        // 请在正式上线环境下，设为false,并且setEnv接口传参数为release
                        // .setDebuggable(debuggable).setEnv("test")
                        .setDebuggable(false).setEnv("release")
                        // 如果你需要模拟某个国家的支付测试 ，
                        // 你需要开启Debug模式 ，并传入对应的MccMnc
                        // .setTestMccMnc("510")
                        // 订单补充回调
                        .setSupplementCallback(new SupplementCallback() {
                            @Override
                            public void reissueProduct(PayParams payParams, OrderInfo orderInfo) {
                                // 当用户付完钱后 ，没等回调结果就退出应用 ，那么当实际结果是扣款成功的时候 ，
                                // 游戏还未给用户发放道具或权益 ，在下次进入应用 ，检测到支付成功时 ，将通知游戏客户端 //在这里补发道具
                                // 但是 ，如果您有自己的权益发放管理 ，请先检测您是否已经发放过权益了
                            }

                            @Override
                            public void supplementOrderSuccess(PayParams payParams, OrderInfo orderInfo) {
                                // 短代支付 ，支付成功后 ，优先通知游戏发放道具 ，后续将自动验证这笔订单的扣款情况
                                // 如果是失败的情况 ，那么会提示用户进行补单 ，这里将告诉你补单情况的结果是成功 。
                                // 这里不需要再发放道具
                            }

                            @Override
                            public void supplementOrderFail(PayParams payParams, OrderInfo orderInfo, int errorCode) {
                                // 短代支付 ，支付成功后 ，优先通知游戏发放道具 ，后续将自动验证这笔订单的扣款情况
                                // 如果是失败的情况 ，那么会提示用户进行补单 ，这里将告诉你补单情况的结果是失败 。
                                // 这里不需要再发放道具
                            }
                        })); */
        // AdInitializer.init(new
        // AdInitializer.Builder(this).setDebuggable(debuggable));
        GameAnalytics.init(new GameAnalytics.Builder(this));
        // GameAnalytics.tracker(Constants.ACTION_LEVEL_END, "20", "0");
        FirebaseMessaging.getInstance().getToken()
                .addOnCompleteListener(new OnCompleteListener<String>() {
                    @Override
                    public void onComplete(@NonNull Task<String> task) {
                        if (!task.isSuccessful()) {
                            Log.w(TAG, "Fetching FCM registration token failed", task.getException());
                            return;
                        }

                        // Get new FCM registration token
                        String token = task.getResult();

                        // Log and toast
//                        String msg = getString(R.string.msg_token_fmt, token);
//                        Log.d(TAG, msg);
//                        Toast.makeText(MainActivity.this, msg, Toast.LENGTH_SHORT).show();
                    }
                });
    }
}
