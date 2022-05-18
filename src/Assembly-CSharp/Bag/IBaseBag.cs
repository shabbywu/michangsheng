using System;
using System.Collections.Generic;
using KBEngine;

namespace Bag
{
	// Token: 0x02000D21 RID: 3361
	public interface IBaseBag
	{
		// Token: 0x06004FF2 RID: 20466
		void OpenBag(List<ITEM_INFO> itemList);

		// Token: 0x06004FF3 RID: 20467
		void UpdateItem();
	}
}
