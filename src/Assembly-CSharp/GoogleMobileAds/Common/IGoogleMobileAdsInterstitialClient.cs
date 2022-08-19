using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000B0D RID: 2829
	internal interface IGoogleMobileAdsInterstitialClient
	{
		// Token: 0x06004ED0 RID: 20176
		void CreateInterstitialAd(string adUnitId);

		// Token: 0x06004ED1 RID: 20177
		void LoadAd(AdRequest request);

		// Token: 0x06004ED2 RID: 20178
		bool IsLoaded();

		// Token: 0x06004ED3 RID: 20179
		void ShowInterstitial();

		// Token: 0x06004ED4 RID: 20180
		void DestroyInterstitial();

		// Token: 0x06004ED5 RID: 20181
		void SetInAppPurchaseParams(IInAppPurchaseListener listener, string androidPublicKey);
	}
}
