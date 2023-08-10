using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

public class LianDanMag : MonoBehaviour
{
	public class DanyaoItem
	{
		public int ItemID = -1;

		public int ItemNum;

		public int YaoZhi;

		public int YaoZhiType;
	}

	public Inventory2 inventoryCaiLiao;

	public Inventory2 inventoryDanlu;

	public Inventory2 InventoryShowDanlu;

	public Inventory2 InventoryFinish;

	public List<ItemCellEX> itemCells;

	public static LianDanMag instence;

	public List<List<int>> duiying;

	public List<int> YaoZhi;

	public List<int> ReduceNaijiu;

	public List<int> MaxCaoYao;

	public LianDanDanFang lianDanDanFang;

	public LianDanFinsh zhaLu;

	public LianDanFinsh finshPlan;

	public GameObject DanLuPlan;

	public GameObject AddDanluBtn;

	public GameObject XuanZeDanLu;

	public GameObject CaiLiaoItem;

	public LianDanShu LianDanShu;

	public GameObject DanfunPlan;

	public GameObject YaoCaiPlan;

	private void Awake()
	{
		instence = this;
		duiying = new List<List<int>>();
		duiying.Add(new List<int> { 0, 1, 3 });
		duiying.Add(new List<int> { 0, 1, 3, 4 });
		duiying.Add(new List<int> { 0, 1, 3, 4 });
		duiying.Add(new List<int> { 0, 1, 3, 4 });
		duiying.Add(new List<int> { 0, 1, 3, 4 });
		duiying.Add(new List<int> { 0, 1, 2, 3, 4 });
		for (int i = 0; i < (int)inventoryCaiLiao.count; i++)
		{
			inventoryCaiLiao.inventory.Add(new item());
		}
		for (int j = 0; j < (int)inventoryDanlu.count; j++)
		{
			inventoryDanlu.inventory.Add(new item());
		}
		for (int k = 0; k < (int)InventoryShowDanlu.count; k++)
		{
			InventoryShowDanlu.inventory.Add(new item());
		}
		for (int l = 0; l < (int)InventoryFinish.count; l++)
		{
			InventoryFinish.inventory.Add(new item());
		}
		show();
	}

	private void OnDestroy()
	{
		instence = null;
	}

	private void Start()
	{
	}

