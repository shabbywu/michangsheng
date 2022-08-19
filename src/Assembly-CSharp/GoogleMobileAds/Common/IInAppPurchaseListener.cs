using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000B0E RID: 2830
	internal interface IInAppPurchaseListener
	{
		// Token: 0x06004ED6 RID: 20182
		void FireOnInAppPurchaseFinished(IInAppPurchaseResult result);

		// Token: 0x06004ED7 RID: 20183
		bool FireIsValidPurchase(string sku);
	}
}
