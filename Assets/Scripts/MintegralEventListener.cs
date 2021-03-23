using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID || UNITY_IPHONE
public class MintegralEventListener : MonoBehaviour
{

	// void OnEnable ()
	// {
	// 	// Listen to all events for illustration purposes

	// 	//InterActive 
	// 	MintegralManager.onInterActiveLoadedEvent += onInterActiveLoadedEvent;
	// 	MintegralManager.onInterActiveFailedEvent += onInterActiveFailedEvent;
	// 	MintegralManager.onInterActiveShownEvent += onInterActiveShownEvent;
	// 	MintegralManager.onInterActiveShownFailedEvent += onInterActiveShownFailedEvent;
	// 	MintegralManager.onInterActiveClickedEvent += onInterActiveClickedEvent;
	// 	MintegralManager.onInterActiveDismissedEvent += onInterActiveDismissedEvent;

    //     MintegralManager.onInterActiveMaterialLoadedEvent += onInterActiveMaterialLoadedEvent;
    //     MintegralManager.onInterActivePlayingCompleteEvent += onInterActivePlayingCompleteEvent;


    //     //InterstitialVideo 
    //     MintegralManager.onInterstitialVideoLoadSuccessEvent += onInterstitialVideoLoadSuccessEvent;
    //     MintegralManager.onInterstitialVideoLoadedEvent += onInterstitialVideoLoadedEvent;
	// 	MintegralManager.onInterstitialVideoFailedEvent += onInterstitialVideoFailedEvent;
	// 	MintegralManager.onInterstitialVideoShownEvent += onInterstitialVideoShownEvent;
	// 	MintegralManager.onInterstitialVideoShownFailedEvent += onInterstitialVideoShownFailedEvent;
	// 	MintegralManager.onInterstitialVideoClickedEvent += onInterstitialVideoClickedEvent;
	// 	MintegralManager.onInterstitialVideoDismissedEvent += onInterstitialVideoDismissedEvent;
    //     MintegralManager.onInterstitialVideoPlayCompletedEvent += onInterstitialVideoPlayCompletedEvent;
    //     MintegralManager.onInterstitialVideoEndCardShowSuccessEvent += onInterstitialVideoEndCardShowSuccessEvent;


    //     //Interstitial 
    //     MintegralManager.onInterstitialLoadedEvent += onInterstitialLoadedEvent;
	// 	MintegralManager.onInterstitialFailedEvent += onInterstitialFailedEvent;
	// 	MintegralManager.onInterstitialShownEvent += onInterstitialShownEvent;
	// 	MintegralManager.onInterstitialShownFailedEvent += onInterstitialShownFailedEvent;
	// 	MintegralManager.onInterstitialClickedEvent += onInterstitialClickedEvent;
	// 	MintegralManager.onInterstitialDismissedEvent += onInterstitialDismissedEvent;


    //     //Reward Video
    //     MintegralManager.onRewardedVideoLoadSuccessEvent += onRewardedVideoLoadSuccessEvent;
    //     MintegralManager.onRewardedVideoLoadedEvent += onRewardedVideoLoadedEvent;
	// 	MintegralManager.onRewardedVideoFailedEvent += onRewardedVideoFailedEvent;
	// 	MintegralManager.onRewardedVideoShownFailedEvent += onRewardedVideoShownFailedEvent;
	// 	MintegralManager.onRewardedVideoShownEvent += onRewardedVideoShownEvent;
    //     MintegralManager.onRewardedVideoClickedEvent += onRewardedVideoClickedEvent;
	// 	MintegralManager.onRewardedVideoClosedEvent += onRewardedVideoClosedEvent;
    //     MintegralManager.onRewardedVideoPlayCompletedEvent += onRewardedVideoPlayCompletedEvent;
    //     MintegralManager.onRewardedVideoEndCardShowSuccessEvent += onRewardedVideoEndCardShowSuccessEvent;


