package com.unity3d.player;

import android.app.Activity;
import android.support.annotation.NonNull;
import android.util.DisplayMetrics;
import android.util.Log;
import android.util.TypedValue;
import android.view.Gravity;
import android.widget.FrameLayout;

import com.transsion.gamead.AdHelper;
import com.transsion.gamead.GameAdBannerListener;
import com.transsion.gamead.GameAdLoadListener;
import com.transsion.gamead.GameAdRewardShowListener;
import com.transsion.gamead.GameAdShowListener;
import com.transsion.gamead.GameRewardItem;

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
        Activity activity = unityActivity;
        AdHelper.loadInterstitial(activity, new GameAdLoadListener() {
            @Override
            public void onAdFailedToLoad(int code, String message) {
                Log.i("ADTest", "Interstitial onAdFailedToLoad " + code + " " + message);
            }

            @Override
            public void onAdLoaded() {
                Log.i("ADTest", "Interstitial onAdLoaded");
                interstitialLoaded = true;
            }
        });
    }

    public void ShowInterstitial() {
        if (interstitialLoaded) {
            AdHelper.showInterstitial(unityActivity, new GameAdShowListener() {
                @Override
                public void onShow() {
                    Log.i("ADTest", "Interstitial show");
                }

                @Override
                public void onClose() {
                    Log.i("ADTest", "Interstitial close");
                }

                @Override
                public void onShowFailed(int code, String message) {
                    Log.i("ADTest", "Interstitial show fail " + code + " " + message);
                }
            });
            interstitialLoaded = false;
        } else {
            LoadInterstitial();
        }
    }

    public void LoadReward() {
        Activity activity = unityActivity;

        AdHelper.loadReward(activity, new GameAdLoadListener() {
            @Override
            public void onAdFailedToLoad(int code, String message) {
                Log.i("ADTest", "Reward onRewardedAdFailedToLoad " + code + " " + message);
            }

            @Override
            public void onAdLoaded() {
                Log.i("ADTest", "Reward onRewardedAdLoaded");
                rewardLoaded = true;
            }
        });
    }

    public void ShowReward() {
        if (rewardLoaded) {

            AdHelper.showReward(unityActivity, new GameAdRewardShowListener() {
                @Override
                public void onShow() {
                    Log.i("ADTest", "Reward show");
                }

                @Override
                public void onClose() {
                    Log.i("ADTest", "Reward close");
                }

                @Override
                public void onShowFailed(int code, String message) {
                    Log.i("ADTest", "Reward show fail " + code + " " + message);
                    UnityPlayer.UnitySendMessage("PS", "RewardFail", "");
                }

                @Override
                public void onUserEarnedReward(GameRewardItem rewardItem) {
                    Log.i("ADTest", "Reward onUserEarnedReward " + rewardItem.getType() + " " +
                            rewardItem.getAmount());
                    UnityPlayer.UnitySendMessage("PS", "RewardSuccess", "");
                }
            });
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
        AdHelper.showBanner(self, layoutParams, new GameAdBannerListener() {
            @Override
            public void onAdFailedToLoad(int code, String message) {
                Log.i("ADTest", "Banner onAdFailedToLoad " + code + " " + message);
            }

            @Override
            public void onAdOpened() {
                Log.i("ADTest", "Banner onAdOpened");
            }

            @Override
            public void onAdImpression() {
                Log.i("ADTest", "Banner onAdImpression");
            }

            @Override
            public void onAdLoaded() {
                Log.i("ADTest", "Banner onAdLoaded");
            }

            @Override
            public void onAdClosed() {
                Log.i("ADTest", "Banner onAdClosed");
            }
        });

    }

    public void LoadFloat() {
        Activity activity = unityActivity;
        AdHelper.loadFloat(activity, new GameAdLoadListener() {
            @Override
            public void onAdFailedToLoad(int code, String message) {
                Log.i("ADTest", "Float onAdFailedToLoad " + code + " " + message);
            }

            @Override
            public void onAdLoaded() {
                Log.i("ADTest", "Float onAdLoaded");
            }
        });
    }

    public void SetFloatActive(boolean active) {
        Activity activity = unityActivity;
        if (active) {
            int width = FrameLayout.LayoutParams.WRAP_CONTENT;
            int height = FrameLayout.LayoutParams.WRAP_CONTENT;
            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(width, height);
            layoutParams.gravity = Gravity.BOTTOM;
            DisplayMetrics displayMetrics = activity.getResources().getDisplayMetrics();
            layoutParams.bottomMargin = (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP
                    , 100f, displayMetrics);
            int dp16 = (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 16f, displayMetrics);
            layoutParams.setMarginStart(dp16);
            layoutParams.setMarginEnd(dp16);
            AdHelper.showFloat(activity, layoutParams, new GameAdShowListener() {
                @Override
                public void onShow() {
                    Log.i("ADTest", "Float show");
                }

                @Override
                public void onClose() {
                    Log.i("ADTest", "Float close");
                }

                @Override
                public void onShowFailed(int code, String message) {
                    Log.i("ADTest", "Float show fail " + code + " " + message);
                }
            });
        } else {
            AdHelper.closeFloat(activity);
        }
    }
}
