using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x02000463 RID: 1123
public class LianDanShu : MonoBehaviour
{
	// Token: 0x06002340 RID: 9024 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002341 RID: 9025 RVA: 0x000F1B1C File Offset: 0x000EFD1C
	public void Inputcheng()
	{
		int num;
		if (int.TryParse(this.uIInput.value, out num))
		{
			this.Num = num;
			if (this.Num > this.max)
			{
				this.Num = this.max;
				this.uIInput.value = this.max.ToString();
			}
		}
	}

	// Token: 0x06002342 RID: 9026 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public void show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06002343 RID: 9027 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002344 RID: 9028 RVA: 0x000F1B74 File Offset: 0x000EFD74
	public void ok()
	{
		if (this.Num <= 0 || this.Num > this.max)
		{
			return;
		}
		LianDanMag.instence.getDanFang();
		this.close();
	}

	// Token: 0x06002345 RID: 9029 RVA: 0x000F1BA0 File Offset: 0x000EFDA0
	private void Update()
	{
		int num = 100000;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		using (List<ItemCellEX>.Enumerator enumerator = LianDanMag.instence.itemCells.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ItemCellEX _temp = enumerator.Current;
				int itemID = LianDanMag.instence.inventoryCaiLiao.inventory[int.Parse(_temp.name)].itemID;
				if (itemID > 0)
				{
					ITEM_INFO item_INFO = Tools.instance.getPlayer().itemList.values.Find((ITEM_INFO item) => item.itemId == LianDanMag.instence.inventoryCaiLiao.inventory[int.Parse(_temp.name)].itemID);
					if (item_INFO != null)
					{
						if (dictionary.ContainsKey(itemID))
						{
							Dictionary<int, int> dictionary2 = dictionary;
							int key = itemID;
							dictionary2[key] += LianDanMag.instence.inventoryCaiLiao.inventory[int.Parse(_temp.name)].itemNum;
						}
						else
						{
							dictionary[itemID] = LianDanMag.instence.inventoryCaiLiao.inventory[int.Parse(_temp.name)].itemNum;
						}
						int num2 = (int)(item_INFO.itemCount / (uint)dictionary[itemID]);
						if (num2 < num)
						{
							num = num2;
						}
					}
				}
			}
		}
		if (num != 100000)
		{
			this.max = num;
		}
		int num3;
		if (int.TryParse(this.uIInput.value, out num3))
		{
			if (this.Num != num3)
			{
				this.uIInput.value = this.Num.ToString();
				return;
			}
		}
		else
		{
			this.uIInput.value = "1";
		}
	}

	// Token: 0x06002346 RID: 9030 RVA: 0x000F1D70 File Offset: 0x000EFF70
	public void add(int num)
	{
		this.Num += num;
		if (this.Num > this.max)
		{
			this.Num = this.max;
		}
	}

	// Token: 0x06002347 RID: 9031 RVA: 0x000F1D9A File Offset: 0x000EFF9A
	public void reduce(int num)
	{
		this.Num -= num;
		if (this.Num < 1)
		{
			this.Num = 1;
		}
	}

	// Token: 0x06002348 RID: 9032 RVA: 0x000F1DBA File Offset: 0x000EFFBA
	public void add1()
	{
		this.add(1);
	}

	// Token: 0x06002349 RID: 9033 RVA: 0x000F1DC3 File Offset: 0x000EFFC3
	public void add5()
	{
		this.add(5);
	}

	// Token: 0x0600234A RID: 9034 RVA: 0x000F1DCC File Offset: 0x000EFFCC
	public void addMax()
	{
		this.Num = this.max;
	}

	// Token: 0x0600234B RID: 9035 RVA: 0x000F1DDA File Offset: 0x000EFFDA
	public void reduce1()
	{
		this.reduce(1);
	}

	// Token: 0x0600234C RID: 9036 RVA: 0x000F1DE3 File Offset: 0x000EFFE3
	public void reduce5()
	{
		this.reduce(5);
	}

	// Token: 0x0600234D RID: 9037 RVA: 0x000F1DEC File Offset: 0x000EFFEC
	public void reduceMin()
	{
		this.Num = 1;
	}

	// Token: 0x04001C61 RID: 7265
	public int Num = 1;

	// Token: 0x04001C62 RID: 7266
	public int max;

	// Token: 0x04001C63 RID: 7267
	public UIInput uIInput;

	// Token: 0x04001C64 RID: 7268
	public UILabel uiLabel;
}
