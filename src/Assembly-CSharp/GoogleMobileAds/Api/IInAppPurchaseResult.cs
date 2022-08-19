using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000B16 RID: 2838
	public interface IInAppPurchaseResult
	{
		// Token: 0x06004F02 RID: 20226
		void FinishPurchase();

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06004F03 RID: 20227
		string ProductId { get; }
	}
}
