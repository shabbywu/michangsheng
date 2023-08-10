using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;

namespace script.ExchangeMeeting.UI.Base;

public class ExchangeBagB : ExchangeBag
{
	public Func<BaseItem, bool> FiddlerAction;

	public override void CreateTempList()
	{
		TempBagList = new List<ITEM_INFO>();
		foreach (ITEM_INFO value in PlayerEx.Player.itemList.values)
		{
			if (value.itemCount != 0 && _ItemJsonData.DataDict.ContainsKey(value.itemId) && _ItemJsonData.DataDict[value.itemId].CanSale != 1)
			{
				ITEM_INFO iTEM_INFO = new ITEM_INFO();
				iTEM_INFO.itemId = value.itemId;
				iTEM_INFO.itemCount = value.itemCount;
				iTEM_INFO.uuid = value.uuid;
				if (value.Seid != null)
				{
					iTEM_INFO.Seid = value.Seid.Copy();
				}
				TempBagList.Add(iTEM_INFO);
			}
		}
	}

	protected override bool FiddlerItem(BaseItem baseItem)
	{
		return FiddlerAction(baseItem);
	}
}
