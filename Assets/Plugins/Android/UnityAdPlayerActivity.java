package com.unity3d.player;

import android.app.Activity;
import android.support.annotation.NonNull;
import android.view.Gravity;
import android.widget.FrameLayout;

import com.transsion.gamead.AdHelper;
import com.transsion.gamead.GameAdListener;
import com.transsion.gamead.GameRewardItem;
import com.transsion.gamead.GameRewardedAdCallback;
import com.transsion.gamead.GameRewardedAdLoadCallback;

class UnityAdPlayerActivity {
    private Activity unityActivity = null;

    public void Init(Activity activity) {
        unityActivity = activity;
    }

    public boolean isInterstitialLoaded() {
        return interstitialLoaded;
    }

    public boolean isRewardLoaded() {
        return rewardLoaded;
    }

    private boolean interstitialLoaded = false;
    private boolean rewardLoaded = false;

    public void LoadInterstitial() {
        Activity self = unityActivity;
        AdHelper.loadInterstitial(unityActivity, new GameAdListener() {
            @Override
            public void onAdLoaded() {
                super.onAdLoaded();
                interstitialLoaded = true;
//                AdHelper.showInterstitial(self);
            }

            @Override
            public void onAdFailedToLoad(int i) {
                super.onAdFailedToLoad(i);
            }
            //您可以点击进入GameAdListener类，查看更多您需要的回调，并重写对应的方法
        });
    }

    public void ShowInterstitial() {
        if (interstitialLoaded) {
            AdHelper.showInterstitial(unityActivity);
            interstitialLoaded = false;
        } else {
            LoadInterstitial();
        }
    }

    public void LoadReward() {
        Activity self = unityActivity;

        AdHelper.loadReward(unityActivity, new GameRewardedAdLoadCallback() {

            @Override
            public void onRewardedAdLoaded() {
                super.onRewardedAdLoaded();
                rewardLoaded = true;
//                AdHelper.showReward(self);
            }

            @Override
            public void onRewardedAdFailedToLoad(int i) {
                super.onRewardedAdFailedToLoad(i);
            }

        }, new GameRewardedAdCallback() {

            public void onRewardedAdOpened() {
                //激励广告被打开
            }

            public void onRewardedAdClosed() {
                //激励广告被关闭
            }

            public void onUserEarnedReward(@NonNull GameRewardItem gameRewardItem) {
                //激励广告播放完成
                UnityPlayer.UnitySendMessage("PS", "RewardSuccess", "");
            }

            public void onRewardedAdFailedToShow(int reason) {
                //激励广告展示失败
                UnityPlayer.UnitySendMessage("PS", "RewardFail", "");
            }
        });
    }

    public void ShowReward() {
        if (rewardLoaded) {
            AdHelper.showReward(unityActivity);
            rewardLoaded = false;
        } else {
            LoadReward();
        }
    }

    public void ShowBanner() {
        Activity self = unityActivity;

        //这里设置广告宽度，可以为match_parent或者一个固定值，但是不能为0或者wrap_content，否则广告无法出来
        int width = FrameLayout.LayoutParams.MATCH_PARENT;
        //广告高度由宽度进行自适应
        int height = FrameLayout.LayoutParams.WRAP_CONTENT;
        FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(width, height);
        //从这里开始设置你的广告放置位置
        layoutParams.gravity = Gravity.BOTTOM;
//        layoutParams.bottomMargin = 400;
        layoutParams.setMarginStart(32);
        layoutParams.setMarginEnd(32);
        AdHelper.showBanner(unityActivity, layoutParams, new GameAdListener() {
            @Override
            public void onAdLoaded() {
                super.onAdLoaded();
                AdHelper.showBanner(self);

            }

            @Override
            public void onAdFailedToLoad(int i) {
                super.onAdFailedToLoad(i);
            }
            //您可以点击进入GameAdListener类，查看更多您需要的回调，并重写对应的方法
        });
    }
}
