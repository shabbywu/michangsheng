using System.Collections.Generic;
using KBEngine;

namespace Bag;

public interface IBaseBag
{
	void OpenBag(List<ITEM_INFO> itemList);

	void UpdateItem();
}
