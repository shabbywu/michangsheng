using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000517 RID: 1303
public class TpEatDanYao : MonoBehaviour
{
	// Token: 0x06002191 RID: 8593 RVA: 0x001184EC File Offset: 0x001166EC
	public void Init()
	{
		if (!this.isInit)
		{
			this.isInit = true;
			Avatar player = Tools.instance.getPlayer();
			foreach (int num in jsonData.instance.NPCTuPuoDate[player.level.ToString()]["ShouJiItem"].ToList())
			{
				GameObject gameObject = Tools.InstantiateGameObject(this.danYaoCell, this.danYaoList);
				UIIconShow cell = gameObject.GetComponent<UIIconShow>();
				cell.SetItem(num);
				cell.OnClick = delegate(PointerEventData b)
				{
					if (b.button == 1)
					{
						this.UserItem(cell);
					}
				};
				if (this.IsNaiYao(num))
				{
					cell.NowJiaoBiao = UIIconShow.JiaoBiaoType.Nai;
				}
				cell.SetCount(this.GetPlayerItemNum(num));
				cell.CanDrag = false;
				gameObject.SetActive(true);
			}
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x0001B963 File Offset: 0x00019B63
	public void Close()
	{
		base.gameObject.SetActive(false);
		TpUIMag.inst.ShowTuPoPanel();
	}

	// Token: 0x06002193 RID: 8595 RVA: 0x00118618 File Offset: 0x00116818
	public int GetPlayerItemNum(int itemId)
	{
		List<ITEM_INFO> values = Tools.instance.getPlayer().itemList.values;
		int num = 0;
		foreach (ITEM_INFO item_INFO in values)
		{
			if (item_INFO.itemId == itemId && item_INFO.itemCount > 0U)
			{
				num += (int)item_INFO.itemCount;
				break;
			}
		}
		return num;
	}

	// Token: 0x06002194 RID: 8596 RVA: 0x00118694 File Offset: 0x00116894
	public void UserItem(UIIconShow item)
	{
		int curCount = item.GetCount();
		if (curCount > 0)
		{
			item.tmpItem.gongneng(delegate
			{
				item.SetCount(curCount - 1);
				Tools.instance.getPlayer().removeItem(item.tmpItem.itemID, 1);
			}, false);
			if (this.IsNaiYao(item.tmpItem.itemID))
			{
				item.NowJiaoBiao = UIIconShow.JiaoBiaoType.Nai;
				return;
			}
		}
		else
		{
			UIPopTip.Inst.Pop("丹药数量不足", PopTipIconType.叹号);
		}
	}

	// Token: 0x06002195 RID: 8597 RVA: 0x0011871C File Offset: 0x0011691C
	private bool IsNaiYao(int itemID)
	{
		int itemCanUseNum = item.GetItemCanUseNum(itemID);
		return Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(itemID)) >= itemCanUseNum;
	}

	// Token: 0x04001D15 RID: 7445
	[SerializeField]
	private GameObject danYaoCell;

	// Token: 0x04001D16 RID: 7446
	[SerializeField]
	private Transform danYaoList;

	// Token: 0x04001D17 RID: 7447
	private bool isInit;
}
