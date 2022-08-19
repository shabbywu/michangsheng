using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x02000462 RID: 1122
public class LianDanMag : MonoBehaviour
{
	// Token: 0x06002328 RID: 9000 RVA: 0x000F021C File Offset: 0x000EE41C
	private void Awake()
	{
		LianDanMag.instence = this;
		this.duiying = new List<List<int>>();
		this.duiying.Add(new List<int>
		{
			0,
			1,
			3
		});
		this.duiying.Add(new List<int>
		{
			0,
			1,
			3,
			4
		});
		this.duiying.Add(new List<int>
		{
			0,
			1,
			3,
			4
		});
		this.duiying.Add(new List<int>
		{
			0,
			1,
			3,
			4
		});
		this.duiying.Add(new List<int>
		{
			0,
			1,
			3,
			4
		});
		this.duiying.Add(new List<int>
		{
			0,
			1,
			2,
			3,
			4
		});
		for (int i = 0; i < (int)this.inventoryCaiLiao.count; i++)
		{
			this.inventoryCaiLiao.inventory.Add(new item());
		}
		for (int j = 0; j < (int)this.inventoryDanlu.count; j++)
		{
			this.inventoryDanlu.inventory.Add(new item());
		}
		for (int k = 0; k < (int)this.InventoryShowDanlu.count; k++)
		{
			this.InventoryShowDanlu.inventory.Add(new item());
		}
		for (int l = 0; l < (int)this.InventoryFinish.count; l++)
		{
			this.InventoryFinish.inventory.Add(new item());
		}
		this.show();
	}

