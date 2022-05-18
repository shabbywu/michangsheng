using System;
using System.Runtime.InteropServices;

namespace GoogleMobileAds.iOS
{
	// Token: 0x02000E67 RID: 3687
	internal class Externs
	{
		// Token: 0x0600584A RID: 22602
		[DllImport("__Internal")]
		internal static extern IntPtr GADUCreateRequest();

		// Token: 0x0600584B RID: 22603
		[DllImport("__Internal")]
		internal static extern void GADUAddTestDevice(IntPtr request, string deviceId);

		// Token: 0x0600584C RID: 22604
		[DllImport("__Internal")]
		internal static extern void GADUAddKeyword(IntPtr request, string keyword);

		// Token: 0x0600584D RID: 22605
		[DllImport("__Internal")]
		internal static extern void GADUSetBirthday(IntPtr request, int year, int month, int day);

		// Token: 0x0600584E RID: 22606
		[DllImport("__Internal")]
		internal static extern void GADUSetGender(IntPtr request, int genderCode);

		// Token: 0x0600584F RID: 22607
		[DllImport("__Internal")]
		internal static extern void GADUTagForChildDirectedTreatment(IntPtr request, bool childDirectedTreatment);

		// Token: 0x06005850 RID: 22608
		[DllImport("__Internal")]
		internal static extern void GADUSetExtra(IntPtr request, string key, string value);

		// Token: 0x06005851 RID: 22609
		[DllImport("__Internal")]
		internal static extern void GADURelease(IntPtr obj);

		// Token: 0x06005852 RID: 22610
		[DllImport("__Internal")]
		internal static extern IntPtr GADUCreateBannerView(IntPtr bannerClient, string adUnitId, int width, int height, int positionAtTop);

		// Token: 0x06005853 RID: 22611
		[DllImport("__Internal")]
		internal static extern IntPtr GADUCreateSmartBannerView(IntPtr bannerClient, string adUnitId, int positionAtTop);

		// Token: 0x06005854 RID: 22612
		[DllImport("__Internal")]
		internal static extern void GADUSetBannerCallbacks(IntPtr bannerView, IOSBannerClient.GADUAdViewDidReceiveAdCallback adReceivedCallback, IOSBannerClient.GADUAdViewDidFailToReceiveAdWithErrorCallback adFailedCallback, IOSBannerClient.GADUAdViewWillPresentScreenCallback willPresentCallback, IOSBannerClient.GADUAdViewWillDismissScreenCallback willDismissCallback, IOSBannerClient.GADUAdViewDidDismissScreenCallback didDismissCallback, IOSBannerClient.GADUAdViewWillLeaveApplicationCallback willLeaveCallback);

		// Token: 0x06005855 RID: 22613
		[DllImport("__Internal")]
		internal static extern void GADUHideBannerView(IntPtr bannerView);

		// Token: 0x06005856 RID: 22614
		[DllImport("__Internal")]
		internal static extern void GADUShowBannerView(IntPtr bannerView);

		// Token: 0x06005857 RID: 22615
		[DllImport("__Internal")]
		internal static extern void GADURemoveBannerView(IntPtr bannerView);

		// Token: 0x06005858 RID: 22616
		[DllImport("__Internal")]
		internal static extern void GADURequestBannerAd(IntPtr bannerView, IntPtr request);

		// Token: 0x06005859 RID: 22617
		[DllImport("__Internal")]
		internal static extern IntPtr GADUCreateInterstitial(IntPtr interstitialClient, string adUnitId);

		// Token: 0x0600585A RID: 22618
		[DllImport("__Internal")]
		internal static extern void GADUSetInterstitialCallbacks(IntPtr interstitial, IOSInterstitialClient.GADUInterstitialDidReceiveAdCallback adReceivedCallback, IOSInterstitialClient.GADUInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback, IOSInterstitialClient.GADUInterstitialWillPresentScreenCallback willPresentCallback, IOSInterstitialClient.GADUInterstitialWillDismissScreenCallback willDismissCallback, IOSInterstitialClient.GADUInterstitialDidDismissScreenCallback didDismissCallback, IOSInterstitialClient.GADUInterstitialWillLeaveApplicationCallback willLeaveCallback);

		// Token: 0x0600585B RID: 22619
		[DllImport("__Internal")]
		internal static extern bool GADUInterstitialReady(IntPtr interstitial);

		// Token: 0x0600585C RID: 22620
		[DllImport("__Internal")]
		internal static extern void GADUShowInterstitial(IntPtr interstitial);

		// Token: 0x0600585D RID: 22621
		[DllImport("__Internal")]
		internal static extern void GADURequestInterstitial(IntPtr interstitial, IntPtr request);
	}
}
