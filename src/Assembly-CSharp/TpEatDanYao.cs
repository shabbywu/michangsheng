using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000392 RID: 914
public class TpEatDanYao : MonoBehaviour
{
	// Token: 0x06001E16 RID: 7702 RVA: 0x000D48E8 File Offset: 0x000D2AE8
	public void Init()
	{
		if (!this.isInit)
		{
			this.isInit = true;
			int num = (int)Tools.instance.getPlayer().level;
			if (num >= 13)
			{
				num = 15;
			}
			foreach (int num2 in jsonData.instance.NPCTuPuoDate[num.ToString()]["ShouJiItem"].ToList())
			{
				GameObject gameObject = Tools.InstantiateGameObject(this.danYaoCell, this.danYaoList);
				UIIconShow cell = gameObject.GetComponent<UIIconShow>();
				cell.SetItem(num2);
				cell.OnClick = delegate(PointerEventData b)
				{
					if (b.button == 1)
					{
						this.UserItem(cell);
					}
				};
				if (this.IsNaiYao(num2))
				{
					cell.NowJiaoBiao = UIIconShow.JiaoBiaoType.Nai;
				}
				cell.SetCount(this.GetPlayerItemNum(num2));
				cell.CanDrag = false;
				gameObject.SetActive(true);
			}
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x000D4A1C File Offset: 0x000D2C1C
	public void Close()
	{
		base.gameObject.SetActive(false);
		TpUIMag.inst.ShowTuPoPanel();
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x000D4A34 File Offset: 0x000D2C34
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

	// Token: 0x06001E19 RID: 7705 RVA: 0x000D4AB0 File Offset: 0x000D2CB0
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

	// Token: 0x06001E1A RID: 7706 RVA: 0x000D4B38 File Offset: 0x000D2D38
	private bool IsNaiYao(int itemID)
	{
		int itemCanUseNum = item.GetItemCanUseNum(itemID);
		return Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(itemID)) >= itemCanUseNum;
	}

	// Token: 0x040018B5 RID: 6325
	[SerializeField]
	private GameObject danYaoCell;

	// Token: 0x040018B6 RID: 6326
	[SerializeField]
	private Transform danYaoList;

	// Token: 0x040018B7 RID: 6327
	private bool isInit;
}
