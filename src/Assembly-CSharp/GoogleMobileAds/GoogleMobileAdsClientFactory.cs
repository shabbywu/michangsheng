using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds
{
	// Token: 0x02000E66 RID: 3686
	internal class GoogleMobileAdsClientFactory
	{
		// Token: 0x06005847 RID: 22599 RVA: 0x0003F16E File Offset: 0x0003D36E
		internal static IGoogleMobileAdsBannerClient GetGoogleMobileAdsBannerClient(IAdListener listener)
		{
			return new DummyClient(listener);
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x0003F16E File Offset: 0x0003D36E
		internal static IGoogleMobileAdsInterstitialClient GetGoogleMobileAdsInterstitialClient(IAdListener listener)
		{
			return new DummyClient(listener);
		}
	}
}
