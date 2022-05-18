using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000E76 RID: 3702
	internal class DummyClient : IGoogleMobileAdsBannerClient, IGoogleMobileAdsInterstitialClient
	{
		// Token: 0x060058AE RID: 22702 RVA: 0x0003F2FF File Offset: 0x0003D4FF
		public DummyClient(IAdListener listener)
		{
			Debug.Log("Created DummyClient");
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x0003F311 File Offset: 0x0003D511
		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Debug.Log("Dummy CreateBannerView");
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x0003F31D File Offset: 0x0003D51D
		public void LoadAd(AdRequest request)
		{
			Debug.Log("Dummy LoadAd");
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x0003F329 File Offset: 0x0003D529
		public void ShowBannerView()
		{
			Debug.Log("Dummy ShowBannerView");
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x0003F335 File Offset: 0x0003D535
		public void HideBannerView()
		{
			Debug.Log("Dummy HideBannerView");
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x0003F341 File Offset: 0x0003D541
		public void DestroyBannerView()
		{
			Debug.Log("Dummy DestroyBannerView");
		}

		// Token: 0x060058B4 RID: 22708 RVA: 0x0003F34D File Offset: 0x0003D54D
		public void CreateInterstitialAd(string adUnitId)
		{
			Debug.Log("Dummy CreateIntersitialAd");
		}

		// Token: 0x060058B5 RID: 22709 RVA: 0x0003F359 File Offset: 0x0003D559
		public bool IsLoaded()
		{
			Debug.Log("Dummy IsLoaded");
			return true;
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x0003F366 File Offset: 0x0003D566
		public void ShowInterstitial()
		{
			Debug.Log("Dummy ShowInterstitial");
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x0003F372 File Offset: 0x0003D572
		public void DestroyInterstitial()
		{
			Debug.Log("Dummy DestroyInterstitial");
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x0003F37E File Offset: 0x0003D57E
		public void SetInAppPurchaseParams(IInAppPurchaseListener listener, string androidPublicKey)
		{
			Debug.Log("Dummy SetInAppPurchaseParams");
		}
	}
}