	// Token: 0x06002329 RID: 9001 RVA: 0x000F03F4 File Offset: 0x000EE5F4
	private void OnDestroy()
	{
		LianDanMag.instence = null;
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x000F03FC File Offset: 0x000EE5FC
	public void OpenLianDanMag()
	{
		base.transform.localPosition = Vector3.zero;
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600232C RID: 9004 RVA: 0x000F041A File Offset: 0x000EE61A
	public void show()
	{
		this.closeDanlu();
		this.XuanZeDanLu.SetActive(false);
	}

	// Token: 0x0600232D RID: 9005 RVA: 0x000F0430 File Offset: 0x000EE630
	public void PlayDanfangIn()
	{
		if (this.DanfunPlan.transform.localPosition.x < -800f)
		{
			this.DanfunPlan.GetComponent<Animation>().Play("Danfangout");
			return;
		}
		this.DanfunPlan.GetComponent<Animation>().Play("Danfang");
	}

	// Token: 0x0600232E RID: 9006 RVA: 0x000F0488 File Offset: 0x000EE688
	public void PlayYaoCai()
	{
		if (this.YaoCaiPlan.transform.localPosition.x > 50f)
		{
			this.YaoCaiPlan.GetComponent<Animation>().Play("yaocaiout");
			return;
		}
		this.YaoCaiPlan.GetComponent<Animation>().Play("yaocaiin");
	}

	// Token: 0x0600232F RID: 9007 RVA: 0x000F04DE File Offset: 0x000EE6DE
	public void showChoiceDanLu()
	{
		this.XuanZeDanLu.SetActive(true);
		this.LianDanShu.gameObject.SetActive(false);
		this.finshPlan.gameObject.SetActive(false);
	}

	// Token: 0x06002330 RID: 9008 RVA: 0x000F050E File Offset: 0x000EE70E
	public void CloseChoiceDanlu()
	{
		this.XuanZeDanLu.SetActive(false);
	}

	// Token: 0x06002331 RID: 9009 RVA: 0x000F051C File Offset: 0x000EE71C
	public bool DanLuIsFull()
	{
		bool result = true;
		for (int i = 0; i < 5; i++)
		{
			if (!this.itemCells[i].JustShow && this.inventoryCaiLiao.inventory[i + 24].itemID <= 0)
			{
				result = false;
			}
		}
		return result;
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x000F056C File Offset: 0x000EE76C
	public void showDanlu()
	{
		this.AddDanluBtn.SetActive(false);
		int itemID = this.InventoryShowDanlu.inventory[0].itemID;
		int num = (int)jsonData.instance.ItemJsonData[itemID.ToString()]["quality"].n;
		int num2 = 0;
		foreach (object obj in this.CaiLiaoItem.transform)
		{
			Transform transform = (Transform)obj;
			ItemCellEX component = transform.GetComponent<ItemCellEX>();
			if (this.duiying[num - 1].Contains(num2))
			{
				component.JustShow = false;
				transform.Find("Background").GetComponent<UISprite>().spriteName = "liandna (22)";
			}
			else
			{
				component.JustShow = true;
				transform.Find("Background").GetComponent<UISprite>().spriteName = "liandna (23)";
			}
			num2++;
		}
		this.DanLuPlan.SetActive(true);
	}

	// Token: 0x06002333 RID: 9011 RVA: 0x000F068C File Offset: 0x000EE88C
	public void closeDanlu()
	{
		this.AddDanluBtn.SetActive(true);
		this.DanLuPlan.SetActive(false);
	}

	// Token: 0x06002334 RID: 9012 RVA: 0x000F06A8 File Offset: 0x000EE8A8
	public void StartLianDan()
	{
		this.XuanZeDanLu.SetActive(false);
		bool flag = false;
		int num = 0;
		foreach (ItemCellEX itemCellEX in LianDanMag.instence.itemCells)
		{
			int itemID = LianDanMag.instence.inventoryCaiLiao.inventory[int.Parse(itemCellEX.name)].itemID;
			if (itemID > 0)
			{
				flag = true;
			}
			if (itemID > 0)
			{
				num += this.inventoryCaiLiao.inventory[int.Parse(itemCellEX.name)].itemNum;
			}
		}
		int num2 = (int)jsonData.instance.ItemJsonData[this.InventoryShowDanlu.inventory[0].itemID.ToString()]["quality"].n;
		if (num > this.MaxCaoYao[num2 - 1])
		{
			UIPopTip.Inst.Pop("该品阶丹炉最大药材数" + this.MaxCaoYao[num2 - 1] + "个", PopTipIconType.叹号);
			return;
		}
		if (flag)
		{
			this.LianDanShu.show();
		}
	}

	// Token: 0x06002335 RID: 9013 RVA: 0x000F07E4 File Offset: 0x000EE9E4
	public void GetYaoLeiList(List<int> indexToLeixin, List<LianDanMag.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
	{
		int num = 0;
		foreach (ItemCellEX itemCellEX in this.itemCells)
		{
			LianDanMag.DanyaoItem danyaoItem = new LianDanMag.DanyaoItem();
			danyaoItem.ItemID = this.inventoryCaiLiao.inventory[int.Parse(itemCellEX.name)].itemID;
			danyaoItem.ItemNum = this.inventoryCaiLiao.inventory[int.Parse(itemCellEX.name)].itemNum;
			if (danyaoItem.ItemID != -1)
			{
				JSONObject jsonobject = jsonData.instance.ItemJsonData[danyaoItem.ItemID.ToString()];
				int yaoZhiType = (int)jsonobject["yaoZhi" + indexToLeixin[num]].n;
				int num2 = (int)jsonobject["quality"].n;
				danyaoItem.YaoZhi = danyaoItem.ItemNum * this.YaoZhi[num2 - 1];
				danyaoItem.YaoZhiType = yaoZhiType;
				if (num == 1 || num == 2)
				{
					Tools.dictionaryAddNum(zhuyaoList, danyaoItem.YaoZhiType, danyaoItem.YaoZhi);
				}
				else if (num == 3 || num == 4)
				{
					Tools.dictionaryAddNum(fuyaoList, danyaoItem.YaoZhiType, danyaoItem.YaoZhi);
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

	// Token: 0x06002336 RID: 9014 RVA: 0x000F0984 File Offset: 0x000EEB84
	public void GetDanfangList(List<JSONObject> DanFans, List<int> indexToLeixin, List<LianDanMag.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
	{
		foreach (JSONObject jsonobject in jsonData.instance.LianDanDanFangBiao.list)
		{
			bool flag = true;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			for (int i = 2; i <= 5; i++)
			{
				if ((int)jsonobject["value" + i].n != 0)
				{
					JSONObject jsonobject2 = jsonData.instance.ItemJsonData[((int)jsonobject["value" + i].n).ToString()];
					int num = (int)jsonobject2["yaoZhi" + indexToLeixin[i - 1]].n;
					int num2 = (int)jsonobject2["quality"].n;
					int num3 = (int)jsonobject["num" + i].n * this.YaoZhi[num2 - 1];
					int key = num;
					if (i == 2 || i == 3)
					{
						Tools.dictionaryAddNum(dictionary2, key, num3);
					}
					else if (i == 4 || i == 5)
					{
						Tools.dictionaryAddNum(dictionary, key, num3);
					}
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				if (!fuyaoList.ContainsKey(keyValuePair.Key) || fuyaoList[keyValuePair.Key] < keyValuePair.Value)
				{
					flag = false;
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair2 in dictionary2)
			{
				if (!zhuyaoList.ContainsKey(keyValuePair2.Key) || zhuyaoList[keyValuePair2.Key] < keyValuePair2.Value)
				{
					flag = false;
				}
			}
			if (flag)
			{
				DanFans.Add(jsonobject);
			}
		}
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x000F0BE4 File Offset: 0x000EEDE4
	public void GetDanFang(out int maxNum, out int maxpingzhi, out JSONObject danFangItemID, List<JSONObject> DanFans, List<int> indexToLeixin, List<LianDanMag.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
	{
		maxpingzhi = 0;
		maxNum = 0;
		danFangItemID = null;
		foreach (JSONObject jsonobject in DanFans)
		{
			int i = jsonData.instance.ItemJsonData[jsonobject["ItemID"].I.ToString()]["quality"].I;
			Debug.Log("丹方ID" + jsonobject["ItemID"].I);
			if (i > maxpingzhi)
			{
				maxNum = 0;
				maxpingzhi = i;
				danFangItemID = jsonobject;
			}
			if (i == maxpingzhi)
			{
				maxNum++;
			}
		}
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x000F0CAC File Offset: 0x000EEEAC
	public int getDanFang()
	{
		List<LianDanMag.DanyaoItem> list = new List<LianDanMag.DanyaoItem>();
		List<int> list2 = new List<int>
		{
			1,
			2,
			2,
			3,
			3
		};
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		this.GetYaoLeiList(list2, list, dictionary, dictionary2);
		List<JSONObject> danFans = new List<JSONObject>();
		this.GetDanfangList(danFans, list2, list, dictionary, dictionary2);
		int num = 0;
		int num2 = 0;
		JSONObject jsonobject = null;
		this.GetDanFang(out num2, out num, out jsonobject, danFans, list2, list, dictionary, dictionary2);
		Avatar player = Tools.instance.getPlayer();
		int num3 = this.LianDanShu.Num;
		List<int> list3 = new List<int>
		{
			3,
			4,
			5,
			6,
			7,
			8
		};
		if (num > 0)
		{
			player.AddTime(list3[num - 1] * num3, 0, 0);
		}
		else
		{
			player.AddTime(3 * num3, 0, 0);
		}
		foreach (ItemCellEX itemCellEX in this.itemCells)
		{
			if (this.inventoryCaiLiao.inventory[int.Parse(itemCellEX.name)].itemID != -1)
			{
				player.removeItem(this.inventoryCaiLiao.inventory[int.Parse(itemCellEX.name)].UUID, this.inventoryCaiLiao.inventory[int.Parse(itemCellEX.name)].itemNum * num3);
			}
		}
		this.InventoryFinish.resteAllInventoryItem();
		if (this.InventoryShowDanlu.inventory[0].itemID != -1)
		{
			int num4 = (int)jsonData.instance.ItemJsonData[this.InventoryShowDanlu.inventory[0].itemID.ToString()]["quality"].n;
			int num5 = num;
			int num6 = 0;
			if (num5 - num4 == 0)
			{
				num6 = this.ReduceNaijiu[1];
			}
			else if (num5 - num4 == 1)
			{
				num6 = this.ReduceNaijiu[2];
			}
			else if (num5 - num4 == 2)
			{
				num6 = this.ReduceNaijiu[3];
			}
			else if (num5 - num4 >= 3)
			{
				num6 = this.ReduceNaijiu[4];
			}
			else if (num5 - num4 < 0)
			{
				num6 = this.ReduceNaijiu[0];
			}
			if (num <= 0)
			{
				num6 = 2;
			}
			num6 *= num3;
			if (num4 > num5 && player.getStaticSkillAddSum(13) != 0)
			{
				num6 = 0;
			}
			List<int> list4 = new List<int>
			{
				30,
				40,
				120,
				200,
				250,
				300
			};
			if (this.InventoryShowDanlu.inventory[0].Seid.HasField("NaiJiu"))
			{
				int num7 = (int)this.InventoryShowDanlu.inventory[0].Seid["NaiJiu"].n;
				if (num7 - num6 <= 0)
				{
					this.zhaLu.SHowZhalu();
					player.removeItem(this.InventoryShowDanlu.inventory[0].UUID);
					this.inventoryCaiLiao.resteAllInventoryItem();
					this.inventoryCaiLiao.LoadInventory();
					this.inventoryDanlu.resteAllInventoryItem();
					this.inventoryDanlu.LoadInventory();
					this.closeDanlu();
					int num8 = (int)jsonData.instance.ItemJsonData[this.InventoryShowDanlu.inventory[0].itemID.ToString()]["quality"].n;
					player.AllMapAddHP(-list4[num8 - 1], DeathType.炉毁人亡);
					return -1;
				}
				using (List<ITEM_INFO>.Enumerator enumerator2 = player.itemList.values.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ITEM_INFO item_INFO = enumerator2.Current;
						if (item_INFO.uuid == this.InventoryShowDanlu.inventory[0].UUID)
						{
							item_INFO.Seid.SetField("NaiJiu", num7 - num6);
						}
					}
					goto IL_458;
				}
			}
			UIPopTip.Inst.Pop("丹炉错误", PopTipIconType.叹号);
			return -1;
		}
		IL_458:
		ItemDatebase component = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
		Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
		if (jsonobject != null)
		{
			Dictionary<int, int> dictionary4 = new Dictionary<int, int>();
			Dictionary<int, int> dictionary5 = new Dictionary<int, int>();
			for (int i = 1; i <= 5; i++)
			{
				if (list[i - 1].ItemID > 0 && (int)jsonobject["value" + i].n != 0)
				{
					JSONObject jsonobject2 = jsonData.instance.ItemJsonData[((int)jsonobject["value" + i].n).ToString()];
					int num9 = (int)jsonobject2["yaoZhi" + list2[i - 1]].n;
					int num10 = (int)jsonobject2["quality"].n;
					int num11 = (int)jsonobject["num" + i].n * this.YaoZhi[num10 - 1];
					int key = num9;
					if (i == 1)
					{
						if (list[i - 1].YaoZhi - num11 > 0)
						{
							dictionary3[list[i - 1].YaoZhiType] = list[i - 1].YaoZhi - num11;
						}
					}
					else if (i == 2 || i == 3)
					{
						Tools.dictionaryAddNum(dictionary5, key, num11);
					}
					else if (i == 4 || i == 5)
					{
						Tools.dictionaryAddNum(dictionary4, key, num11);
					}
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				if (dictionary4.ContainsKey(keyValuePair.Key) && keyValuePair.Value > dictionary4[keyValuePair.Key])
				{
					Tools.dictionaryAddNum(dictionary3, keyValuePair.Key, keyValuePair.Value - dictionary4[keyValuePair.Key]);
				}
			}
			using (Dictionary<int, int>.Enumerator enumerator3 = dictionary2.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					KeyValuePair<int, int> keyValuePair2 = enumerator3.Current;
					if (dictionary5.ContainsKey(keyValuePair2.Key) && keyValuePair2.Value > dictionary5[keyValuePair2.Key])
					{
						Tools.dictionaryAddNum(dictionary3, keyValuePair2.Key, keyValuePair2.Value - dictionary5[keyValuePair2.Key]);
					}
				}
				goto IL_71B;
			}
		}
		for (int j = 1; j <= 5; j++)
		{
			if (list[j - 1].ItemID > 0)
			{
				dictionary3[list[j - 1].YaoZhiType] = list[j - 1].YaoZhi;
			}
		}
		IL_71B:
		if (num2 != 1)
		{
			if (num == 0)
			{
				int num12 = 0;
				foreach (KeyValuePair<int, int> keyValuePair3 in dictionary3)
				{
					if (keyValuePair3.Key > 0)
					{
						this.FinishAddItem(num12, this.InventoryFinish, component, keyValuePair3.Key + 6900, keyValuePair3.Value * num3);
						num12++;
					}
				}
				this.finshPlan.fail(13, num3);
			}
			else
			{
				this.FinishAddItem(0, this.InventoryFinish, component, num + 5900, num3);
				player.addItem(num + 5900, num3, Tools.CreateItemSeid(jsonobject["ItemID"].I), false);
				this.finshPlan.fail(15, num3);
			}
			this.setFinsh();
			return -1;
		}
		List<int> list5 = new List<int>
		{
			-1,
			1,
			0
		};
		int num13 = -1;
		if (jsonobject != null)
		{
			int num14 = 0;
			for (int k = 2; k <= 5; k++)
			{
				if (list[k - 1].ItemID > 0)
				{
					int num15 = (int)jsonData.instance.ItemJsonData[list[k - 1].ItemID.ToString()]["yaoZhi1"].n;
					num14 += list5[num15 - 1];
				}
			}
			if (num14 > 0)
			{
				num13 = 1;
			}
			else if (num14 == 0)
			{
				num13 = 3;
			}
			else if (num14 < 0)
			{
				num13 = 2;
			}
		}
		if (list[0].YaoZhiType != num13)
		{
			this.FinishAddItem(0, this.InventoryFinish, component, num + 5906, num3);
			player.addItem(num + 5906, num3, Tools.CreateItemSeid(num + 5906), false);
			this.finshPlan.fail(14, num3);
			this.setFinsh();
			return -1;
		}
		JSONObject jsonobject3 = jsonData.instance.ItemJsonData[((int)jsonobject["value1"].n).ToString()];
		float n = jsonobject3["yaoZhi1"].n;
		int num16 = (int)jsonobject3["quality"].n;
		int num17 = (int)jsonobject["num1"].n * this.YaoZhi[num16 - 1];
		if (list[0].YaoZhi < num17)
		{
			this.FinishAddItem(0, this.InventoryFinish, component, num + 5906, num3);
			player.addItem(num + 5906, num3, Tools.CreateItemSeid(num + 5906), false);
			this.finshPlan.fail(14, num3);
			this.setFinsh();
			return -1;
		}
		if (num2 == 1 && num > 0)
		{
			if (jsonobject != null)
			{
				List<int> list6 = new List<int>();
				List<int> list7 = new List<int>();
				for (int l = 1; l <= 5; l++)
				{
					list6.Add(list[l - 1].ItemID);
					list7.Add(list[l - 1].ItemNum);
				}
				player.addDanFang(jsonobject["ItemID"].I, list6, list7);
			}
			int num18 = 0;
			if (player.getStaticSkillAddSum(17) != 0)
			{
				num3 *= 2;
			}
			this.FinishAddItem(num18, this.InventoryFinish, component, jsonobject["ItemID"].I, num3);
			num18++;
			foreach (KeyValuePair<int, int> keyValuePair4 in dictionary3)
			{
				if (keyValuePair4.Key > 0)
				{
					this.FinishAddItem(num18, this.InventoryFinish, component, keyValuePair4.Key + 6900, keyValuePair4.Value * num3);
					num18++;
				}
			}
			player.addItem(jsonobject["ItemID"].I, num3, Tools.CreateItemSeid(jsonobject["ItemID"].I), false);
			if (dictionary3.Count > 0)
			{
				this.finshPlan.succes(2 + (num - 1) * 2, num3);
			}
			else
			{
				this.finshPlan.succes(1 + (num - 1) * 2, num3);
			}
			try
			{
				LianDanMag.AddWuDaoLianDan(jsonobject["ItemID"].I, num3);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
		}
		this.lianDanDanFang.InitDanFang();
		this.setFinsh();
		return -1;
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x000F1898 File Offset: 0x000EFA98
	public static void AddWuDaoLianDan(int ItemId, int Num)
	{
		JSONObject jsonobject = jsonData.instance.ItemJsonData[ItemId.ToString()];
		int i = jsonobject["quality"].I;
		int i2 = jsonobject["price"].I;
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int>
		{
			10,
			15,
			30,
			50,
			70,
			100
		};
		float itemPercent = LianDanMag.getItemPercent(i);
		if (!player.HasLianZhiDanYao.HasItem(ItemId))
		{
			player.HasLianZhiDanYao.Add(ItemId);
			player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * 4f * itemPercent));
		}
		player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * (float)Num * itemPercent));
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x000F1984 File Offset: 0x000EFB84
	public static float getItemPercent(int itemQuality)
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(21);
		float result;
		if (wuDaoLevelByType <= itemQuality)
		{
			result = 1f;
		}
		else if (wuDaoLevelByType == itemQuality + 1)
		{
			result = 0.5f;
		}
		else
		{
			result = 0f;
		}
		return result;
	}

	// Token: 0x0600233B RID: 9019 RVA: 0x000F19CF File Offset: 0x000EFBCF
	public void setFinsh()
	{
		this.inventoryCaiLiao.resteAllInventoryItem();
		this.inventoryCaiLiao.LoadInventory();
	}

	// Token: 0x0600233C RID: 9020 RVA: 0x000F19E7 File Offset: 0x000EFBE7
	public void FinishAddItem(int index, Inventory2 InventoryFinish, ItemDatebase datebase, int id, int lianzhicishu)
	{
		InventoryFinish.inventory[index] = datebase.items[id].Clone();
		InventoryFinish.inventory[index].itemNum = lianzhicishu;
	}

	// Token: 0x0600233D RID: 9021 RVA: 0x000F1A1C File Offset: 0x000EFC1C
	public void addYaoCai(List<int> danyao, List<int> num)
	{
		if (!this.DanLuPlan.activeSelf)
		{
			return;
		}
		this.inventoryCaiLiao.resteAllInventoryItem();
		this.inventoryCaiLiao.LoadInventory();
		for (int i = 0; i < danyao.Count; i++)
		{
			if (danyao[i] > 0)
			{
				foreach (ITEM_INFO item_INFO in Tools.instance.getPlayer().itemList.values)
				{
					if (danyao[i] == item_INFO.itemId && (long)num[i] <= (long)((ulong)item_INFO.itemCount))
					{
						this.inventoryCaiLiao.setindexItem(24 + i, danyao[i], num[i], item_INFO.uuid);
						this.inventoryCaiLiao.reduceItem1(2, item_INFO.uuid, num[i]);
					}
				}
			}
		}
	}

	// Token: 0x0600233E RID: 9022 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C4D RID: 7245
	public Inventory2 inventoryCaiLiao;

	// Token: 0x04001C4E RID: 7246
	public Inventory2 inventoryDanlu;

	// Token: 0x04001C4F RID: 7247
	public Inventory2 InventoryShowDanlu;

	// Token: 0x04001C50 RID: 7248
	public Inventory2 InventoryFinish;

	// Token: 0x04001C51 RID: 7249
	public List<ItemCellEX> itemCells;

	// Token: 0x04001C52 RID: 7250
	public static LianDanMag instence;

	// Token: 0x04001C53 RID: 7251
	public List<List<int>> duiying;

	// Token: 0x04001C54 RID: 7252
	public List<int> YaoZhi;

	// Token: 0x04001C55 RID: 7253
	public List<int> ReduceNaijiu;

	// Token: 0x04001C56 RID: 7254
	public List<int> MaxCaoYao;

	// Token: 0x04001C57 RID: 7255
	public LianDanDanFang lianDanDanFang;

	// Token: 0x04001C58 RID: 7256
	public LianDanFinsh zhaLu;

	// Token: 0x04001C59 RID: 7257
	public LianDanFinsh finshPlan;

	// Token: 0x04001C5A RID: 7258
	public GameObject DanLuPlan;

	// Token: 0x04001C5B RID: 7259
	public GameObject AddDanluBtn;

	// Token: 0x04001C5C RID: 7260
	public GameObject XuanZeDanLu;

	// Token: 0x04001C5D RID: 7261
	public GameObject CaiLiaoItem;

	// Token: 0x04001C5E RID: 7262
	public LianDanShu LianDanShu;

	// Token: 0x04001C5F RID: 7263
	public GameObject DanfunPlan;

	// Token: 0x04001C60 RID: 7264
	public GameObject YaoCaiPlan;

	// Token: 0x020013A0 RID: 5024
	public class DanyaoItem
	{
		// Token: 0x040068E8 RID: 26856
		public int ItemID = -1;

		// Token: 0x040068E9 RID: 26857
		public int ItemNum;

		// Token: 0x040068EA RID: 26858
		public int YaoZhi;

		// Token: 0x040068EB RID: 26859
		public int YaoZhiType;
	}
}
