using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000B0A RID: 2826
	internal class DummyClient : IGoogleMobileAdsBannerClient, IGoogleMobileAdsInterstitialClient
	{
		// Token: 0x06004EBA RID: 20154 RVA: 0x00217954 File Offset: 0x00215B54
		public DummyClient(IAdListener listener)
		{
			Debug.Log("Created DummyClient");
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x00217966 File Offset: 0x00215B66
		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Debug.Log("Dummy CreateBannerView");
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x00217972 File Offset: 0x00215B72
		public void LoadAd(AdRequest request)
		{
			Debug.Log("Dummy LoadAd");
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x0021797E File Offset: 0x00215B7E
		public void ShowBannerView()
		{
			Debug.Log("Dummy ShowBannerView");
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x0021798A File Offset: 0x00215B8A
		public void HideBannerView()
		{
			Debug.Log("Dummy HideBannerView");
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x00217996 File Offset: 0x00215B96
		public void DestroyBannerView()
		{
			Debug.Log("Dummy DestroyBannerView");
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x002179A2 File Offset: 0x00215BA2
		public void CreateInterstitialAd(string adUnitId)
		{
			Debug.Log("Dummy CreateIntersitialAd");
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x002179AE File Offset: 0x00215BAE
		public bool IsLoaded()
		{
			Debug.Log("Dummy IsLoaded");
			return true;
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x002179BB File Offset: 0x00215BBB
		public void ShowInterstitial()
		{
			Debug.Log("Dummy ShowInterstitial");
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x002179C7 File Offset: 0x00215BC7
		public void DestroyInterstitial()
		{
			Debug.Log("Dummy DestroyInterstitial");
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x002179D3 File Offset: 0x00215BD3
		public void SetInAppPurchaseParams(IInAppPurchaseListener listener, string androidPublicKey)
		{
			Debug.Log("Dummy SetInAppPurchaseParams");
		}
	}
}