	public void OpenLianDanMag()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).gameObject.SetActive(true);
	}

	public void show()
	{
		closeDanlu();
		XuanZeDanLu.SetActive(false);
	}

	public void PlayDanfangIn()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (DanfunPlan.transform.localPosition.x < -800f)
		{
			DanfunPlan.GetComponent<Animation>().Play("Danfangout");
		}
		else
		{
			DanfunPlan.GetComponent<Animation>().Play("Danfang");
		}
	}

	public void PlayYaoCai()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (YaoCaiPlan.transform.localPosition.x > 50f)
		{
			YaoCaiPlan.GetComponent<Animation>().Play("yaocaiout");
		}
		else
		{
			YaoCaiPlan.GetComponent<Animation>().Play("yaocaiin");
		}
	}

	public void showChoiceDanLu()
	{
		XuanZeDanLu.SetActive(true);
		((Component)LianDanShu).gameObject.SetActive(false);
		((Component)finshPlan).gameObject.SetActive(false);
	}

	public void CloseChoiceDanlu()
	{
		XuanZeDanLu.SetActive(false);
	}

	public bool DanLuIsFull()
	{
		bool result = true;
		for (int i = 0; i < 5; i++)
		{
			if (!itemCells[i].JustShow && inventoryCaiLiao.inventory[i + 24].itemID <= 0)
			{
				result = false;
			}
		}
		return result;
	}

	public void showDanlu()
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		AddDanluBtn.SetActive(false);
		int itemID = InventoryShowDanlu.inventory[0].itemID;
		int num = (int)jsonData.instance.ItemJsonData[itemID.ToString()]["quality"].n;
		int num2 = 0;
		foreach (Transform item in CaiLiaoItem.transform)
		{
			Transform val = item;
			ItemCellEX component = ((Component)val).GetComponent<ItemCellEX>();
			if (duiying[num - 1].Contains(num2))
			{
				component.JustShow = false;
				((Component)val.Find("Background")).GetComponent<UISprite>().spriteName = "liandna (22)";
			}
			else
			{
				component.JustShow = true;
				((Component)val.Find("Background")).GetComponent<UISprite>().spriteName = "liandna (23)";
			}
			num2++;
		}
		DanLuPlan.SetActive(true);
	}

	public void closeDanlu()
	{
		AddDanluBtn.SetActive(true);
		DanLuPlan.SetActive(false);
	}

	public void StartLianDan()
	{
		XuanZeDanLu.SetActive(false);
		bool flag = false;
		int num = 0;
		foreach (ItemCellEX itemCell in instence.itemCells)
		{
			int itemID = instence.inventoryCaiLiao.inventory[int.Parse(((Object)itemCell).name)].itemID;
			if (itemID > 0)
			{
				flag = true;
			}
			if (itemID > 0)
			{
				num += inventoryCaiLiao.inventory[int.Parse(((Object)itemCell).name)].itemNum;
			}
		}
		int num2 = (int)jsonData.instance.ItemJsonData[InventoryShowDanlu.inventory[0].itemID.ToString()]["quality"].n;
		if (num > MaxCaoYao[num2 - 1])
		{
			UIPopTip.Inst.Pop("该品阶丹炉最大药材数" + MaxCaoYao[num2 - 1] + "个");
		}
		else if (flag)
		{
			LianDanShu.show();
		}
	}

	public void GetYaoLeiList(List<int> indexToLeixin, List<DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
	{
		int num = 0;
		foreach (ItemCellEX itemCell in itemCells)
		{
			DanyaoItem danyaoItem = new DanyaoItem();
			danyaoItem.ItemID = inventoryCaiLiao.inventory[int.Parse(((Object)itemCell).name)].itemID;
			danyaoItem.ItemNum = inventoryCaiLiao.inventory[int.Parse(((Object)itemCell).name)].itemNum;
			if (danyaoItem.ItemID != -1)
			{
				JSONObject jSONObject = jsonData.instance.ItemJsonData[danyaoItem.ItemID.ToString()];
				int yaoZhiType = (int)jSONObject["yaoZhi" + indexToLeixin[num]].n;
				int num2 = (int)jSONObject["quality"].n;
				danyaoItem.YaoZhi = danyaoItem.ItemNum * YaoZhi[num2 - 1];
				danyaoItem.YaoZhiType = yaoZhiType;
				switch (num)
				{
				case 1:
				case 2:
					Tools.dictionaryAddNum(zhuyaoList, danyaoItem.YaoZhiType, danyaoItem.YaoZhi);
					break;
				case 3:
				case 4:
					Tools.dictionaryAddNum(fuyaoList, danyaoItem.YaoZhiType, danyaoItem.YaoZhi);
					break;
				}
				Tools.instance.getPlayer().AddYaoCaiShuXin(danyaoItem.ItemID, indexToLeixin[num]);
			}
			else
			{
				danyaoItem.YaoZhi = 0;
				danyaoItem.YaoZhiType = -1;
			}
			DanYaoItemList.Add(danyaoItem);
			num++;
		}
	}

	public void GetDanfangList(List<JSONObject> DanFans, List<int> indexToLeixin, List<DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
	{
		foreach (JSONObject item in jsonData.instance.LianDanDanFangBiao.list)
		{
			bool flag = true;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			for (int i = 2; i <= 5; i++)
			{
				if ((int)item["value" + i].n != 0)
				{
					JSONObject jSONObject = jsonData.instance.ItemJsonData[((int)item["value" + i].n).ToString()];
					int num = (int)jSONObject["yaoZhi" + indexToLeixin[i - 1]].n;
					int num2 = (int)jSONObject["quality"].n;
					int num3 = (int)item["num" + i].n * YaoZhi[num2 - 1];
					int key = num;
					switch (i)
					{
					case 2:
					case 3:
						Tools.dictionaryAddNum(dictionary2, key, num3);
						break;
					case 4:
					case 5:
						Tools.dictionaryAddNum(dictionary, key, num3);
						break;
					}
				}
			}
			foreach (KeyValuePair<int, int> item2 in dictionary)
			{
				if (!fuyaoList.ContainsKey(item2.Key) || fuyaoList[item2.Key] < item2.Value)
				{
					flag = false;
				}
			}
			foreach (KeyValuePair<int, int> item3 in dictionary2)
			{
				if (!zhuyaoList.ContainsKey(item3.Key) || zhuyaoList[item3.Key] < item3.Value)
				{
					flag = false;
				}
			}
			if (flag)
			{
				DanFans.Add(item);
			}
		}
	}

	public void GetDanFang(out int maxNum, out int maxpingzhi, out JSONObject danFangItemID, List<JSONObject> DanFans, List<int> indexToLeixin, List<DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
	{
		maxpingzhi = 0;
		maxNum = 0;
		danFangItemID = null;
		foreach (JSONObject DanFan in DanFans)
		{
			int i = jsonData.instance.ItemJsonData[DanFan["ItemID"].I.ToString()]["quality"].I;
			Debug.Log((object)("丹方ID" + DanFan["ItemID"].I));
			if (i > maxpingzhi)
			{
				maxNum = 0;
				maxpingzhi = i;
				danFangItemID = DanFan;
			}
			if (i == maxpingzhi)
			{
				maxNum++;
			}
		}
	}

	public int getDanFang()
	{
		List<DanyaoItem> list = new List<DanyaoItem>();
		List<int> list2 = new List<int> { 1, 2, 2, 3, 3 };
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		GetYaoLeiList(list2, list, dictionary, dictionary2);
		List<JSONObject> danFans = new List<JSONObject>();
		GetDanfangList(danFans, list2, list, dictionary, dictionary2);
		int maxpingzhi = 0;
		int maxNum = 0;
		JSONObject danFangItemID = null;
		GetDanFang(out maxNum, out maxpingzhi, out danFangItemID, danFans, list2, list, dictionary, dictionary2);
		Avatar player = Tools.instance.getPlayer();
		int num = LianDanShu.Num;
		List<int> list3 = new List<int> { 3, 4, 5, 6, 7, 8 };
		if (maxpingzhi > 0)
		{
			player.AddTime(list3[maxpingzhi - 1] * num);
		}
		else
		{
			player.AddTime(3 * num);
		}
		foreach (ItemCellEX itemCell in itemCells)
		{
			if (inventoryCaiLiao.inventory[int.Parse(((Object)itemCell).name)].itemID != -1)
			{
				player.removeItem(inventoryCaiLiao.inventory[int.Parse(((Object)itemCell).name)].UUID, inventoryCaiLiao.inventory[int.Parse(((Object)itemCell).name)].itemNum * num);
			}
		}
		InventoryFinish.resteAllInventoryItem();
		if (InventoryShowDanlu.inventory[0].itemID != -1)
		{
			int num2 = (int)jsonData.instance.ItemJsonData[InventoryShowDanlu.inventory[0].itemID.ToString()]["quality"].n;
			int num3 = 0;
			num3 = maxpingzhi;
			int num4 = 0;
			if (num3 - num2 == 0)
			{
				num4 = ReduceNaijiu[1];
			}
			else if (num3 - num2 == 1)
			{
				num4 = ReduceNaijiu[2];
			}
			else if (num3 - num2 == 2)
			{
				num4 = ReduceNaijiu[3];
			}
			else if (num3 - num2 >= 3)
			{
				num4 = ReduceNaijiu[4];
			}
			else if (num3 - num2 < 0)
			{
				num4 = ReduceNaijiu[0];
			}
			if (maxpingzhi <= 0)
			{
				num4 = 2;
			}
			num4 *= num;
			if (num2 > num3 && player.getStaticSkillAddSum(13) != 0)
			{
				num4 = 0;
			}
			List<int> list4 = new List<int> { 30, 40, 120, 200, 250, 300 };
			if (!InventoryShowDanlu.inventory[0].Seid.HasField("NaiJiu"))
			{
				UIPopTip.Inst.Pop("丹炉错误");
				return -1;
			}
			int num5 = (int)InventoryShowDanlu.inventory[0].Seid["NaiJiu"].n;
			if (num5 - num4 <= 0)
			{
				zhaLu.SHowZhalu();
				player.removeItem(InventoryShowDanlu.inventory[0].UUID);
				inventoryCaiLiao.resteAllInventoryItem();
				inventoryCaiLiao.LoadInventory();
				inventoryDanlu.resteAllInventoryItem();
				inventoryDanlu.LoadInventory();
				closeDanlu();
				int num6 = (int)jsonData.instance.ItemJsonData[InventoryShowDanlu.inventory[0].itemID.ToString()]["quality"].n;
				player.AllMapAddHP(-list4[num6 - 1], DeathType.炉毁人亡);
				return -1;
			}
			foreach (ITEM_INFO value in player.itemList.values)
			{
				if (value.uuid == InventoryShowDanlu.inventory[0].UUID)
				{
					value.Seid.SetField("NaiJiu", num5 - num4);
				}
			}
		}
		ItemDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
		Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
		if (danFangItemID != null)
		{
			Dictionary<int, int> dictionary4 = new Dictionary<int, int>();
			Dictionary<int, int> dictionary5 = new Dictionary<int, int>();
			for (int i = 1; i <= 5; i++)
			{
				if (list[i - 1].ItemID <= 0 || (int)danFangItemID["value" + i].n == 0)
				{
					continue;
				}
				JSONObject jSONObject = jsonData.instance.ItemJsonData[((int)danFangItemID["value" + i].n).ToString()];
				int num7 = (int)jSONObject["yaoZhi" + list2[i - 1]].n;
				int num8 = (int)jSONObject["quality"].n;
				int num9 = (int)danFangItemID["num" + i].n * YaoZhi[num8 - 1];
				int key = num7;
				switch (i)
				{
				case 1:
					if (list[i - 1].YaoZhi - num9 > 0)
					{
						dictionary3[list[i - 1].YaoZhiType] = list[i - 1].YaoZhi - num9;
					}
					break;
				case 2:
				case 3:
					Tools.dictionaryAddNum(dictionary5, key, num9);
					break;
				case 4:
				case 5:
					Tools.dictionaryAddNum(dictionary4, key, num9);
					break;
				}
			}
			foreach (KeyValuePair<int, int> item in dictionary)
			{
				if (dictionary4.ContainsKey(item.Key) && item.Value > dictionary4[item.Key])
				{
					Tools.dictionaryAddNum(dictionary3, item.Key, item.Value - dictionary4[item.Key]);
				}
			}
			foreach (KeyValuePair<int, int> item2 in dictionary2)
			{
				if (dictionary5.ContainsKey(item2.Key) && item2.Value > dictionary5[item2.Key])
				{
					Tools.dictionaryAddNum(dictionary3, item2.Key, item2.Value - dictionary5[item2.Key]);
				}
			}
		}
		else
		{
			for (int j = 1; j <= 5; j++)
			{
				if (list[j - 1].ItemID > 0)
				{
					dictionary3[list[j - 1].YaoZhiType] = list[j - 1].YaoZhi;
				}
			}
		}
		if (maxNum != 1)
		{
			if (maxpingzhi == 0)
			{
				int num10 = 0;
				foreach (KeyValuePair<int, int> item3 in dictionary3)
				{
					if (item3.Key > 0)
					{
						FinishAddItem(num10, InventoryFinish, component, item3.Key + 6900, item3.Value * num);
						num10++;
					}
				}
				finshPlan.fail(13, num);
			}
			else
			{
				FinishAddItem(0, InventoryFinish, component, maxpingzhi + 5900, num);
				player.addItem(maxpingzhi + 5900, num, Tools.CreateItemSeid(danFangItemID["ItemID"].I));
				finshPlan.fail(15, num);
			}
			setFinsh();
			return -1;
		}
		List<int> list5 = new List<int> { -1, 1, 0 };
		int num11 = -1;
		if (danFangItemID != null)
		{
			int num12 = 0;
			for (int k = 2; k <= 5; k++)
			{
				if (list[k - 1].ItemID > 0)
				{
					int num13 = (int)jsonData.instance.ItemJsonData[list[k - 1].ItemID.ToString()]["yaoZhi1"].n;
					num12 += list5[num13 - 1];
				}
			}
			if (num12 > 0)
			{
				num11 = 1;
			}
			else if (num12 == 0)
			{
				num11 = 3;
			}
			else if (num12 < 0)
			{
				num11 = 2;
			}
		}
		if (list[0].YaoZhiType != num11)
		{
			FinishAddItem(0, InventoryFinish, component, maxpingzhi + 5906, num);
			player.addItem(maxpingzhi + 5906, num, Tools.CreateItemSeid(maxpingzhi + 5906));
			finshPlan.fail(14, num);
			setFinsh();
			return -1;
		}
		JSONObject jSONObject2 = jsonData.instance.ItemJsonData[((int)danFangItemID["value1"].n).ToString()];
		_ = jSONObject2["yaoZhi1"].n;
		int num14 = (int)jSONObject2["quality"].n;
		int num15 = (int)danFangItemID["num1"].n * YaoZhi[num14 - 1];
		if (list[0].YaoZhi < num15)
		{
			FinishAddItem(0, InventoryFinish, component, maxpingzhi + 5906, num);
			player.addItem(maxpingzhi + 5906, num, Tools.CreateItemSeid(maxpingzhi + 5906));
			finshPlan.fail(14, num);
			setFinsh();
			return -1;
		}
		if (maxNum == 1 && maxpingzhi > 0)
		{
			if (danFangItemID != null)
			{
				List<int> list6 = new List<int>();
				List<int> list7 = new List<int>();
				for (int l = 1; l <= 5; l++)
				{
					list6.Add(list[l - 1].ItemID);
					list7.Add(list[l - 1].ItemNum);
				}
				player.addDanFang(danFangItemID["ItemID"].I, list6, list7);
			}
			int num16 = 0;
			if (player.getStaticSkillAddSum(17) != 0)
			{
				num *= 2;
			}
			FinishAddItem(num16, InventoryFinish, component, danFangItemID["ItemID"].I, num);
			num16++;
			foreach (KeyValuePair<int, int> item4 in dictionary3)
			{
				if (item4.Key > 0)
				{
					FinishAddItem(num16, InventoryFinish, component, item4.Key + 6900, item4.Value * num);
					num16++;
				}
			}
			player.addItem(danFangItemID["ItemID"].I, num, Tools.CreateItemSeid(danFangItemID["ItemID"].I));
			if (dictionary3.Count > 0)
			{
				finshPlan.succes(2 + (maxpingzhi - 1) * 2, num);
			}
			else
			{
				finshPlan.succes(1 + (maxpingzhi - 1) * 2, num);
			}
			try
			{
				AddWuDaoLianDan(danFangItemID["ItemID"].I, num);
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex);
			}
		}
		lianDanDanFang.InitDanFang();
		setFinsh();
		return -1;
	}

	public static void AddWuDaoLianDan(int ItemId, int Num)
	{
		JSONObject jSONObject = jsonData.instance.ItemJsonData[ItemId.ToString()];
		int i = jSONObject["quality"].I;
		int i2 = jSONObject["price"].I;
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int> { 10, 15, 30, 50, 70, 100 };
		float itemPercent = getItemPercent(i);
		if (!player.HasLianZhiDanYao.HasItem(ItemId))
		{
			player.HasLianZhiDanYao.Add(ItemId);
			player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * 4f * itemPercent));
		}
		player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * (float)Num * itemPercent));
	}

	public static float getItemPercent(int itemQuality)
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(21);
		float num = 1f;
		if (wuDaoLevelByType <= itemQuality)
		{
			return 1f;
		}
		if (wuDaoLevelByType == itemQuality + 1)
		{
			return 0.5f;
		}
		return 0f;
	}

	public void setFinsh()
	{
		inventoryCaiLiao.resteAllInventoryItem();
		inventoryCaiLiao.LoadInventory();
	}

	public void FinishAddItem(int index, Inventory2 InventoryFinish, ItemDatebase datebase, int id, int lianzhicishu)
	{
		InventoryFinish.inventory[index] = datebase.items[id].Clone();
		InventoryFinish.inventory[index].itemNum = lianzhicishu;
	}

	public void addYaoCai(List<int> danyao, List<int> num)
	{
		if (!DanLuPlan.activeSelf)
		{
			return;
		}
		inventoryCaiLiao.resteAllInventoryItem();
		inventoryCaiLiao.LoadInventory();
		for (int i = 0; i < danyao.Count; i++)
		{
			if (danyao[i] <= 0)
			{
				continue;
			}
			foreach (ITEM_INFO value in Tools.instance.getPlayer().itemList.values)
			{
				if (danyao[i] == value.itemId && num[i] <= value.itemCount)
				{
					inventoryCaiLiao.setindexItem(24 + i, danyao[i], num[i], value.uuid);
					inventoryCaiLiao.reduceItem1(2, value.uuid, num[i]);
				}
			}
		}
	}

	private void Update()
	{
	}
}
