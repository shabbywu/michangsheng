using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

public class LianDanShu : MonoBehaviour
{
	public int Num = 1;

	public int max;

	public UIInput uIInput;

	public UILabel uiLabel;

	private void Start()
	{
	}

	public void Inputcheng()
	{
		if (int.TryParse(uIInput.value, out var result))
		{
			Num = result;
			if (Num > max)
			{
				Num = max;
				uIInput.value = max.ToString();
			}
		}
	}

	public void show()
	{
		((Component)this).gameObject.SetActive(true);
	}

	public void close()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void ok()
	{
		if (Num > 0 && Num <= max)
		{
			LianDanMag.instence.getDanFang();
			close();
		}
	}

	private void Update()
	{
		int num = 100000;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (ItemCellEX _temp in LianDanMag.instence.itemCells)
		{
			int itemID = LianDanMag.instence.inventoryCaiLiao.inventory[int.Parse(((Object)_temp).name)].itemID;
			if (itemID <= 0)
			{
				continue;
			}
			ITEM_INFO iTEM_INFO = Tools.instance.getPlayer().itemList.values.Find((ITEM_INFO item) => item.itemId == LianDanMag.instence.inventoryCaiLiao.inventory[int.Parse(((Object)_temp).name)].itemID);
			if (iTEM_INFO != null)
			{
				if (dictionary.ContainsKey(itemID))
				{
					dictionary[itemID] += LianDanMag.instence.inventoryCaiLiao.inventory[int.Parse(((Object)_temp).name)].itemNum;
				}
				else
				{
					dictionary[itemID] = LianDanMag.instence.inventoryCaiLiao.inventory[int.Parse(((Object)_temp).name)].itemNum;
				}
				int num2 = (int)iTEM_INFO.itemCount / dictionary[itemID];
				if (num2 < num)
				{
					num = num2;
				}
			}
		}
		if (num != 100000)
		{
			max = num;
		}
		if (int.TryParse(uIInput.value, out var result))
		{
			if (Num != result)
			{
				uIInput.value = Num.ToString();
			}
		}
		else
		{
			uIInput.value = "1";
		}
	}

	public void add(int num)
	{
		Num += num;
		if (Num > max)
		{
			Num = max;
		}
	}

	public void reduce(int num)
	{
		Num -= num;
		if (Num < 1)
		{
			Num = 1;
		}
	}

	public void add1()
	{
		add(1);
	}

	public void add5()
	{
		add(5);
	}

	public void addMax()
	{
		Num = max;
	}

	public void reduce1()
	{
		reduce(1);
	}

	public void reduce5()
	{
		reduce(5);
	}

	public void reduceMin()
	{
		Num = 1;
	}
}
