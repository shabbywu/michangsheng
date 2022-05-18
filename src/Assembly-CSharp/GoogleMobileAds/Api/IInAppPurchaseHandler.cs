using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000E83 RID: 3715
	public interface IInAppPurchaseHandler
	{
		// Token: 0x06005909 RID: 22793
		void OnInAppPurchaseFinished(IInAppPurchaseResult result);

		// Token: 0x0600590A RID: 22794
		bool IsValidPurchase(string sku);

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600590B RID: 22795
		string AndroidPublicKey { get; }
	}
}
