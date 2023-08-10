using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TpEatDanYao : MonoBehaviour
{
	[SerializeField]
	private GameObject danYaoCell;

	[SerializeField]
	private Transform danYaoList;

	private bool isInit;

	public void Init()
	{
		if (!isInit)
		{
			isInit = true;
			int num = Tools.instance.getPlayer().level;
			if (num >= 13)
			{
				num = 15;
			}
			foreach (int item in jsonData.instance.NPCTuPuoDate[num.ToString()]["ShouJiItem"].ToList())
			{
				GameObject val = Tools.InstantiateGameObject(danYaoCell, danYaoList);
				UIIconShow cell = val.GetComponent<UIIconShow>();
				cell.SetItem(item);
				cell.OnClick = delegate(PointerEventData b)
				{
					//IL_0001: Unknown result type (might be due to invalid IL or missing references)
					//IL_0007: Invalid comparison between Unknown and I4
					if ((int)b.button == 1)
					{
						UserItem(cell);
					}
				};
				if (IsNaiYao(item))
				{
					cell.NowJiaoBiao = UIIconShow.JiaoBiaoType.Nai;
				}
				cell.SetCount(GetPlayerItemNum(item));
				cell.CanDrag = false;
				val.SetActive(true);
			}
		}
		((Component)this).gameObject.SetActive(true);
	}

	public void Close()
	{
		((Component)this).gameObject.SetActive(false);
		TpUIMag.inst.ShowTuPoPanel();
	}

	public int GetPlayerItemNum(int itemId)
	{
		List<ITEM_INFO> values = Tools.instance.getPlayer().itemList.values;
		int num = 0;
		foreach (ITEM_INFO item in values)
		{
			if (item.itemId == itemId && item.itemCount != 0)
			{
				num += (int)item.itemCount;
				break;
			}
		}
		return num;
	}

	public void UserItem(UIIconShow item)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		int curCount = item.GetCount();
		if (curCount > 0)
		{
			item.tmpItem.gongneng((UnityAction)delegate
			{
				item.SetCount(curCount - 1);
				Tools.instance.getPlayer().removeItem(item.tmpItem.itemID, 1);
			});
			if (IsNaiYao(item.tmpItem.itemID))
			{
				item.NowJiaoBiao = UIIconShow.JiaoBiaoType.Nai;
			}
		}
		else
		{
			UIPopTip.Inst.Pop("丹药数量不足");
		}
	}

	private bool IsNaiYao(int itemID)
	{
		int itemCanUseNum = item.GetItemCanUseNum(itemID);
		if (Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(itemID)) >= itemCanUseNum)
		{
			return true;
		}
		return false;
	}
}
