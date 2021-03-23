using UnityEngine;
using System.Collections.Generic;


#if UNITY_ANDROID

public class MintegralAndroidOfferWall
{
	private readonly AndroidJavaObject _offerWallPlugin;

	public MintegralAndroidOfferWall (MTGOfferWallInfo info)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;
		
		string adCategoryStr = ((MTGAdCategory)info.adCategory).ToString ("d");
		string alertTipsStr = JsonUtility.ToJson (info.alertTips);

		_offerWallPlugin = new AndroidJavaObject ("com.mintegral.msdk.unity.MTGOfferWall", info.adUnitId,info.userId,adCategoryStr,alertTipsStr);
	}


	// Starts loading an OfferWall ad
	public void requestOfferWallAd ()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_offerWallPlugin.Call ("loadOfferwall");
	}


	// If an OfferWall ad is loaded this will take over the screen and show the ad
	public void showOfferWallAd ()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_offerWallPlugin.Call ("showOfferwall");
	}

  	// If an OfferWall ad is complete this will
  	public void queryOfferWallRewards ()
  	{
	    if (Application.platform != RuntimePlatform.Android)
	      return;

		_offerWallPlugin.Call ("queryOfferwallRewards");
  	}

}

#endif
