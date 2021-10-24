package com.unity3d.player;


import android.app.Application;
//import android.support.multidex.BuildConfig;

import com.transsion.game.analytics.GameAnalytics;
import com.transsion.gamead.AdInitializer;

import java.util.Collections;

public class UnityApplication extends Application {
    @Override
    public void onCreate() {
        super.onCreate();
        GameAnalytics.init(new GameAnalytics.Builder(this)
                .setTest(false)
        );
        AdInitializer.Builder builder =//appKey是我们提供给您的一串数值
                new AdInitializer.Builder(this)
                        //开启debuggable 展示测试广告
                        //请在正式上线环境下，设为false
                        .setDebuggable(false);
                        //开启debug，展示的是admob的测试服务器广告
                        //如果你需要调试线上真实广告，你需要把debuggable设为false
                        //并输入你的设备ID，如何获取设备ID见 第6点广告测试的问题QA
                        //请在正式上线的环境下，删除本行代码
//                        .setTestDeviceIds(Collections.singletonList("83C0EECDFE32F56622BF2A7B4C6A0AEF"))
                        //sdk内部需要用到子线程，你可以传入你全局统一构建的线程池，
                        //否则内部默认使用AsyncTask.THREAD_POOL_EXECUTOR
                        //.setExecutor(executor)
                        //sdk内部需要用到主线程，你可以传入你全局统一构建的Handler,
                        //否则内部默认创建一个新的handler
                        //.setMainThreadHandler(handler)
                        //广告开关，不传默认开启
//                        .setTotalSwitch(true);
//        builder.setTestDeviceIds(Collections.singletonList("83C0EECDFE32F56622BF2A7B4C6A0AEF"));
        AdInitializer.init(builder);
    }
}
