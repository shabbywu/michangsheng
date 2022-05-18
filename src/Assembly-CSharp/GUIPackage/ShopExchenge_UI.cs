using System;
using System.Collections.Generic;

namespace GUIPackage
{
	// Token: 0x02000D75 RID: 3445
	public class ShopExchenge_UI : Shop_UI
	{
		// Token: 0x060052C4 RID: 21188 RVA: 0x0003B463 File Offset: 0x00039663
		private void Start()
		{
			base.InitExChengShopItems(0);
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x0003B46C File Offset: 0x0003966C
		public void init(int shopID)
		{
			this.ShopID = shopID;
			base.InitExChengMethod(0);
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x0003B47C File Offset: 0x0003967C
		public override int getExShopID(int type)
		{
			return this.ShopID;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x00004050 File Offset: 0x00002250
		public override int getShopType(int type)
		{
			return 0;
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x00227DE0 File Offset: 0x00225FE0
		public override void updateItem()
		{
			int num = 0;
			foreach (Inventory2 inv in this.invList)
			{
				this.InitExShopPage(this.selectList[num].NowIndex, inv, num + 1);
				num++;
			}
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x00017C2D File Offset: 0x00015E2D
		public new void closeShop()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x00004050 File Offset: 0x00002250
		public override bool ShouldSetLevel(int Type = 0)
		{
			return false;
		}

		// Token: 0x040052D1 RID: 21201
		public int ShopID = -1;

		// Token: 0x040052D2 RID: 21202
		public bool IsCangJinGe;

		// Token: 0x040052D3 RID: 21203
		public List<int> CangJinGeShop = new List<int>();
	}
}