    //     // OfferWall
    //     MintegralManager.onOfferWallLoadedEvent += onOfferWallLoadedEvent;
	// 	MintegralManager.onOfferWallFailedEvent += onOfferWallFailedEvent;
	// 	MintegralManager.onOfferWallDidClickEvent += onOfferWallDidClickEvent;
	// 	MintegralManager.onOfferWallShownEvent += onOfferWallShownEvent;
	// 	MintegralManager.onOfferWallShownFailedEvent += onOfferWallShownFailedEvent;
	// 	MintegralManager.onOfferWallClosedEvent += onOfferWallClosedEvent;
	// 	MintegralManager.onOfferWallEarnedImmediatelyEvent += onOfferWallEarnedImmediatelyEvent;
	// 	MintegralManager.onOfferWallNotifyCreditsEarnedAfterQueryEvent +=
	// 					onOfferWallNotifyCreditsEarnedAfterQueryEvent;

	// 	// Native

	// 	MintegralManager.onNativeLoadedEvent += onNativeLoadedEvent;
	// 	MintegralManager.onNativeFailedEvent += onNativeFailedEvent;
	// 	MintegralManager.onNativeDidClickEvent += onNativeDidClickEvent;
	// 	MintegralManager.onNativeLoggingImpressionEvent += onNativeLoggingImpressionEvent;
	// 	MintegralManager.onNativeRedirectionStartEvent += onNativeRedirectionStartEvent;
	// 	MintegralManager.onNativeRedirectionFinishedEvent += onNativeRedirectionFinishedEvent;

	// 	// GDPR

	// 	MintegralManager.onShowUserInfoTipsEvent += onShowUserInfoTipsEvent;
	// }



	// void OnDisable ()
	// {
	// 	// Remove all event handlers

	// 	//InterActive 
	// 	MintegralManager.onInterActiveLoadedEvent -= onInterActiveLoadedEvent;
	// 	MintegralManager.onInterActiveFailedEvent -= onInterActiveFailedEvent;
	// 	MintegralManager.onInterActiveShownEvent -= onInterActiveShownEvent;
	// 	MintegralManager.onInterActiveShownFailedEvent -= onInterActiveShownFailedEvent;
	// 	MintegralManager.onInterActiveClickedEvent -= onInterActiveClickedEvent;
	// 	MintegralManager.onInterActiveDismissedEvent -= onInterActiveDismissedEvent;

	// 	//InterstitialVideo 
	// 	MintegralManager.onInterstitialVideoLoadedEvent -= onInterstitialVideoLoadedEvent;
	// 	MintegralManager.onInterstitialVideoFailedEvent -= onInterstitialVideoFailedEvent;
	// 	MintegralManager.onInterstitialVideoShownEvent -= onInterstitialVideoShownEvent;
	// 	MintegralManager.onInterstitialVideoShownFailedEvent -= onInterstitialVideoShownFailedEvent;
	// 	MintegralManager.onInterstitialVideoClickedEvent -= onInterstitialVideoClickedEvent;
	// 	MintegralManager.onInterstitialVideoDismissedEvent -= onInterstitialVideoDismissedEvent;


	// 	//Interstitial 
	// 	MintegralManager.onInterstitialLoadedEvent -= onInterstitialLoadedEvent;
	// 	MintegralManager.onInterstitialFailedEvent -= onInterstitialFailedEvent;
	// 	MintegralManager.onInterstitialShownEvent -= onInterstitialShownEvent;
	// 	MintegralManager.onInterstitialShownFailedEvent -= onInterstitialShownFailedEvent;
	// 	MintegralManager.onInterstitialClickedEvent -= onInterstitialClickedEvent;
	// 	MintegralManager.onInterstitialDismissedEvent -= onInterstitialDismissedEvent;



