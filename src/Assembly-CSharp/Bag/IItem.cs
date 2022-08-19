using System;

namespace Bag
{
	// Token: 0x020009A4 RID: 2468
	public interface IItem
	{
		// Token: 0x060044E2 RID: 17634
		void SetItem(int id, int count, JSONObject seid);

		// Token: 0x060044E3 RID: 17635
		void SetItem(int id, int count);

		// Token: 0x060044E4 RID: 17636
		void Use();

		// Token: 0x060044E5 RID: 17637
		int GetPlayerPrice();

		// Token: 0x060044E6 RID: 17638
		int GetPrice();
	}
}
