package com.unity3d.player;

import android.app.Application;
import android.content.Context;
import android.os.Build;
import android.util.Log;
import android.webkit.WebView;

import androidx.annotation.NonNull;

import com.anythink.core.api.ATSDK;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.messaging.FirebaseMessaging;
import com.transsion.core.CoreUtil;
import com.transsion.game.analytics.GameAnalytics;
import com.transsion.gamead.AdInitializer;

public class UnityApp extends Application {
    private static final String TAG = "UnityApp";

    @Override
    protected void attachBaseContext(Context base) {
        super.attachBaseContext(base);
//        MultiDex.install(this);
    }

    public static final String appid = "a625ea9d410d16";
    public static final String appKey = "b1b44444ff8e3638c963fd14133a642d";

    //Splash
    public static final String mPlacementId_splash_all = "b63217657e53dd";

    @Override
    public void onCreate() {
        super.onCreate();
        Application app = this;
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
                    }
                });
        //Android 9 or above must be set
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
            String processName = getProcessName();
            if (!getPackageName().equals(processName)) {
                WebView.setDataDirectorySuffix(processName);
            }
        }
        ATSDK.setNetworkLogDebug(true);
        ATSDK.integrationChecking(getApplicationContext());
        ATSDK.init(this, appid, appKey);

        CoreUtil.init(this);
        GameAnalytics.init(new GameAnalytics.Builder(this));
        AdInitializer.init(
                new AdInitializer.Builder(this)
                        //开启debuggable，设置测试环境，展示测试广告
                        //请在正式上线环境下，设为false,并且setEnv接口传参数为release
                        .setDebuggable(false).setEnv("release")
//                         .setDebuggable(true).setEnv("test")
                //开启debug，展示的是admob的测试服务器广告
                //如果你需要调试线上真实广告，你需要把debuggable设为false
                //并输入你的设备ID，如何获取设备ID见 第6点广告测试的问题QA
                //请在正式上线的环境下，删除本行代码
//                        .setTestDeviceIds(Collections.singletonList("7FC2C0BE39C47406C984C08C16418C5C"))
                //广告开关，不传默认开启
//                        .setTotalSwitch(true)
        );
    }
}
