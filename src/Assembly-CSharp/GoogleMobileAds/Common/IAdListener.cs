using System;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000B0B RID: 2827
	internal interface IAdListener
	{
		// Token: 0x06004EC5 RID: 20165
		void FireAdLoaded();

		// Token: 0x06004EC6 RID: 20166
		void FireAdFailedToLoad(string message);

		// Token: 0x06004EC7 RID: 20167
		void FireAdOpened();

		// Token: 0x06004EC8 RID: 20168
		void FireAdClosing();

		// Token: 0x06004EC9 RID: 20169
		void FireAdClosed();

		// Token: 0x06004ECA RID: 20170
		void FireAdLeftApplication();
	}
}
