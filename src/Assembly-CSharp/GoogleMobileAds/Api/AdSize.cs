using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000B12 RID: 2834
	public class AdSize
	{
		// Token: 0x06004EE2 RID: 20194 RVA: 0x00217A83 File Offset: 0x00215C83
		public AdSize(int width, int height)
		{
			this.isSmartBanner = false;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x00217AA0 File Offset: 0x00215CA0
		private AdSize(bool isSmartBanner)
		{
			this.isSmartBanner = isSmartBanner;
			this.width = 0;
			this.height = 0;
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x00217ABD File Offset: 0x00215CBD
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x00217AC5 File Offset: 0x00215CC5
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x00217ACD File Offset: 0x00215CCD
		public bool IsSmartBanner
		{
			get
			{
				return this.isSmartBanner;
			}
		}

		// Token: 0x04004E43 RID: 20035
		private bool isSmartBanner;

		// Token: 0x04004E44 RID: 20036
		private int width;

		// Token: 0x04004E45 RID: 20037
		private int height;

		// Token: 0x04004E46 RID: 20038
		public static readonly AdSize Banner = new AdSize(320, 50);

		// Token: 0x04004E47 RID: 20039
		public static readonly AdSize MediumRectangle = new AdSize(300, 250);

		// Token: 0x04004E48 RID: 20040
		public static readonly AdSize IABBanner = new AdSize(468, 60);

		// Token: 0x04004E49 RID: 20041
		public static readonly AdSize Leaderboard = new AdSize(728, 90);

		// Token: 0x04004E4A RID: 20042
		public static readonly AdSize SmartBanner = new AdSize(true);
	}
}
