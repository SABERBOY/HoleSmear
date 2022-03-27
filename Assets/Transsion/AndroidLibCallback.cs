using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AdHelper;

public class AndroidLibCallback : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    //banner
    public void PayCallback(params string[] args)
    {
        Debug.Log("PayCallback " + args[0]);
    }

    //other load listener
    public void onAdClosed(params string[] args)
    {
        int type = int.Parse(args[0]);
        Debug.Log("onAdClosed " + (AdHelper.AdType)type);

        if (type == (int)AdType.AD_BANNER && AdHelper.getInstance().loadListeners[type - 1] != null)
        {
            BannerListener bannerListener = (BannerListener)AdHelper.getInstance().loadListeners[type - 1];
            bannerListener.onAdClosed();
        }
    }

    public void onAdOpened(params string[] args)
    {
        int type = int.Parse(args[0]);
        Debug.Log("onAdOpened " + (AdHelper.AdType)type);


        if (type == (int)AdType.AD_BANNER && AdHelper.getInstance().loadListeners[type - 1] != null)
        {
            BannerListener bannerListener = (BannerListener)AdHelper.getInstance().loadListeners[type - 1];
            bannerListener.onAdOpened();
        }
    }

    public void onAdImpression(params string[] args)
    {
        int type = int.Parse(args[0]);
        Debug.Log("onAdImpression " + (AdHelper.AdType)type);

        if (type == (int)AdType.AD_BANNER && AdHelper.getInstance().loadListeners[type - 1] != null)
        {
            BannerListener bannerListener = (BannerListener)AdHelper.getInstance().loadListeners[type - 1];
            bannerListener.onAdImpression();
        }
    }

    //load
    public void onAdLoaded(params string[] args)
    {
        int type = int.Parse(args[0]);
        Debug.Log("onAdLoaded " + (AdHelper.AdType)type);

        if (AdHelper.getInstance().loadListeners[type - 1] != null)
        {
            AdHelper.getInstance().loadListeners[type - 1].onAdLoaded();
        }
    }

    public void onAdFailedToLoad(params string[] args)
    {
        String[] subStrings = args[0].Split('|');
        int type = int.Parse(subStrings[0]);
        Debug.Log("onAdFailedToLoad " + (AdHelper.AdType)type + " " + int.Parse(subStrings[1]) + " " + subStrings[2]);

        if (AdHelper.getInstance().loadListeners[type - 1] != null)
        {
            AdHelper.getInstance().loadListeners[type - 1].onAdFailedToLoad((REASON)int.Parse(subStrings[1]), subStrings[2]);
        }
    }

    //show
    public void onShow(params string[] args)
    {
        int type = int.Parse(args[0]);
        Debug.Log("onShow " + (AdHelper.AdType)type);

        if (AdHelper.getInstance().showListeners[type - 1] != null)
        {
            AdHelper.getInstance().showListeners[type - 1].onShow();
        }
    }

    public void onClose(params string[] args)
    {
        int type = int.Parse(args[0]);
        Debug.Log("onClose " + (AdHelper.AdType)type);

        if (AdHelper.getInstance().showListeners[type - 1] != null)
        {
            AdHelper.getInstance().showListeners[type - 1].onClose();
        }
    }

    public void onShowFailed(params string[] args)
    {
        String[] subStrings = args[0].Split('|');
        int type = int.Parse(subStrings[0]);
        Debug.Log("onShowFailed " + (AdHelper.AdType)type + " " + (REASON)int.Parse(subStrings[1]) + " " + subStrings[2]);


        if (AdHelper.getInstance().showListeners[type - 1] != null)
        {
            AdHelper.getInstance().showListeners[type - 1].onShowFailed((REASON)int.Parse(subStrings[1]), subStrings[2]);
        }
    }

    //reward show
    public void onUserEarnedReward(params string[] args)
    {
        String[] subStrings = args[0].Split('|');
        int type = int.Parse(subStrings[0]);
        Debug.Log("onUserEarnedReward " + (AdHelper.AdType)type + " " + int.Parse(subStrings[1]) + " " + subStrings[2]);

        if (AdHelper.getInstance().showListeners[(int)AdType.AD_REWARD - 1] != null)
        {
            RewardShowListener rewardShowListener =  (RewardShowListener)AdHelper.getInstance().showListeners[(int)AdType.AD_REWARD - 1];
            rewardShowListener.onUserEarnedReward(int.Parse(subStrings[1]), subStrings[2]);
        }
    }

    //display listener
}
