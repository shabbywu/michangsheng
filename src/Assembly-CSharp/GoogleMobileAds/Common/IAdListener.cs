using System;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000E77 RID: 3703
	internal interface IAdListener
	{
		// Token: 0x060058B9 RID: 22713
		void FireAdLoaded();

		// Token: 0x060058BA RID: 22714
		void FireAdFailedToLoad(string message);

		// Token: 0x060058BB RID: 22715
		void FireAdOpened();

		// Token: 0x060058BC RID: 22716
		void FireAdClosing();

		// Token: 0x060058BD RID: 22717
		void FireAdClosed();

		// Token: 0x060058BE RID: 22718
		void FireAdLeftApplication();
	}
}
