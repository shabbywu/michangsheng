using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000B15 RID: 2837
	public interface IInAppPurchaseHandler
	{
		// Token: 0x06004EFF RID: 20223
		void OnInAppPurchaseFinished(IInAppPurchaseResult result);

		// Token: 0x06004F00 RID: 20224
		bool IsValidPurchase(string sku);

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06004F01 RID: 20225
		string AndroidPublicKey { get; }
	}
}
