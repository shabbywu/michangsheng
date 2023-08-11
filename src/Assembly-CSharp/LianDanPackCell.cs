using GUIPackage;
using UnityEngine;
using UnityEngine.Events;

public class LianDanPackCell : ItemCellEX
{
	private float selectSum;

	public override void PCOnPress()
	{
		if (isCanClick())
		{
			if (Item.itemNum == 1)
			{
				putCaiLiao(1);
			}
			else
			{
				openSelectSum();
			}
		}
	}

	private void openSelectSum()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		if (Item.itemNum > 0)
		{
			GameObject val = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SumSelect"), LianDanSystemManager.inst.sumSelectTransform);
			SumSelectManager sumSelectManager = val.GetComponent<SumSelectManager>();
			LianDanSystemManager.inst.selectLianDanCaiLiaoPage.clickMask();
			inventory.showTooltip = false;
			sumSelectManager.showSelect("置入", Item.itemID, Item.itemNum, (UnityAction)delegate
			{
				selectSum = sumSelectManager.itemSum;
				putCaiLiao();
			}, null);
		}
	}

	private void putCaiLiao(int num = -1)
	{
		if (num > 0)
		{
			selectSum = num;
		}
		int curSelectIndex = LianDanSystemManager.inst.selectLianDanCaiLiaoPage.getCurSelectIndex();
		if (curSelectIndex == -1 || selectSum < 1f || (float)Item.itemNum < selectSum)
		{
			return;
		}
		inventory.showTooltip = false;
		item item = Item.Clone();
		if (jsonData.instance.ItemJsonData[Item.itemID.ToString()]["type"].I == 6)
		{
			if (curSelectIndex - 25 > 4)
			{
				return;
			}
		}
		else if (curSelectIndex - 25 != 5)
		{
			return;
		}
		item.itemNum = (int)selectSum;
		Item.itemNum -= (int)selectSum;
		inventory.inventory[curSelectIndex] = item;
		LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList[curSelectIndex - 25].updateItem();
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.CloseCaiLiaoPackge();
		LianDanSystemManager.inst.putLianDanCaiLiaoCallBack();
		if (LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList[curSelectIndex - 25].itemType == ItemTypes.丹炉)
		{
			LianDanSystemManager.inst.putDanLuCallBack();
			if (((Component)LianDanSystemManager.inst.putDanLuManager).gameObject.activeSelf)
			{
				((Component)LianDanSystemManager.inst.putDanLuManager).gameObject.SetActive(false);
			}
			if (!((Component)LianDanSystemManager.inst.lianDanPageManager).gameObject.activeSelf)
			{
				((Component)LianDanSystemManager.inst.lianDanPageManager).gameObject.SetActive(true);
			}
		}
		selectSum = 0f;
	}

	private bool isCanClick()
	{
		if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
		{
			return Item.itemName != null;
		}
		return false;
	}

	private new void Update()
	{
		base.Update();
		if (Item.itemID != -1)
		{
			string str = jsonData.instance.ItemJsonData[Item.itemID.ToString()]["name"].str;
			KeyObject.SetActive(true);
			KeyName.text = Tools.Code64(str);
		}
		else
		{
			KeyObject.SetActive(false);
		}
	}

	public override void PCOnHover(bool isOver)
	{
		base.PCOnHover(isOver);
	}
}
