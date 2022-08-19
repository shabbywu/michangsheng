using System;
using System.Runtime.InteropServices;

namespace GoogleMobileAds.iOS
{
	// Token: 0x02000B07 RID: 2823
	internal class Externs
	{
		// Token: 0x06004E86 RID: 20102
		[DllImport("__Internal")]
		internal static extern IntPtr GADUCreateRequest();

		// Token: 0x06004E87 RID: 20103
		[DllImport("__Internal")]
		internal static extern void GADUAddTestDevice(IntPtr request, string deviceId);

		// Token: 0x06004E88 RID: 20104
		[DllImport("__Internal")]
		internal static extern void GADUAddKeyword(IntPtr request, string keyword);

		// Token: 0x06004E89 RID: 20105
		[DllImport("__Internal")]
		internal static extern void GADUSetBirthday(IntPtr request, int year, int month, int day);

		// Token: 0x06004E8A RID: 20106
		[DllImport("__Internal")]
		internal static extern void GADUSetGender(IntPtr request, int genderCode);

		// Token: 0x06004E8B RID: 20107
		[DllImport("__Internal")]
		internal static extern void GADUTagForChildDirectedTreatment(IntPtr request, bool childDirectedTreatment);

		// Token: 0x06004E8C RID: 20108
		[DllImport("__Internal")]
		internal static extern void GADUSetExtra(IntPtr request, string key, string value);

		// Token: 0x06004E8D RID: 20109
		[DllImport("__Internal")]
		internal static extern void GADURelease(IntPtr obj);

		// Token: 0x06004E8E RID: 20110
		[DllImport("__Internal")]
		internal static extern IntPtr GADUCreateBannerView(IntPtr bannerClient, string adUnitId, int width, int height, int positionAtTop);

		// Token: 0x06004E8F RID: 20111
		[DllImport("__Internal")]
		internal static extern IntPtr GADUCreateSmartBannerView(IntPtr bannerClient, string adUnitId, int positionAtTop);

		// Token: 0x06004E90 RID: 20112
		[DllImport("__Internal")]
		internal static extern void GADUSetBannerCallbacks(IntPtr bannerView, IOSBannerClient.GADUAdViewDidReceiveAdCallback adReceivedCallback, IOSBannerClient.GADUAdViewDidFailToReceiveAdWithErrorCallback adFailedCallback, IOSBannerClient.GADUAdViewWillPresentScreenCallback willPresentCallback, IOSBannerClient.GADUAdViewWillDismissScreenCallback willDismissCallback, IOSBannerClient.GADUAdViewDidDismissScreenCallback didDismissCallback, IOSBannerClient.GADUAdViewWillLeaveApplicationCallback willLeaveCallback);

		// Token: 0x06004E91 RID: 20113
		[DllImport("__Internal")]
		internal static extern void GADUHideBannerView(IntPtr bannerView);

		// Token: 0x06004E92 RID: 20114
		[DllImport("__Internal")]
		internal static extern void GADUShowBannerView(IntPtr bannerView);

		// Token: 0x06004E93 RID: 20115
		[DllImport("__Internal")]
		internal static extern void GADURemoveBannerView(IntPtr bannerView);

		// Token: 0x06004E94 RID: 20116
		[DllImport("__Internal")]
		internal static extern void GADURequestBannerAd(IntPtr bannerView, IntPtr request);

		// Token: 0x06004E95 RID: 20117
		[DllImport("__Internal")]
		internal static extern IntPtr GADUCreateInterstitial(IntPtr interstitialClient, string adUnitId);

		// Token: 0x06004E96 RID: 20118
		[DllImport("__Internal")]
		internal static extern void GADUSetInterstitialCallbacks(IntPtr interstitial, IOSInterstitialClient.GADUInterstitialDidReceiveAdCallback adReceivedCallback, IOSInterstitialClient.GADUInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback, IOSInterstitialClient.GADUInterstitialWillPresentScreenCallback willPresentCallback, IOSInterstitialClient.GADUInterstitialWillDismissScreenCallback willDismissCallback, IOSInterstitialClient.GADUInterstitialDidDismissScreenCallback didDismissCallback, IOSInterstitialClient.GADUInterstitialWillLeaveApplicationCallback willLeaveCallback);

		// Token: 0x06004E97 RID: 20119
		[DllImport("__Internal")]
		internal static extern bool GADUInterstitialReady(IntPtr interstitial);

		// Token: 0x06004E98 RID: 20120
		[DllImport("__Internal")]
		internal static extern void GADUShowInterstitial(IntPtr interstitial);

		// Token: 0x06004E99 RID: 20121
		[DllImport("__Internal")]
		internal static extern void GADURequestInterstitial(IntPtr interstitial, IntPtr request);
	}
}
