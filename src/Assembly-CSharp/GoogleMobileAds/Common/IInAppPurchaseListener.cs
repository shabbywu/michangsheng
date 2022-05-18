using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000E7A RID: 3706
	internal interface IInAppPurchaseListener
	{
		// Token: 0x060058CA RID: 22730
		void FireOnInAppPurchaseFinished(IInAppPurchaseResult result);

		// Token: 0x060058CB RID: 22731
		bool FireIsValidPurchase(string sku);
	}
}
