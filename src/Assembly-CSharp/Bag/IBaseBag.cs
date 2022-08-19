using System;
using System.Collections.Generic;
using KBEngine;

namespace Bag
{
	// Token: 0x0200099C RID: 2460
	public interface IBaseBag
	{
		// Token: 0x06004492 RID: 17554
		void OpenBag(List<ITEM_INFO> itemList);

		// Token: 0x06004493 RID: 17555
		void UpdateItem();
	}
}
