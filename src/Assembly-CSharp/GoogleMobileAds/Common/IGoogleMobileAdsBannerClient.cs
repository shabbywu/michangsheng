using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000B0C RID: 2828
	internal interface IGoogleMobileAdsBannerClient
	{
		// Token: 0x06004ECB RID: 20171
		void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position);

		// Token: 0x06004ECC RID: 20172
		void LoadAd(AdRequest request);

		// Token: 0x06004ECD RID: 20173
		void ShowBannerView();

		// Token: 0x06004ECE RID: 20174
		void HideBannerView();

		// Token: 0x06004ECF RID: 20175
		void DestroyBannerView();
	}
}
