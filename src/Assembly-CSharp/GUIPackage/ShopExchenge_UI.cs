using System;
using System.Collections.Generic;

namespace GUIPackage
{
	// Token: 0x02000A55 RID: 2645
	public class ShopExchenge_UI : Shop_UI
	{
		// Token: 0x060049D9 RID: 18905 RVA: 0x001F531F File Offset: 0x001F351F
		private void Start()
		{
			base.InitExChengShopItems(0);
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x001F5328 File Offset: 0x001F3528
		public void init(int shopID)
		{
			this.ShopID = shopID;
			base.InitExChengMethod(0);
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x001F5338 File Offset: 0x001F3538
		public override int getExShopID(int type)
		{
			return this.ShopID;
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x0000280F File Offset: 0x00000A0F
		public override int getShopType(int type)
		{
			return 0;
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x001F5340 File Offset: 0x001F3540
		public override void updateItem()
		{
			int num = 0;
			foreach (Inventory2 inv in this.invList)
			{
				this.InitExShopPage(this.selectList[num].NowIndex, inv, num + 1);
				num++;
			}
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x000B5E62 File Offset: 0x000B4062
		public new void closeShop()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool ShouldSetLevel(int Type = 0)
		{
			return false;
		}

		// Token: 0x0400494C RID: 18764
		public int ShopID = -1;

		// Token: 0x0400494D RID: 18765
		public bool IsCangJinGe;

		// Token: 0x0400494E RID: 18766
		public List<int> CangJinGeShop = new List<int>();
	}
}
