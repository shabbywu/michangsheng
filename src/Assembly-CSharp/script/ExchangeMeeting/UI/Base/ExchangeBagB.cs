using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;

namespace script.ExchangeMeeting.UI.Base
{
	// Token: 0x02000A35 RID: 2613
	public class ExchangeBagB : ExchangeBag
	{
		// Token: 0x060047E9 RID: 18409 RVA: 0x001E60B4 File Offset: 0x001E42B4
		public override void CreateTempList()
		{
			this.TempBagList = new List<ITEM_INFO>();
			foreach (ITEM_INFO item_INFO in PlayerEx.Player.itemList.values)
			{
				if (item_INFO.itemCount > 0U && _ItemJsonData.DataDict.ContainsKey(item_INFO.itemId) && _ItemJsonData.DataDict[item_INFO.itemId].CanSale != 1)
				{
					ITEM_INFO item_INFO2 = new ITEM_INFO();
					item_INFO2.itemId = item_INFO.itemId;
					item_INFO2.itemCount = item_INFO.itemCount;
					item_INFO2.uuid = item_INFO.uuid;
					if (item_INFO.Seid != null)
					{
						item_INFO2.Seid = item_INFO.Seid.Copy();
					}
					this.TempBagList.Add(item_INFO2);
				}
			}
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x001E619C File Offset: 0x001E439C
		protected override bool FiddlerItem(BaseItem baseItem)
		{
			return this.FiddlerAction(baseItem);
		}

		// Token: 0x040048B7 RID: 18615
		public Func<BaseItem, bool> FiddlerAction;
	}
}
