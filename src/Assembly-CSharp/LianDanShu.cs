using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x0200061E RID: 1566
public class LianDanShu : MonoBehaviour
{
	// Token: 0x060026F4 RID: 9972 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x00131E80 File Offset: 0x00130080
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

	// Token: 0x060026F6 RID: 9974 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060026F7 RID: 9975 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060026F8 RID: 9976 RVA: 0x0001EF57 File Offset: 0x0001D157
	public void ok()
	{
		if (this.Num <= 0 || this.Num > this.max)
		{
			return;
		}
		LianDanMag.instence.getDanFang();
		this.close();
	}

	// Token: 0x060026F9 RID: 9977 RVA: 0x00131ED8 File Offset: 0x001300D8
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

	// Token: 0x060026FA RID: 9978 RVA: 0x0001EF82 File Offset: 0x0001D182
	public void add(int num)
	{
		this.Num += num;
		if (this.Num > this.max)
		{
			this.Num = this.max;
		}
	}

	// Token: 0x060026FB RID: 9979 RVA: 0x0001EFAC File Offset: 0x0001D1AC
	public void reduce(int num)
	{
		this.Num -= num;
		if (this.Num < 1)
		{
			this.Num = 1;
		}
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x0001EFCC File Offset: 0x0001D1CC
	public void add1()
	{
		this.add(1);
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x0001EFD5 File Offset: 0x0001D1D5
	public void add5()
	{
		this.add(5);
	}

	// Token: 0x060026FE RID: 9982 RVA: 0x0001EFDE File Offset: 0x0001D1DE
	public void addMax()
	{
		this.Num = this.max;
	}

	// Token: 0x060026FF RID: 9983 RVA: 0x0001EFEC File Offset: 0x0001D1EC
	public void reduce1()
	{
		this.reduce(1);
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x0001EFF5 File Offset: 0x0001D1F5
	public void reduce5()
	{
		this.reduce(5);
	}

	// Token: 0x06002701 RID: 9985 RVA: 0x0001EFFE File Offset: 0x0001D1FE
	public void reduceMin()
	{
		this.Num = 1;
	}

	// Token: 0x04002135 RID: 8501
	public int Num = 1;

	// Token: 0x04002136 RID: 8502
	public int max;

	// Token: 0x04002137 RID: 8503
	public UIInput uIInput;

	// Token: 0x04002138 RID: 8504
	public UILabel uiLabel;
}
