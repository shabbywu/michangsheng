using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000E84 RID: 3716
	public interface IInAppPurchaseResult
	{
		// Token: 0x0600590C RID: 22796
		void FinishPurchase();

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600590D RID: 22797
		string ProductId { get; }
	}
}
