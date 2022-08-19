using System;
using GUIPackage;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class LianDanPackCell : ItemCellEX
{
	// Token: 0x060019F5 RID: 6645 RVA: 0x000B9DAE File Offset: 0x000B7FAE
	public override void PCOnPress()
	{
		if (this.isCanClick())
		{
			if (this.Item.itemNum == 1)
			{
				this.putCaiLiao(1);
				return;
			}
			this.openSelectSum();
		}
	}

	// Token: 0x060019F6 RID: 6646 RVA: 0x000B9DD4 File Offset: 0x000B7FD4
	private void openSelectSum()
	{
		if (this.Item.itemNum > 0)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SumSelect"), LianDanSystemManager.inst.sumSelectTransform);
			SumSelectManager sumSelectManager = gameObject.GetComponent<SumSelectManager>();
			LianDanSystemManager.inst.selectLianDanCaiLiaoPage.clickMask();
			this.inventory.showTooltip = false;
			sumSelectManager.showSelect("置入", this.Item.itemID, (float)this.Item.itemNum, delegate
			{
				this.selectSum = sumSelectManager.itemSum;
				this.putCaiLiao(-1);
			}, null, SumSelectManager.SpecialType.空);
		}
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x000B9E7C File Offset: 0x000B807C
	private void putCaiLiao(int num = -1)
	{
		if (num > 0)
		{
			this.selectSum = (float)num;
		}
		int curSelectIndex = LianDanSystemManager.inst.selectLianDanCaiLiaoPage.getCurSelectIndex();
		if (curSelectIndex == -1 || this.selectSum < 1f || (float)this.Item.itemNum < this.selectSum)
		{
			return;
		}
		this.inventory.showTooltip = false;
		item item = this.Item.Clone();
		if (jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["type"].I == 6)
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
		item.itemNum = (int)this.selectSum;
		this.Item.itemNum = this.Item.itemNum - (int)this.selectSum;
		this.inventory.inventory[curSelectIndex] = item;
		LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList[curSelectIndex - 25].updateItem();
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.CloseCaiLiaoPackge();
		LianDanSystemManager.inst.putLianDanCaiLiaoCallBack();
		if (LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList[curSelectIndex - 25].itemType == ItemTypes.丹炉)
		{
			LianDanSystemManager.inst.putDanLuCallBack();
			if (LianDanSystemManager.inst.putDanLuManager.gameObject.activeSelf)
			{
				LianDanSystemManager.inst.putDanLuManager.gameObject.SetActive(false);
			}
			if (!LianDanSystemManager.inst.lianDanPageManager.gameObject.activeSelf)
			{
				LianDanSystemManager.inst.lianDanPageManager.gameObject.SetActive(true);
			}
		}
		this.selectSum = 0f;
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x000BA023 File Offset: 0x000B8223
	private bool isCanClick()
	{
		return (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) && this.Item.itemName != null;
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x000BA048 File Offset: 0x000B8248
	private new void Update()
	{
		base.Update();
		if (this.Item.itemID != -1)
		{
			string str = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["name"].str;
			this.KeyObject.SetActive(true);
			this.KeyName.text = Tools.Code64(str);
			return;
		}
		this.KeyObject.SetActive(false);
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x000BA0C2 File Offset: 0x000B82C2
	public override void PCOnHover(bool isOver)
	{
		base.PCOnHover(isOver);
	}

	// Token: 0x0400150C RID: 5388
	private float selectSum;
}