	// 	//Reward Video
	// 	MintegralManager.onRewardedVideoLoadedEvent -= onRewardedVideoLoadedEvent;
	// 	MintegralManager.onRewardedVideoFailedEvent -= onRewardedVideoFailedEvent;
	// 	MintegralManager.onRewardedVideoShownFailedEvent -= onRewardedVideoShownFailedEvent;
	// 	MintegralManager.onRewardedVideoShownEvent -= onRewardedVideoShownEvent;
	// 	MintegralManager.onRewardedVideoClickedEvent -= onRewardedVideoClickedEvent;
	// 	MintegralManager.onRewardedVideoClosedEvent -= onRewardedVideoClosedEvent;


	// 	// OfferWall
	// 	MintegralManager.onOfferWallLoadedEvent -= onOfferWallLoadedEvent;
	// 	MintegralManager.onOfferWallFailedEvent -= onOfferWallFailedEvent;
	// 	MintegralManager.onOfferWallDidClickEvent -= onOfferWallDidClickEvent;
	// 	MintegralManager.onOfferWallShownEvent -= onOfferWallShownEvent;
	// 	MintegralManager.onOfferWallShownFailedEvent -= onOfferWallShownFailedEvent;
	// 	MintegralManager.onOfferWallClosedEvent -= onOfferWallClosedEvent;
	// 	MintegralManager.onOfferWallEarnedImmediatelyEvent -= onOfferWallEarnedImmediatelyEvent;
	// 	MintegralManager.onOfferWallNotifyCreditsEarnedAfterQueryEvent -=
	// 		onOfferWallNotifyCreditsEarnedAfterQueryEvent;

	// 	// Native

	// 	MintegralManager.onNativeLoadedEvent -= onNativeLoadedEvent;
	// 	MintegralManager.onNativeFailedEvent -= onNativeFailedEvent;
	// 	MintegralManager.onNativeDidClickEvent -= onNativeDidClickEvent;
	// 	MintegralManager.onNativeLoggingImpressionEvent -= onNativeLoggingImpressionEvent;
	// 	MintegralManager.onNativeRedirectionStartEvent -= onNativeRedirectionStartEvent;
	// 	MintegralManager.onNativeRedirectionFinishedEvent -= onNativeRedirectionFinishedEvent;

	// 	// GDPR

	// 	MintegralManager.onShowUserInfoTipsEvent -= onShowUserInfoTipsEvent;
	// }

	// void mtgLog(string log){

	// 	Debug.LogError ("Mintegral: " + log + "\n" + "------------------------------");

	// }

	// // InterActive Events

	// void onInterActiveLoadedEvent (string adUnitId)
	// {
	// 	this.mtgLog ("onInterActiveLoadedEvent: " + adUnitId);
	// }

	// void onInterActiveFailedEvent (string errorMsg)
	// {
	// 	this.mtgLog ("onInterActiveFailedEvent: " + errorMsg);
	// }

	// void onInterActiveShownEvent (string errorMsg)
	// {
	// 	this.mtgLog("onInterActiveShownEvent: " + errorMsg);
	// }

	// void onInterActiveShownFailedEvent (string adUnitId)
	// {
	// 	this.mtgLog ("onInterActiveShownFailedEvent: " + adUnitId);
	// }

	// void onInterActiveClickedEvent (string adUnitId)
	// {
	// 	this.mtgLog ("onInterActiveClickedEvent: " + adUnitId);
	// }

	// void onInterActiveDismissedEvent (string errorMsg)
	// {
	// 	this.mtgLog ("onInterActiveDismissedEvent: " + errorMsg);
	// }

    // void onInterActiveMaterialLoadedEvent(string adUnitId)
    // {
    //     this.mtgLog("onInterActiveMaterialLoadedEvent: " + adUnitId);
    // }

    // void onInterActivePlayingCompleteEvent(string completeOrNot)
    // {
    //     this.mtgLog("onInterActivePlayingCompleteEvent: " + completeOrNot);
    // }



    // // InterstitialVideo Events

    // void onInterstitialVideoLoadSuccessEvent(string adUnitId)
    // {
    //     this.mtgLog("onInterstitialVideoLoadSuccessEvent: " + adUnitId);
    // }

