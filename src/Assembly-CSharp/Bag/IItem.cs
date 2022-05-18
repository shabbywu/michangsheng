using System;

namespace Bag
{
	// Token: 0x02000D2A RID: 3370
	public interface IItem
	{
		// Token: 0x06005041 RID: 20545
		void SetItem(int id, int count, JSONObject seid);

		// Token: 0x06005042 RID: 20546
		void SetItem(int id, int count);

		// Token: 0x06005043 RID: 20547
		void Use();

		// Token: 0x06005044 RID: 20548
		int GetPlayerPrice();

		// Token: 0x06005045 RID: 20549
		int GetPrice();
	}
}
