using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.iOS
{
	// Token: 0x02000E68 RID: 3688
	internal class IOSBannerClient : IGoogleMobileAdsBannerClient
	{
		// Token: 0x0600585F RID: 22623 RVA: 0x0003F176 File Offset: 0x0003D376
		public IOSBannerClient(IAdListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06005860 RID: 22624 RVA: 0x0003F185 File Offset: 0x0003D385
		// (set) Token: 0x06005861 RID: 22625 RVA: 0x0003F18D File Offset: 0x0003D38D
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

		// Token: 0x06005862 RID: 22626 RVA: 0x002477AC File Offset: 0x002459AC
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

		// Token: 0x06005863 RID: 22627 RVA: 0x0024784C File Offset: 0x00245A4C
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

		// Token: 0x06005864 RID: 22628 RVA: 0x0003F1A1 File Offset: 0x0003D3A1
		public void ShowBannerView()
		{
			Externs.GADUShowBannerView(this.BannerViewPtr);
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x0003F1AE File Offset: 0x0003D3AE
		public void HideBannerView()
		{
			Externs.GADUHideBannerView(this.BannerViewPtr);
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x0003F1BB File Offset: 0x0003D3BB
		public void DestroyBannerView()
		{
			Externs.GADURemoveBannerView(this.BannerViewPtr);
			this.BannerViewPtr = IntPtr.Zero;
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x0003F1D3 File Offset: 0x0003D3D3
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewDidReceiveAdCallback))]
		private static void AdViewDidReceiveAdCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdLoaded();
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x0003F1E5 File Offset: 0x0003D3E5
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewDidFailToReceiveAdWithErrorCallback))]
		private static void AdViewDidFailToReceiveAdWithErrorCallback(IntPtr bannerClient, string error)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdFailedToLoad(error);
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x0003F1F8 File Offset: 0x0003D3F8
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewWillPresentScreenCallback))]
		private static void AdViewWillPresentScreenCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdOpened();
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x0003F20A File Offset: 0x0003D40A
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewWillDismissScreenCallback))]
		private static void AdViewWillDismissScreenCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdClosing();
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x0003F21C File Offset: 0x0003D41C
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewDidDismissScreenCallback))]
		private static void AdViewDidDismissScreenCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdClosed();
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x0003F22E File Offset: 0x0003D42E
		[MonoPInvokeCallback(typeof(IOSBannerClient.GADUAdViewWillLeaveApplicationCallback))]
		private static void AdViewWillLeaveApplicationCallback(IntPtr bannerClient)
		{
			IOSBannerClient.IntPtrToBannerClient(bannerClient).listener.FireAdLeftApplication();
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x002479D8 File Offset: 0x00245BD8
		private static IOSBannerClient IntPtrToBannerClient(IntPtr bannerClient)
		{
			return ((GCHandle)bannerClient).Target as IOSBannerClient;
		}

		// Token: 0x04005876 RID: 22646
		private IAdListener listener;

		// Token: 0x04005877 RID: 22647
		private IntPtr bannerViewPtr;

		// Token: 0x04005878 RID: 22648
		private static Dictionary<IntPtr, IOSBannerClient> bannerClients;

		// Token: 0x02000E69 RID: 3689
		// (Invoke) Token: 0x0600586F RID: 22639
		internal delegate void GADUAdViewDidReceiveAdCallback(IntPtr bannerClient);

		// Token: 0x02000E6A RID: 3690
		// (Invoke) Token: 0x06005873 RID: 22643
		internal delegate void GADUAdViewDidFailToReceiveAdWithErrorCallback(IntPtr bannerClient, string error);

		// Token: 0x02000E6B RID: 3691
		// (Invoke) Token: 0x06005877 RID: 22647
		internal delegate void GADUAdViewWillPresentScreenCallback(IntPtr bannerClient);

		// Token: 0x02000E6C RID: 3692
		// (Invoke) Token: 0x0600587B RID: 22651
		internal delegate void GADUAdViewWillDismissScreenCallback(IntPtr bannerClient);

		// Token: 0x02000E6D RID: 3693
		// (Invoke) Token: 0x0600587F RID: 22655
		internal delegate void GADUAdViewDidDismissScreenCallback(IntPtr bannerClient);

		// Token: 0x02000E6E RID: 3694
		// (Invoke) Token: 0x06005883 RID: 22659
		internal delegate void GADUAdViewWillLeaveApplicationCallback(IntPtr bannerClient);
	}
}