    // void onInterstitialVideoLoadedEvent (string adUnitId)
	// {
	// 	this.mtgLog ("onInterstitialVideoLoadedEvent: " + adUnitId);
	// }

	// void onInterstitialVideoFailedEvent (string errorMsg)
	// {
	// 	this.mtgLog ("onInterstitialVideoFailedEvent: " + errorMsg);
	// }

	// void onInterstitialVideoShownEvent (string errorMsg)
	// {
	// 	this.mtgLog("onInterstitialVideoShownEvent: " + errorMsg);
	// }

	// void onInterstitialVideoShownFailedEvent (string adUnitId)
	// {
	// 	this.mtgLog ("onInterstitialVideoShownFailedEvent: " + adUnitId);
	// }

	// void onInterstitialVideoClickedEvent (string adUnitId)
	// {
	// 	this.mtgLog ("onInterstitialVideoClickedEvent: " + adUnitId);
	// }

	// void onInterstitialVideoDismissedEvent (string errorMsg)
	// {
	// 	this.mtgLog ("onInterstitialVideoDismissedEvent: " + errorMsg);
	// }

    // void onInterstitialVideoPlayCompletedEvent(string adUnitId)
    // {
    //     this.mtgLog("onInterstitialVideoPlayCompletedEvent: " + adUnitId);
    // }

    // void onInterstitialVideoEndCardShowSuccessEvent(string adUnitId)
    // {
    //     this.mtgLog("onInterstitialVideoEndCardShowSuccessEvent: " + adUnitId);
    // }



    // // Interstitial Events

    // void onInterstitialLoadedEvent ()
	// {
	// 	this.mtgLog ("onInterstitialLoadedEvent");
	// }

	// void onInterstitialFailedEvent (string errorMsg)
	// {
	// 	this.mtgLog ("onInterstitialFailedEvent: " + errorMsg);
	// }

	// void onInterstitialShownEvent ()
	// {
	// 	this.mtgLog("onInterstitialShownEvent");
	// }

	// void onInterstitialShownFailedEvent (string adUnitId)
	// {
	// 	this.mtgLog ("onInterstitialShownFailedEvent: " + adUnitId);
	// }

	// void onInterstitialClickedEvent ()
	// {
	// 	this.mtgLog ("onInterstitialClickedEvent");
	// }

	// void onInterstitialDismissedEvent ()
	// {
	// 	this.mtgLog ("onInterstitialDismissedEvent");
	// }




    // // Rewarded Video Events

    // void onRewardedVideoLoadSuccessEvent(string adUnitId)
    // {
    //     this.mtgLog("onRewardedVideoLoadSuccessEvent: " + adUnitId);
    // }

    // void onRewardedVideoLoadedEvent (string adUnitId)
	// {
	// 	this.mtgLog ("onRewardedVideoLoadedEvent: " + adUnitId);
	// }

	// void onRewardedVideoFailedEvent (string errorMsg)
	// {
	// 	this.mtgLog ("onRewardedVideoFailedEvent: " + errorMsg);
	// }

	// void onRewardedVideoShownFailedEvent (string adUnitId)
	// {
	// 	this.mtgLog ("onRewardedVideoShownFailedEvent: " + adUnitId);
	// }

	// void onRewardedVideoShownEvent ()
	// {
	// 	this.mtgLog ("onRewardedVideoShownEvent");
	// }

	// void onRewardedVideoClickedEvent (string errorMsg)
	// {
	// 	this.mtgLog ("onRewardedVideoClickedEvent: " + errorMsg);
	// }

	// void onRewardedVideoClosedEvent (MintegralManager.MTGRewardData rewardData)
	// {
	// 	if (rewardData.converted) {
	// 		this.mtgLog ("onRewardedVideoClosedEvent: " + rewardData.ToString ());
	// 	} else {
	// 		this.mtgLog ("onRewardedVideoClosedEvent: No Reward"  );
	// 	}
	// }

