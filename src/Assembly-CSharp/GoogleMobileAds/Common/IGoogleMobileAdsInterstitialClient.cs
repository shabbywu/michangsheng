using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000E79 RID: 3705
	internal interface IGoogleMobileAdsInterstitialClient
	{
		// Token: 0x060058C4 RID: 22724
		void CreateInterstitialAd(string adUnitId);

		// Token: 0x060058C5 RID: 22725
		void LoadAd(AdRequest request);

		// Token: 0x060058C6 RID: 22726
		bool IsLoaded();

		// Token: 0x060058C7 RID: 22727
		void ShowInterstitial();

		// Token: 0x060058C8 RID: 22728
		void DestroyInterstitial();

		// Token: 0x060058C9 RID: 22729
		void SetInAppPurchaseParams(IInAppPurchaseListener listener, string androidPublicKey);
	}
}
