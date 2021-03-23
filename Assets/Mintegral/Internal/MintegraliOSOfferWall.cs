using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using AOT;

#if UNITY_IPHONE


public class MintegraliOSOfferWall
{
	System.IntPtr offerWallManager;

	[DllImport ("__Internal")]
	private static extern System.IntPtr initOfferWall (string unitId,string userId,string adCategory);

	[DllImport ("__Internal")]
	private static extern void loadOfferWall (System.IntPtr instance);

	[DllImport ("__Internal")]
	private static extern void showOfferWall (System.IntPtr instance);

	[DllImport ("__Internal")]
	private static extern void queryOfferWallRewards (System.IntPtr instance);

	/*
	[DllImport ("__Internal")]
	private static extern void setAlertTipsWhenVideoClosed (System.IntPtr instance,string alertTips);
	*/



	public void queryOfferWallRewards()
	{
		if (Application.platform != RuntimePlatform.OSXEditor) {

	        if (Application.platform == RuntimePlatform.IPhonePlayer){

				if (offerWallManager == System.IntPtr.Zero) return;

				queryOfferWallRewards (offerWallManager);
	        }
		}
	}


	public void showOfferWallAd()
	{
		if (Application.platform != RuntimePlatform.OSXEditor) {

	        if (Application.platform == RuntimePlatform.IPhonePlayer){
					if (offerWallManager == System.IntPtr.Zero)
							return;

					showOfferWall (offerWallManager);
	        	}
			}
	}

	public void requestOfferWallAd()
	{
		if (Application.platform != RuntimePlatform.OSXEditor) {

	        if (Application.platform == RuntimePlatform.IPhonePlayer){
			
				if (offerWallManager == System.IntPtr.Zero)
					return;

					loadOfferWall (offerWallManager);
	        }
		}
	}

	public MintegraliOSOfferWall(MTGOfferWallInfo info)
  {
		string adCategoryStr = ((MTGAdCategory)info.adCategory).ToString ("d");

		//string alertTipsStr = JsonUtility.ToJson (info.alertTips);
		if (Application.platform != RuntimePlatform.OSXEditor) {
			if (Application.platform == RuntimePlatform.IPhonePlayer){

				offerWallManager = initOfferWall (info.adUnitId, info.userId, adCategoryStr);

				/*
				if (offerWallManager != System.IntPtr.Zero){
					setAlertTipsWhenVideoClosed (offerWallManager,alertTipsStr);
				}
				*/
			}
		}
  }


}
#endif
