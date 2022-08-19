using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds
{
	// Token: 0x02000B06 RID: 2822
	internal class GoogleMobileAdsClientFactory
	{
		// Token: 0x06004E83 RID: 20099 RVA: 0x0021733C File Offset: 0x0021553C
		internal static IGoogleMobileAdsBannerClient GetGoogleMobileAdsBannerClient(IAdListener listener)
		{
			return new DummyClient(listener);
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x0021733C File Offset: 0x0021553C
		internal static IGoogleMobileAdsInterstitialClient GetGoogleMobileAdsInterstitialClient(IAdListener listener)
		{
			return new DummyClient(listener);
		}
	}
}
