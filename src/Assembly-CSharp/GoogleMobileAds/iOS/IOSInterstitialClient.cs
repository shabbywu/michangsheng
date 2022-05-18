using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.iOS
{
	// Token: 0x02000E6F RID: 3695
	internal class IOSInterstitialClient : IGoogleMobileAdsInterstitialClient
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06005886 RID: 22662 RVA: 0x0003F240 File Offset: 0x0003D440
		// (set) Token: 0x06005887 RID: 22663 RVA: 0x0003F248 File Offset: 0x0003D448
		private IntPtr InterstitialPtr
		{
			get
			{
				return this.interstitialPtr;
			}
			set
			{
				Externs.GADURelease(this.interstitialPtr);
				this.interstitialPtr = value;
			}
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x0003F25C File Offset: 0x0003D45C
		public IOSInterstitialClient(IAdListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x002479F8 File Offset: 0x00245BF8
		public void CreateInterstitialAd(string adUnitId)
		{
			IntPtr interstitialClient = (IntPtr)GCHandle.Alloc(this);
			this.InterstitialPtr = Externs.GADUCreateInterstitial(interstitialClient, adUnitId);
			Externs.GADUSetInterstitialCallbacks(this.InterstitialPtr, new IOSInterstitialClient.GADUInterstitialDidReceiveAdCallback(IOSInterstitialClient.InterstitialDidReceiveAdCallback), new IOSInterstitialClient.GADUInterstitialDidFailToReceiveAdWithErrorCallback(IOSInterstitialClient.InterstitialDidFailToReceiveAdWithErrorCallback), new IOSInterstitialClient.GADUInterstitialWillPresentScreenCallback(IOSInterstitialClient.InterstitialWillPresentScreenCallback), new IOSInterstitialClient.GADUInterstitialWillDismissScreenCallback(IOSInterstitialClient.InterstitialWillDismissScreenCallback), new IOSInterstitialClient.GADUInterstitialDidDismissScreenCallback(IOSInterstitialClient.InterstitialDidDismissScreenCallback), new IOSInterstitialClient.GADUInterstitialWillLeaveApplicationCallback(IOSInterstitialClient.InterstitialWillLeaveApplicationCallback));
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x00247A74 File Offset: 0x00245C74
		public void LoadAd(AdRequest request)
		{
			IntPtr intPtr = Externs.GADUCreateRequest();
			foreach (string keyword in request.Keywords)
			{
				Externs.GADUAddKeyword(intPtr, keyword);
			}
			foreach (string deviceId in request.TestDevices)
			{
				Externs.GADUAddTestDevice(intPtr, deviceId);
			}
			if (request.Birthday != null)
			{
				DateTime valueOrDefault = request.Birthday.GetValueOrDefault();
				Externs.GADUSetBirthday(intPtr, valueOrDefault.Year, valueOrDefault.Month, valueOrDefault.Day);
			}
			if (request.Gender != null)
			{
				Externs.GADUSetGender(intPtr, (int)request.Gender.GetValueOrDefault());
			}
			if (request.TagForChildDirectedTreatment != null)
			{
				Externs.GADUTagForChildDirectedTreatment(intPtr, request.TagForChildDirectedTreatment.GetValueOrDefault());
			}
			foreach (KeyValuePair<string, string> keyValuePair in request.Extras)
			{
				Externs.GADUSetExtra(intPtr, keyValuePair.Key, keyValuePair.Value);
			}
			Externs.GADUSetExtra(intPtr, "unity", "1");
			Externs.GADURequestInterstitial(this.InterstitialPtr, intPtr);
			Externs.GADURelease(intPtr);
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x0003F26B File Offset: 0x0003D46B
		public bool IsLoaded()
		{
			return Externs.GADUInterstitialReady(this.InterstitialPtr);
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x0003F278 File Offset: 0x0003D478
		public void ShowInterstitial()
		{
			Externs.GADUShowInterstitial(this.InterstitialPtr);
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x0003F285 File Offset: 0x0003D485
		public void DestroyInterstitial()
		{
			this.InterstitialPtr = IntPtr.Zero;
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x0003F292 File Offset: 0x0003D492
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialDidReceiveAdCallback))]
		private static void InterstitialDidReceiveAdCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdLoaded();
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x0003F2A4 File Offset: 0x0003D4A4
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialDidFailToReceiveAdWithErrorCallback))]
		private static void InterstitialDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdFailedToLoad(error);
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x0003F2B7 File Offset: 0x0003D4B7
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialWillPresentScreenCallback))]
		private static void InterstitialWillPresentScreenCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdOpened();
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x0003F2C9 File Offset: 0x0003D4C9
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialWillDismissScreenCallback))]
		private static void InterstitialWillDismissScreenCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosing();
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x0003F2DB File Offset: 0x0003D4DB
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialDidDismissScreenCallback))]
		private static void InterstitialDidDismissScreenCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosed();
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x0003F2ED File Offset: 0x0003D4ED
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialWillLeaveApplicationCallback))]
		private static void InterstitialWillLeaveApplicationCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdLeftApplication();
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x00247C10 File Offset: 0x00245E10
		private static IOSInterstitialClient IntPtrToInterstitialClient(IntPtr interstitialClient)
		{
			return ((GCHandle)interstitialClient).Target as IOSInterstitialClient;
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x000042DD File Offset: 0x000024DD
		public void SetInAppPurchaseParams(IInAppPurchaseListener listener, string publicKey)
		{
		}

		// Token: 0x04005879 RID: 22649
		private IAdListener listener;

		// Token: 0x0400587A RID: 22650
		private IntPtr interstitialPtr;

		// Token: 0x0400587B RID: 22651
		private static Dictionary<IntPtr, IOSBannerClient> bannerClients;

		// Token: 0x02000E70 RID: 3696
		// (Invoke) Token: 0x06005897 RID: 22679
		internal delegate void GADUInterstitialDidReceiveAdCallback(IntPtr interstitialClient);

		// Token: 0x02000E71 RID: 3697
		// (Invoke) Token: 0x0600589B RID: 22683
		internal delegate void GADUInterstitialDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error);

		// Token: 0x02000E72 RID: 3698
		// (Invoke) Token: 0x0600589F RID: 22687
		internal delegate void GADUInterstitialWillPresentScreenCallback(IntPtr interstitialClient);

		// Token: 0x02000E73 RID: 3699
		// (Invoke) Token: 0x060058A3 RID: 22691
		internal delegate void GADUInterstitialWillDismissScreenCallback(IntPtr interstitialClient);

		// Token: 0x02000E74 RID: 3700
		// (Invoke) Token: 0x060058A7 RID: 22695
		internal delegate void GADUInterstitialDidDismissScreenCallback(IntPtr interstitialClient);

		// Token: 0x02000E75 RID: 3701
		// (Invoke) Token: 0x060058AB RID: 22699
		internal delegate void GADUInterstitialWillLeaveApplicationCallback(IntPtr interstitialClient);
	}
}
