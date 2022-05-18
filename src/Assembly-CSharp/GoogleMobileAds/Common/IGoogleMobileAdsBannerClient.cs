using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000E78 RID: 3704
	internal interface IGoogleMobileAdsBannerClient
	{
		// Token: 0x060058BF RID: 22719
		void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position);

		// Token: 0x060058C0 RID: 22720
		void LoadAd(AdRequest request);

		// Token: 0x060058C1 RID: 22721
		void ShowBannerView();

		// Token: 0x060058C2 RID: 22722
		void HideBannerView();

		// Token: 0x060058C3 RID: 22723
		void DestroyBannerView();
	}
}
