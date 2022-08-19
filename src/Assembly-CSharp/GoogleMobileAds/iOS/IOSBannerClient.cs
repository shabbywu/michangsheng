using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.iOS
{
	// Token: 0x02000B08 RID: 2824
	internal class IOSBannerClient : IGoogleMobileAdsBannerClient
	{
		// Token: 0x06004E9B RID: 20123 RVA: 0x00217344 File Offset: 0x00215544
		public IOSBannerClient(IAdListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06004E9C RID: 20124 RVA: 0x00217353 File Offset: 0x00215553
		// (set) Token: 0x06004E9D RID: 20125 RVA: 0x0021735B File Offset: 0x0021555B
		private IntPtr BannerViewPtr
		{
			get
			{
				return this.bannerViewPtr;
			}
			set
			{
				Externs.GADURelease(this.bannerViewPtr);
				this.bannerViewPtr = value;
			}
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x00217370 File Offset: 0x00215570
		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			IntPtr bannerClient = (IntPtr)GCHandle.Alloc(this);
			if (adSize.IsSmartBanner)
			{
				this.BannerViewPtr = Externs.GADUCreateSmartBannerView(bannerClient, adUnitId, (int)position);
			}
			else
			{
				this.BannerViewPtr = Externs.GADUCreateBannerView(bannerClient, adUnitId, adSize.Width, adSize.Height, (int)position);
			}
			Externs.GADUSetBannerCallbacks(this.BannerViewPtr, new IOSBannerClient.GADUAdViewDidReceiveAdCallback(IOSBannerClient.AdViewDidReceiveAdCallback), new IOSBannerClient.GADUAdViewDidFailToReceiveAdWithErrorCallback(IOSBannerClient.AdViewDidFailToReceiveAdWithErrorCallback), new IOSBannerClient.GADUAdViewWillPresentScreenCallback(IOSBannerClient.AdViewWillPresentScreenCallback), new IOSBannerClient.GADUAdViewWillDismissScreenCallback(IOSBannerClient.AdViewWillDismissScreenCallback), new IOSBannerClient.GADUAdViewDidDismissScreenCallback(IOSBannerClient.AdViewDidDismissScreenCallback), new IOSBannerClient.GADUAdViewWillLeaveApplicationCallback(IOSBannerClient.AdViewWillLeaveApplicationCallback));
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x00217410 File Offset: 0x00215610
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
			Externs.GADURequestBannerAd(this.BannerViewPtr, intPtr);
			Externs.GADURelease(intPtr);
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x0021759C File Offset: 0x0021579C
		public void ShowBannerView()
		{
			Externs.GADUShowBannerView(this.BannerViewPtr);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x002175A9 File Offset: 0x002157A9
		public void HideBannerView()
		{
			Externs.GADUHideBannerView(this.BannerViewPtr);
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x002175B6 File Offset: 0x002157B6
		public void DestroyBannerView()
		{
			Externs.GADURemoveBannerView(this.BannerViewPtr);
			this.BannerViewPtr = IntPtr.Zero;
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x002175CE File Offset: 0x002157CE
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewDidReceiveAdCallback))]
		private static void AdViewDidReceiveAdCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdLoaded();
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x002175E0 File Offset: 0x002157E0
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewDidFailToReceiveAdWithErrorCallback))]
		private static void AdViewDidFailToReceiveAdWithErrorCallback(IntPtr bannerClient, string error)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdFailedToLoad(error);
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x002175F3 File Offset: 0x002157F3
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewWillPresentScreenCallback))]
		private static void AdViewWillPresentScreenCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdOpened();
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x00217605 File Offset: 0x00215805
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewWillDismissScreenCallback))]
		private static void AdViewWillDismissScreenCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdClosing();
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x00217617 File Offset: 0x00215817
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewDidDismissScreenCallback))]
		private static void AdViewDidDismissScreenCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdClosed();
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x00217629 File Offset: 0x00215829
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewWillLeaveApplicationCallback))]
		private static void AdViewWillLeaveApplicationCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdLeftApplication();
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x0021763C File Offset: 0x0021583C
		private static IOSBannerClient IntPtrToBannerClient(IntPtr bannerClient)
		{
			return ((GCHandle)bannerClient).Target as IOSBannerClient;
		}

		// Token: 0x04004E2D RID: 20013
		private IAdListener listener;

		// Token: 0x04004E2E RID: 20014
		private IntPtr bannerViewPtr;

		// Token: 0x04004E2F RID: 20015
		private static Dictionary<IntPtr, IOSBannerClient> bannerClients;

		// Token: 0x020015D6 RID: 5590
		// (Invoke) Token: 0x0600852B RID: 34091
		internal delegate void GADUAdViewDidReceiveAdCallback(IntPtr bannerClient);

		// Token: 0x020015D7 RID: 5591
		// (Invoke) Token: 0x0600852F RID: 34095
		internal delegate void GADUAdViewDidFailToReceiveAdWithErrorCallback(IntPtr bannerClient, string error);

		// Token: 0x020015D8 RID: 5592
		// (Invoke) Token: 0x06008533 RID: 34099
		internal delegate void GADUAdViewWillPresentScreenCallback(IntPtr bannerClient);

		// Token: 0x020015D9 RID: 5593
		// (Invoke) Token: 0x06008537 RID: 34103
		internal delegate void GADUAdViewWillDismissScreenCallback(IntPtr bannerClient);

		// Token: 0x020015DA RID: 5594
		// (Invoke) Token: 0x0600853B RID: 34107
		internal delegate void GADUAdViewDidDismissScreenCallback(IntPtr bannerClient);

		// Token: 0x020015DB RID: 5595
		// (Invoke) Token: 0x0600853F RID: 34111
		internal delegate void GADUAdViewWillLeaveApplicationCallback(IntPtr bannerClient);
	}
}
