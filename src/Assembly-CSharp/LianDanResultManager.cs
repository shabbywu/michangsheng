using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E4 RID: 740
public class LianDanResultManager : MonoBehaviour
{
	// Token: 0x060019B6 RID: 6582 RVA: 0x000B7B9C File Offset: 0x000B5D9C
	public void lianDanJieSuan()
	{
		List<LianDanResultManager.DanyaoItem> DanYaoItemList = new List<LianDanResultManager.DanyaoItem>();
		List<int> indexToLeixin = new List<int>
		{
			1,
			2,
			2,
			3,
			3
		};
		Dictionary<int, int> fuyaoList = new Dictionary<int, int>();
		Dictionary<int, int> zhuyaoList = new Dictionary<int, int>();
		this.GetYaoLeiList(indexToLeixin, DanYaoItemList, fuyaoList, zhuyaoList, true);
		List<JSONObject> danFans = new List<JSONObject>();
		this.GetDanfangList(danFans, indexToLeixin, DanYaoItemList, fuyaoList, zhuyaoList);
		int maxpingzhi = 0;
		int maxNum = 0;
		JSONObject danFangItemID = null;
		this.GetDanFang(out maxNum, out maxpingzhi, out danFangItemID, danFans, indexToLeixin, DanYaoItemList, fuyaoList, zhuyaoList);
		Avatar avatar = Tools.instance.getPlayer();
		if (LianDanSystemManager.inst.lianDanPageManager.LianDanSum > 0)
		{
			int lianzhicishu = LianDanSystemManager.inst.lianDanPageManager.LianDanSum;
			List<int> list = new List<int>
			{
				3,
				4,
				5,
				6,
				7,
				8
			};
			if (maxpingzhi > 0)
			{
				avatar.AddTime(list[maxpingzhi - 1] * lianzhicishu, 0, 0);
			}
			else
			{
				avatar.AddTime(3 * lianzhicishu, 0, 0);
			}
			Tools.instance.playFader("正在炼制丹药...", delegate
			{
				this.costItem(lianzhicishu);
				LianDanSystemManager.inst.DanFangPageManager.updateDanFang();
				LianDanSystemManager.inst.inventory.resterLianDanFinshCell();
				this.resetAllResultItem();
				item item = LianDanSystemManager.inst.inventory.inventory[30];
				int maxpingzhi;
				if (LianDanSystemManager.inst.inventory.inventory[30].itemID != -1)
				{
					int num = (int)jsonData.instance.ItemJsonData[item.itemID.ToString()]["quality"].n;
					maxpingzhi = maxpingzhi;
					int num2 = 0;
					if (maxpingzhi - num == 0)
					{
						num2 = this.ReduceNaijiu[1];
					}
					else if (maxpingzhi - num == 1)
					{
						num2 = this.ReduceNaijiu[2];
					}
					else if (maxpingzhi - num == 2)
					{
						num2 = this.ReduceNaijiu[3];
					}
					else if (maxpingzhi - num >= 3)
					{
						num2 = this.ReduceNaijiu[4];
					}
					else if (maxpingzhi - num < 0)
					{
						num2 = this.ReduceNaijiu[0];
					}
					if (maxpingzhi <= 0)
					{
						num2 = 2;
					}
					num2 *= lianzhicishu;
					if (num > maxpingzhi && avatar.getStaticSkillAddSum(13) != 0)
					{
						num2 = 0;
					}
					List<int> list2 = new List<int>
					{
						30,
						40,
						120,
						200,
						250,
						300
					};
					if (item.Seid.HasField("NaiJiu"))
					{
						int num3 = (int)item.Seid["NaiJiu"].n;
						if (num3 - num2 <= 0)
						{
							avatar.removeItem(item.UUID);
							this.zhaLuLianDan();
							int num4 = (int)jsonData.instance.ItemJsonData[item.itemID.ToString()]["quality"].n;
							avatar.AllMapAddHP(-list2[num4 - 1], DeathType.炉毁人亡);
							return;
						}
						using (List<ITEM_INFO>.Enumerator enumerator = avatar.itemList.values.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ITEM_INFO item_INFO = enumerator.Current;
								if (item_INFO.uuid == item.UUID)
								{
									item_INFO.Seid.SetField("NaiJiu", num3 - num2);
								}
							}
							goto IL_2FE;
						}
					}
					UIPopTip.Inst.Pop("丹炉错误", PopTipIconType.叹号);
					return;
				}
				IL_2FE:
				ItemDatebase component = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				if (danFangItemID != null)
				{
					Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
					Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
					for (int i = 1; i <= 5; i++)
					{
						if (DanYaoItemList[i - 1].ItemID > 0 && (int)danFangItemID["value" + i].n != 0)
						{
							JSONObject jsonobject = jsonData.instance.ItemJsonData[((int)danFangItemID["value" + i].n).ToString()];
							int num5 = (int)jsonobject["yaoZhi" + indexToLeixin[i - 1]].n;
							int num6 = (int)jsonobject["quality"].n;
							int num7 = (int)danFangItemID["num" + i].n * this.YaoZhi[num6 - 1];
							int key = num5;
							if (i == 1)
							{
								if (DanYaoItemList[i - 1].YaoZhi - num7 > 0)
								{
									dictionary[DanYaoItemList[i - 1].YaoZhiType] = DanYaoItemList[i - 1].YaoZhi - num7;
								}
							}
							else if (i == 2 || i == 3)
							{
								Tools.dictionaryAddNum(dictionary3, key, num7);
							}
							else if (i == 4 || i == 5)
							{
								Tools.dictionaryAddNum(dictionary2, key, num7);
							}
						}
					}
					foreach (KeyValuePair<int, int> keyValuePair in fuyaoList)
					{
						if (dictionary2.ContainsKey(keyValuePair.Key) && keyValuePair.Value > dictionary2[keyValuePair.Key])
						{
							Tools.dictionaryAddNum(dictionary, keyValuePair.Key, keyValuePair.Value - dictionary2[keyValuePair.Key]);
						}
					}
					using (Dictionary<int, int>.Enumerator enumerator2 = zhuyaoList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<int, int> keyValuePair2 = enumerator2.Current;
							if (dictionary3.ContainsKey(keyValuePair2.Key) && keyValuePair2.Value > dictionary3[keyValuePair2.Key])
							{
								Tools.dictionaryAddNum(dictionary, keyValuePair2.Key, keyValuePair2.Value - dictionary3[keyValuePair2.Key]);
							}
						}
						goto IL_64D;
					}
				}
				for (int j = 1; j <= 5; j++)
				{
					if (DanYaoItemList[j - 1].ItemID > 0)
					{
						dictionary[DanYaoItemList[j - 1].YaoZhiType] = DanYaoItemList[j - 1].YaoZhi;
					}
				}
				IL_64D:
				if (maxNum != 1)
				{
					if (maxpingzhi == 0)
					{
						int num8 = 0;
						foreach (KeyValuePair<int, int> keyValuePair3 in dictionary)
						{
							if (keyValuePair3.Key > 0)
							{
								this.FinishAddItem(num8, component, keyValuePair3.Key + 6900, keyValuePair3.Value * lianzhicishu);
								num8++;
							}
						}
						this.failLianDan(13, lianzhicishu);
						return;
					}
					this.FinishAddItem(0, component, maxpingzhi + 5900, lianzhicishu);
					avatar.addItem(maxpingzhi + 5900, lianzhicishu, Tools.CreateItemSeid(danFangItemID["ItemID"].I), false);
					this.failLianDan(15, lianzhicishu);
					return;
				}
				else
				{
					List<int> list3 = new List<int>
					{
						-1,
						1,
						0
					};
					int num9 = -1;
					if (danFangItemID != null)
					{
						int num10 = 0;
						for (int k = 2; k <= 5; k++)
						{
							if (DanYaoItemList[k - 1].ItemID > 0)
							{
								int num11 = (int)jsonData.instance.ItemJsonData[DanYaoItemList[k - 1].ItemID.ToString()]["yaoZhi1"].n;
								num10 += list3[num11 - 1];
							}
						}
						if (num10 > 0)
						{
							num9 = 1;
						}
						else if (num10 == 0)
						{
							num9 = 3;
						}
						else if (num10 < 0)
						{
							num9 = 2;
						}
					}
					if (DanYaoItemList[0].YaoZhiType != num9)
					{
						this.FinishAddItem(0, component, maxpingzhi + 5906, lianzhicishu);
						avatar.addItem(maxpingzhi + 5906, lianzhicishu, Tools.CreateItemSeid(maxpingzhi + 5906), false);
						this.failLianDan(14, lianzhicishu);
						return;
					}
					JSONObject jsonobject2 = jsonData.instance.ItemJsonData[((int)danFangItemID["value1"].n).ToString()];
					float n = jsonobject2["yaoZhi1"].n;
					int num12 = (int)jsonobject2["quality"].n;
					int num13 = (int)danFangItemID["num1"].n * this.YaoZhi[num12 - 1];
					if (DanYaoItemList[0].YaoZhi < num13)
					{
						this.FinishAddItem(0, component, maxpingzhi + 5906, lianzhicishu);
						avatar.addItem(maxpingzhi + 5906, lianzhicishu, Tools.CreateItemSeid(maxpingzhi + 5906), false);
						this.failLianDan(14, lianzhicishu);
						return;
					}
					if (maxNum == 1 && maxpingzhi > 0)
					{
						if (danFangItemID != null)
						{
							List<int> list4 = new List<int>();
							List<int> list5 = new List<int>();
							for (int l = 1; l <= 5; l++)
							{
								if (DanYaoItemList[l - 1].ItemID <= 0)
								{
									list4.Add(0);
									list5.Add(0);
								}
								else
								{
									list4.Add(DanYaoItemList[l - 1].ItemID);
									list5.Add(DanYaoItemList[l - 1].ItemNum);
								}
							}
							JSONObject jsonobject3 = new JSONObject(JSONObject.Type.OBJECT);
							JSONObject jsonobject4 = new JSONObject(JSONObject.Type.ARRAY);
							JSONObject jsonobject5 = new JSONObject(JSONObject.Type.ARRAY);
							foreach (int num14 in list4)
							{
								if (num14 < 0)
								{
									jsonobject4.Add(0);
								}
								else
								{
									jsonobject4.Add(num14);
								}
							}
							foreach (int num15 in list5)
							{
								if (num15 < 0)
								{
									jsonobject5.Add(0);
								}
								else
								{
									jsonobject5.Add(num15);
								}
							}
							jsonobject3.AddField("ID", danFangItemID["ItemID"].I);
							jsonobject3.AddField("Type", jsonobject4);
							jsonobject3.AddField("Num", jsonobject5);
							LianDanSystemManager.inst.DanFangPageManager.addDanFang(jsonobject3);
						}
						int num16 = 0;
						if (avatar.getStaticSkillAddSum(17) != 0)
						{
							lianzhicishu *= 2;
						}
						this.FinishAddItem(num16, component, danFangItemID["ItemID"].I, lianzhicishu);
						num16++;
						foreach (KeyValuePair<int, int> keyValuePair4 in dictionary)
						{
							if (keyValuePair4.Key > 0)
							{
								this.FinishAddItem(num16, component, keyValuePair4.Key + 6900, keyValuePair4.Value * lianzhicishu);
								num16++;
							}
						}
						int i2 = danFangItemID["ItemID"].I;
						avatar.addItem(i2, lianzhicishu, Tools.CreateItemSeid(i2), false);
						PlayTutorial.CheckLianDan2(i2, DanYaoItemList);
						PlayTutorial.CheckLianDan3(i2, DanYaoItemList);
						PlayTutorial.CheckLianDan4(i2);
						if (dictionary.Count > 0)
						{
							this.successLianDan(2 + (maxpingzhi - 1) * 2, lianzhicishu);
						}
						else
						{
							this.successLianDan(1 + (maxpingzhi - 1) * 2, lianzhicishu);
						}
						try
						{
							this.AddWuDaoLianDan(i2, lianzhicishu);
						}
						catch (Exception ex)
						{
							Debug.LogError(ex);
						}
					}
					return;
				}
			});
			return;
		}
		UIPopTip.Inst.Pop("数据异常", PopTipIconType.叹号);
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x000B7D88 File Offset: 0x000B5F88
	public void AddWuDaoLianDan(int ItemId, int Num)
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
		float itemPercent = this.getItemPercent(i);
		if (!player.HasLianZhiDanYao.HasItem(ItemId))
		{
			player.HasLianZhiDanYao.Add(ItemId);
			player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * 4f * itemPercent));
		}
		player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * (float)Num * itemPercent));
	}

	// Token: 0x060019B8 RID: 6584 RVA: 0x000B7E78 File Offset: 0x000B6078
	public float getItemPercent(int itemQuality)
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

	// Token: 0x060019B9 RID: 6585 RVA: 0x000B7EC4 File Offset: 0x000B60C4
	public void GetDanFang(out int maxNum, out int maxpingzhi, out JSONObject danFangItemID, List<JSONObject> DanFans, List<int> indexToLeixin, List<LianDanResultManager.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
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

	// Token: 0x060019BA RID: 6586 RVA: 0x000B7F8C File Offset: 0x000B618C
	public void GetYaoLeiList(List<int> indexToLeixin, List<LianDanResultManager.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList, bool unlockYaoXing = false)
	{
		int num = 0;
		List<PutLianDanCell> putLianDanCellList = LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList;
		for (int i = 0; i < putLianDanCellList.Count - 1; i++)
		{
			PutLianDanCell putLianDanCell = putLianDanCellList[i];
			LianDanResultManager.DanyaoItem danyaoItem = new LianDanResultManager.DanyaoItem();
			danyaoItem.ItemID = putLianDanCell.Item.itemID;
			danyaoItem.ItemNum = putLianDanCell.Item.itemNum;
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
				if (unlockYaoXing)
				{
					Tools.instance.getPlayer().AddYaoCaiShuXin(danyaoItem.ItemID, indexToLeixin[num]);
				}
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

	// Token: 0x060019BB RID: 6587 RVA: 0x000B80F8 File Offset: 0x000B62F8
	public void GetDanfangList(List<JSONObject> DanFans, List<int> indexToLeixin, List<LianDanResultManager.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
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

	// Token: 0x060019BC RID: 6588 RVA: 0x000B8358 File Offset: 0x000B6558
	public string getCostTime(int count)
	{
		List<LianDanResultManager.DanyaoItem> danYaoItemList = new List<LianDanResultManager.DanyaoItem>();
		List<int> indexToLeixin = new List<int>
		{
			1,
			2,
			2,
			3,
			3
		};
		Dictionary<int, int> fuyaoList = new Dictionary<int, int>();
		Dictionary<int, int> zhuyaoList = new Dictionary<int, int>();
		this.GetYaoLeiList(indexToLeixin, danYaoItemList, fuyaoList, zhuyaoList, false);
		List<JSONObject> list = new List<JSONObject>();
		this.GetDanfangList(list, indexToLeixin, danYaoItemList, fuyaoList, zhuyaoList);
		string text = "???";
		if (list.Count > 0)
		{
			text = "";
			int num = 0;
			int num2 = 0;
			JSONObject jsonobject = null;
			this.GetDanFang(out num2, out num, out jsonobject, list, indexToLeixin, danYaoItemList, fuyaoList, zhuyaoList);
			Tools.instance.getPlayer();
			List<int> list2 = new List<int>
			{
				3,
				4,
				5,
				6,
				7,
				8
			};
			int num3;
			if (num > 0)
			{
				num3 = list2[num - 1] * count;
			}
			else
			{
				num3 = list2[num - 1] * count;
			}
			int num4 = num3 / 365;
			int num5 = (num3 - num4 * 365) / 30;
			int num6 = num3 - num4 * 365 - num5 * 30;
			if (num4 > 0)
			{
				text = text + num4 + "年";
			}
			if (num5 > 0)
			{
				text = text + num5 + "月";
			}
			text = text + num6 + "日";
		}
		return text;
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x000B84D8 File Offset: 0x000B66D8
	public void costItem(int lianzhicishu)
	{
		List<PutLianDanCell> putLianDanCellList = LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList;
		Tools.instance.getPlayer();
		for (int i = 0; i < putLianDanCellList.Count - 1; i++)
		{
			if (putLianDanCellList[i].Item.itemID != -1)
			{
				Tools.instance.getPlayer().removeItem(putLianDanCellList[i].Item.itemID, putLianDanCellList[i].Item.itemNum * lianzhicishu);
			}
		}
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x000B855C File Offset: 0x000B675C
	private void successLianDan(int index, int num)
	{
		this.desc.text = Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		base.gameObject.SetActive(true);
		this.Btn_QueDing.onClick.RemoveAllListeners();
		this.Btn_QueDing.onClick.AddListener(delegate()
		{
			base.gameObject.SetActive(false);
			LianDanSystemManager.inst.lianDanFinshCallBack();
		});
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x000B85F4 File Offset: 0x000B67F4
	private void failLianDan(int index, int num)
	{
		this.desc.text = Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		base.gameObject.SetActive(true);
		this.Btn_QueDing.onClick.RemoveAllListeners();
		this.Btn_QueDing.onClick.AddListener(delegate()
		{
			base.gameObject.SetActive(false);
			LianDanSystemManager.inst.lianDanFinshCallBack();
		});
	}

	// Token: 0x060019C0 RID: 6592 RVA: 0x000B868C File Offset: 0x000B688C
	public void FinishAddItem(int index, ItemDatebase datebase, int id, int lianzhicishu)
	{
		LianDanSystemManager.inst.inventory.inventory[index + 31] = datebase.items[id].Clone();
		LianDanSystemManager.inst.inventory.inventory[index + 31].itemNum = lianzhicishu;
		this.LianDanResultCellList[index].updateItem();
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x000B86F4 File Offset: 0x000B68F4
	public void resetAllResultItem()
	{
		for (int i = 0; i < this.LianDanResultCellList.Count; i++)
		{
			this.LianDanResultCellList[i].updateItem();
		}
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x000B8728 File Offset: 0x000B6928
	private void zhaLuLianDan()
	{
		LianDanSystemManager.inst.inventory.inventory[30] = new item();
		LianDanSystemManager.inst.inventory.inventory[30].itemNum = 1;
		LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList[5].updateItem();
		this.desc.text = Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[16.ToString()]["desc"].str);
		base.gameObject.SetActive(true);
		this.Btn_QueDing.onClick.RemoveAllListeners();
		this.Btn_QueDing.onClick.AddListener(delegate()
		{
			base.gameObject.SetActive(false);
			LianDanSystemManager.inst.lianDanFinshCallBack();
		});
		LianDanSystemManager.inst.lianDanPageManager.RemoveAll();
		LianDanSystemManager.inst.putDanLuManager.backPutDanLuPanel();
	}

	// Token: 0x040014D9 RID: 5337
	public List<int> YaoZhi;

	// Token: 0x040014DA RID: 5338
	public List<int> ReduceNaijiu;

	// Token: 0x040014DB RID: 5339
	public List<LianDanResultCell> LianDanResultCellList;

	// Token: 0x040014DC RID: 5340
	public List<int> MaxCaoYao;

	// Token: 0x040014DD RID: 5341
	[SerializeField]
	private Button Btn_QueDing;

	// Token: 0x040014DE RID: 5342
	[SerializeField]
	private Text desc;

	// Token: 0x0200132C RID: 4908
	public class DanyaoItem
	{
		// Token: 0x06007B52 RID: 31570 RVA: 0x002C096C File Offset: 0x002BEB6C
		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3}", new object[]
			{
				this.ItemID,
				this.ItemNum,
				this.YaoZhi,
				this.YaoZhiType
			});
		}

		// Token: 0x040067B0 RID: 26544
		public int ItemID = -1;

		// Token: 0x040067B1 RID: 26545
		public int ItemNum;

		// Token: 0x040067B2 RID: 26546
		public int YaoZhi;

		// Token: 0x040067B3 RID: 26547
		public int YaoZhiType;
	}
}
