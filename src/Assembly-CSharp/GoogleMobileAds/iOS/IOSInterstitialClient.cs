using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.iOS
{
	// Token: 0x02000B09 RID: 2825
	internal class IOSInterstitialClient : IGoogleMobileAdsInterstitialClient
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06004EAA RID: 20138 RVA: 0x0021765C File Offset: 0x0021585C
		// (set) Token: 0x06004EAB RID: 20139 RVA: 0x00217664 File Offset: 0x00215864
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

		// Token: 0x06004EAC RID: 20140 RVA: 0x00217678 File Offset: 0x00215878
		public IOSInterstitialClient(IAdListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00217688 File Offset: 0x00215888
		public void CreateInterstitialAd(string adUnitId)
		{
			IntPtr interstitialClient = (IntPtr)GCHandle.Alloc(this);
			this.InterstitialPtr = Externs.GADUCreateInterstitial(interstitialClient, adUnitId);
			Externs.GADUSetInterstitialCallbacks(this.InterstitialPtr, new IOSInterstitialClient.GADUInterstitialDidReceiveAdCallback(IOSInterstitialClient.InterstitialDidReceiveAdCallback), new IOSInterstitialClient.GADUInterstitialDidFailToReceiveAdWithErrorCallback(IOSInterstitialClient.InterstitialDidFailToReceiveAdWithErrorCallback), new IOSInterstitialClient.GADUInterstitialWillPresentScreenCallback(IOSInterstitialClient.InterstitialWillPresentScreenCallback), new IOSInterstitialClient.GADUInterstitialWillDismissScreenCallback(IOSInterstitialClient.InterstitialWillDismissScreenCallback), new IOSInterstitialClient.GADUInterstitialDidDismissScreenCallback(IOSInterstitialClient.InterstitialDidDismissScreenCallback), new IOSInterstitialClient.GADUInterstitialWillLeaveApplicationCallback(IOSInterstitialClient.InterstitialWillLeaveApplicationCallback));
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x00217704 File Offset: 0x00215904
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

		// Token: 0x06004EAF RID: 20143 RVA: 0x002178A0 File Offset: 0x00215AA0
		public bool IsLoaded()
		{
			return Externs.GADUInterstitialReady(this.InterstitialPtr);
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x002178AD File Offset: 0x00215AAD
		public void ShowInterstitial()
		{
			Externs.GADUShowInterstitial(this.InterstitialPtr);
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x002178BA File Offset: 0x00215ABA
		public void DestroyInterstitial()
		{
			this.InterstitialPtr = IntPtr.Zero;
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x002178C7 File Offset: 0x00215AC7
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialDidReceiveAdCallback))]
		private static void InterstitialDidReceiveAdCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdLoaded();
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x002178D9 File Offset: 0x00215AD9
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialDidFailToReceiveAdWithErrorCallback))]
		private static void InterstitialDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdFailedToLoad(error);
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x002178EC File Offset: 0x00215AEC
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialWillPresentScreenCallback))]
		private static void InterstitialWillPresentScreenCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdOpened();
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x002178FE File Offset: 0x00215AFE
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialWillDismissScreenCallback))]
		private static void InterstitialWillDismissScreenCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosing();
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x00217910 File Offset: 0x00215B10
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialDidDismissScreenCallback))]
		private static void InterstitialDidDismissScreenCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosed();
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x00217922 File Offset: 0x00215B22
		[MonoPInvokeCallback(typeof(IOSInterstitialClient.GADUInterstitialWillLeaveApplicationCallback))]
		private static void InterstitialWillLeaveApplicationCallback(IntPtr interstitialClient)
		{
			IOSInterstitialClient.IntPtrToInterstitialClient(interstitialClient).listener.FireAdLeftApplication();
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x00217934 File Offset: 0x00215B34
		private static IOSInterstitialClient IntPtrToInterstitialClient(IntPtr interstitialClient)
		{
			return ((GCHandle)interstitialClient).Target as IOSInterstitialClient;
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00004095 File Offset: 0x00002295
		public void SetInAppPurchaseParams(IInAppPurchaseListener listener, string publicKey)
		{
		}

		// Token: 0x04004E30 RID: 20016
		private IAdListener listener;

		// Token: 0x04004E31 RID: 20017
		private IntPtr interstitialPtr;

		// Token: 0x04004E32 RID: 20018
		private static Dictionary<IntPtr, IOSBannerClient> bannerClients;

		// Token: 0x020015DC RID: 5596
		// (Invoke) Token: 0x06008543 RID: 34115
		internal delegate void GADUInterstitialDidReceiveAdCallback(IntPtr interstitialClient);

		// Token: 0x020015DD RID: 5597
		// (Invoke) Token: 0x06008547 RID: 34119
		internal delegate void GADUInterstitialDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error);

		// Token: 0x020015DE RID: 5598
		// (Invoke) Token: 0x0600854B RID: 34123
		internal delegate void GADUInterstitialWillPresentScreenCallback(IntPtr interstitialClient);

		// Token: 0x020015DF RID: 5599
		// (Invoke) Token: 0x0600854F RID: 34127
		internal delegate void GADUInterstitialWillDismissScreenCallback(IntPtr interstitialClient);

		// Token: 0x020015E0 RID: 5600
		// (Invoke) Token: 0x06008553 RID: 34131
		internal delegate void GADUInterstitialDidDismissScreenCallback(IntPtr interstitialClient);

		// Token: 0x020015E1 RID: 5601
		// (Invoke) Token: 0x06008557 RID: 34135
		internal delegate void GADUInterstitialWillLeaveApplicationCallback(IntPtr interstitialClient);
	}
}
