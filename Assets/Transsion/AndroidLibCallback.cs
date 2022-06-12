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
        var type = int.Parse(args[0]);
        Debug.Log("onAdClosed " + (AdType)type);

        if (type == (int)AdType.AD_BANNER && getInstance().loadListeners[type - 1] != null)
        {
            var bannerListener = (BannerListener)getInstance().loadListeners[type - 1];
            bannerListener.onAdClosed();
        }
    }

    public void onAdOpened(params string[] args)
    {
        var type = int.Parse(args[0]);
        Debug.Log("onAdOpened " + (AdType)type);


        if (type == (int)AdType.AD_BANNER && getInstance().loadListeners[type - 1] != null)
        {
            var bannerListener = (BannerListener)getInstance().loadListeners[type - 1];
            bannerListener.onAdOpened();
        }
    }

    public void onAdImpression(params string[] args)
    {
        var type = int.Parse(args[0]);
        Debug.Log("onAdImpression " + (AdType)type);

        if (type == (int)AdType.AD_BANNER && getInstance().loadListeners[type - 1] != null)
        {
            var bannerListener = (BannerListener)getInstance().loadListeners[type - 1];
            bannerListener.onAdImpression();
        }
    }

    //load
    public void onAdLoaded(params string[] args)
    {
        var type = int.Parse(args[0]);
        Debug.Log("onAdLoaded " + (AdType)type);

        if (getInstance().loadListeners[type - 1] != null) getInstance().loadListeners[type - 1].onAdLoaded();
    }

    public void onAdFailedToLoad(params string[] args)
    {
        var subStrings = args[0].Split('|');
        var type = int.Parse(subStrings[0]);
        Debug.Log("onAdFailedToLoad " + (AdType)type + " " + int.Parse(subStrings[1]) + " " + subStrings[2]);

        if (getInstance().loadListeners[type - 1] != null)
            getInstance().loadListeners[type - 1].onAdFailedToLoad((REASON)int.Parse(subStrings[1]), subStrings[2]);
    }

    //show
    public void onShow(params string[] args)
    {
        var type = int.Parse(args[0]);
        Debug.Log("onShow " + (AdType)type);

        if (getInstance().showListeners[type - 1] != null) getInstance().showListeners[type - 1].onShow();
    }

    public void onClose(params string[] args)
    {
        var type = int.Parse(args[0]);
        Debug.Log("onClose " + (AdType)type);

        if (getInstance().showListeners[type - 1] != null) getInstance().showListeners[type - 1].onClose();
    }

    public void onShowFailed(params string[] args)
    {
        var subStrings = args[0].Split('|');
        var type = int.Parse(subStrings[0]);
        Debug.Log("onShowFailed " + (AdType)type + " " + (REASON)int.Parse(subStrings[1]) + " " + subStrings[2]);


        if (getInstance().showListeners[type - 1] != null)
            getInstance().showListeners[type - 1].onShowFailed((REASON)int.Parse(subStrings[1]), subStrings[2]);
    }

    //reward show
    public void onUserEarnedReward(params string[] args)
    {
        var subStrings = args[0].Split('|');
        var type = int.Parse(subStrings[0]);
        Debug.Log("onUserEarnedReward " + (AdType)type + " " + int.Parse(subStrings[1]) + " " + subStrings[2]);

        if (getInstance().showListeners[(int)AdType.AD_REWARD - 1] != null)
        {
            var rewardShowListener = (RewardShowListener)getInstance().showListeners[(int)AdType.AD_REWARD - 1];
            rewardShowListener.onUserEarnedReward(int.Parse(subStrings[1]), subStrings[2]);
        }
    }

    //display listener
}