using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000E7F RID: 3711
	public class AdSize
	{
		// Token: 0x060058E4 RID: 22756 RVA: 0x0003F467 File Offset: 0x0003D667
		public AdSize(int width, int height)
		{
			this.isSmartBanner = false;
			this.width = width;
			this.height = height;
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x0003F484 File Offset: 0x0003D684
		private AdSize(bool isSmartBanner)
		{
			this.isSmartBanner = isSmartBanner;
			this.width = 0;
			this.height = 0;
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060058E6 RID: 22758 RVA: 0x0003F4A1 File Offset: 0x0003D6A1
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060058E7 RID: 22759 RVA: 0x0003F4A9 File Offset: 0x0003D6A9
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x0003F4B1 File Offset: 0x0003D6B1
		public bool IsSmartBanner
		{
			get
			{
				return this.isSmartBanner;
			}
		}

		// Token: 0x04005892 RID: 22674
		private bool isSmartBanner;

		// Token: 0x04005893 RID: 22675
		private int width;

		// Token: 0x04005894 RID: 22676
		private int height;

		// Token: 0x04005895 RID: 22677
		public static readonly AdSize Banner = new AdSize(320, 50);

		// Token: 0x04005896 RID: 22678
		public static readonly AdSize MediumRectangle = new AdSize(300, 250);

		// Token: 0x04005897 RID: 22679
		public static readonly AdSize IABBanner = new AdSize(468, 60);

		// Token: 0x04005898 RID: 22680
		public static readonly AdSize Leaderboard = new AdSize(728, 90);

		// Token: 0x04005899 RID: 22681
		public static readonly AdSize SmartBanner = new AdSize(true);
	}
}
