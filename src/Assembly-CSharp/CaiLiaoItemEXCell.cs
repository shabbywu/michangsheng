using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000465 RID: 1125
public class CaiLiaoItemEXCell : ItemCellEX
{
	// Token: 0x06001E36 RID: 7734 RVA: 0x0001916C File Offset: 0x0001736C
	public override void PCOnPress()
	{
		if (this.isCanClick())
		{
			this.putCaiLiao();
		}
	}

	// Token: 0x06001E37 RID: 7735 RVA: 0x000042DD File Offset: 0x000024DD
	private void putCaiLiao()
	{
	}

	// Token: 0x06001E38 RID: 7736 RVA: 0x0001841D File Offset: 0x0001661D
	private bool isCanClick()
	{
		return (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) && this.Item.itemName != null;
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x0010081C File Offset: 0x000FEA1C
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

	// Token: 0x06001E3A RID: 7738 RVA: 0x00106ED8 File Offset: 0x001050D8
	private void EXCaiLiao(ref item Item1, ref item Item2)
	{
		item item = Item1.Clone();
		item.itemNum = 1;
		int num = Item1.itemNum;
		num--;
		if (num == 0)
		{
			Item1 = new item();
			Item2 = item;
			return;
		}
		Item1.itemNum = num;
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x0001843F File Offset: 0x0001663F
	public override void PCOnHover(bool isOver)
	{
		base.PCOnHover(isOver);
	}
}
