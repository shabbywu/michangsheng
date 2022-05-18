using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000444 RID: 1092
public class LianDanPackCell : ItemCellEX
{
	// Token: 0x06001D17 RID: 7447 RVA: 0x000183F7 File Offset: 0x000165F7
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

	// Token: 0x06001D18 RID: 7448 RVA: 0x001005CC File Offset: 0x000FE7CC
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

	// Token: 0x06001D19 RID: 7449 RVA: 0x00100674 File Offset: 0x000FE874
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

	// Token: 0x06001D1A RID: 7450 RVA: 0x0001841D File Offset: 0x0001661D
	private bool isCanClick()
	{
		return (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) && this.Item.itemName != null;
	}

	// Token: 0x06001D1B RID: 7451 RVA: 0x0010081C File Offset: 0x000FEA1C
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

	// Token: 0x06001D1C RID: 7452 RVA: 0x0001843F File Offset: 0x0001663F
	public override void PCOnHover(bool isOver)
	{
		base.PCOnHover(isOver);
	}

	// Token: 0x04001910 RID: 6416
	private float selectSum;
}
