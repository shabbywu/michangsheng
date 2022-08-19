using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class CaiLiaoItemEXCell : ItemCellEX
{
	// Token: 0x06001B10 RID: 6928 RVA: 0x000C15F3 File Offset: 0x000BF7F3
	public override void PCOnPress()
	{
		if (this.isCanClick())
		{
			this.putCaiLiao();
		}
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x00004095 File Offset: 0x00002295
	private void putCaiLiao()
	{
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x000BA023 File Offset: 0x000B8223
	private bool isCanClick()
	{
		return (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) && this.Item.itemName != null;
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x000C1604 File Offset: 0x000BF804
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

	// Token: 0x06001B14 RID: 6932 RVA: 0x000C1680 File Offset: 0x000BF880
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

	// Token: 0x06001B15 RID: 6933 RVA: 0x000BA0C2 File Offset: 0x000B82C2
	public override void PCOnHover(bool isOver)
	{
		base.PCOnHover(isOver);
	}
}