    // void onRewardedVideoPlayCompletedEvent(string adUnitId)
    // {
    //     this.mtgLog("onRewardedVideoPlayCompletedEvent: " + adUnitId);
    // }

    // void onRewardedVideoEndCardShowSuccessEvent(string adUnitId)
    // {
    //     this.mtgLog("onRewardedVideoEndCardShowSuccessEvent: " + adUnitId);
    // }


    // // OfferWall
    // void onOfferWallLoadedEvent ()
	// {
	// 	this.mtgLog ("onOfferWallLoadedEvent");
	// }
	// void onOfferWallFailedEvent (string errorMsg)
	// {
	// 	this.mtgLog ("onOfferWallFailedEvent: " + errorMsg);
	// }
	// void onOfferWallDidClickEvent ()
	// {
	// 	this.mtgLog ("onOfferWallDidClickEvent");
	// }
	// void onOfferWallShownEvent ()
	// {
	// 	this.mtgLog ("onOfferWallShownEvent");
	// }
	// void onOfferWallShownFailedEvent (string errorMsg)
	// {
	// 	this.mtgLog ("onOfferWallShownFailedEvent: " + errorMsg);
	// }
	// void onOfferWallClosedEvent ()
	// {
	// 	this.mtgLog ("onOfferWallClosedEvent");
	// }
	// void onOfferWallEarnedImmediatelyEvent (MintegralManager.MTGRewardData[] rewardDatas)
	// {
	// 	this.mtgLog ("onOfferWallEarnedImmediatelyEvent: ");

	// 	foreach (MintegralManager.MTGRewardData rewardData in rewardDatas) {
	// 		this.mtgLog ("OfferWall RewardData: " + rewardData.ToString());
	// 	}
	// }
	// void onOfferWallNotifyCreditsEarnedAfterQueryEvent (MintegralManager.MTGRewardData[] rewardDatas)
	// {
	// 	this.mtgLog ("onOfferWallNotifyCreditsEarnedAfterQueryEvent: ");

	// 	foreach (MintegralManager.MTGRewardData rewardData in rewardDatas) {
	// 		this.mtgLog ("OfferWall RewardData: " + rewardData.ToString());
	// 	}
	// }

	// //Native

	// void onNativeLoadedEvent (string msg)
	// {
	// 	this.mtgLog ("onNativeLoadedEvent: " + msg);
	// }
	// void onNativeFailedEvent (string msg)
	// {
	// 	this.mtgLog ("onNativeFailedEvent: " + msg);
	// }
	// void onNativeDidClickEvent (string msg)
	// {
	// 	this.mtgLog ("onNativeDidClickEvent: " + msg);
	// }
	// void onNativeLoggingImpressionEvent (string msg)
	// {
	// 	this.mtgLog ("onNativeLoggingImpressionEvent: " + msg);
	// }
	// void onNativeRedirectionStartEvent (string msg)
	// {
	// 	this.mtgLog ("onNativeRedirectionStartEvent: " + msg);
	// }
	// void onNativeRedirectionFinishedEvent (string msg)
	// {
	// 	this.mtgLog ("onNativeRedirectionFinishedEvent: " + msg);
	// }

	// //Native

	// void onShowUserInfoTipsEvent (string msg)
	// {
		
	// 	this.mtgLog ("onShowUserInfoTipsEvent: " + msg);

	// 	/*
	// 	#if UNITY_ANDROID

	// 	Mintegral.initMTGSDK (MintegralDemoGUI.MTGSDKAppIDForAndroid,MintegralDemoGUI.MTGSDKApiKeyForAndroid);
	// 	#elif UNITY_IPHONE

	// 	Mintegral.initMTGSDK (MintegralDemoGUI.MTGSDKAppIDForiOS,MintegralDemoGUI.MTGSDKApiKeyForiOS);
	// 	#endif
	// 	*/

	// }

}
#endif
