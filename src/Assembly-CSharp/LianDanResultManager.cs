using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LianDanResultManager : MonoBehaviour
{
	public class DanyaoItem
	{
		public int ItemID = -1;

		public int ItemNum;

		public int YaoZhi;

		public int YaoZhiType;

		public override string ToString()
		{
			return $"{ItemID} {ItemNum} {YaoZhi} {YaoZhiType}";
		}
	}

	public List<int> YaoZhi;

	public List<int> ReduceNaijiu;

	public List<LianDanResultCell> LianDanResultCellList;

	public List<int> MaxCaoYao;

	[SerializeField]
	private Button Btn_QueDing;

	[SerializeField]
	private Text desc;

	public void lianDanJieSuan()
	{
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		List<DanyaoItem> DanYaoItemList = new List<DanyaoItem>();
		List<int> indexToLeixin = new List<int> { 1, 2, 2, 3, 3 };
		Dictionary<int, int> fuyaoList = new Dictionary<int, int>();
		Dictionary<int, int> zhuyaoList = new Dictionary<int, int>();
		GetYaoLeiList(indexToLeixin, DanYaoItemList, fuyaoList, zhuyaoList, unlockYaoXing: true);
		List<JSONObject> danFans = new List<JSONObject>();
		GetDanfangList(danFans, indexToLeixin, DanYaoItemList, fuyaoList, zhuyaoList);
		int maxpingzhi = 0;
		int maxNum = 0;
		JSONObject danFangItemID = null;
		GetDanFang(out maxNum, out maxpingzhi, out danFangItemID, danFans, indexToLeixin, DanYaoItemList, fuyaoList, zhuyaoList);
		Avatar avatar = Tools.instance.getPlayer();
		if (LianDanSystemManager.inst.lianDanPageManager.LianDanSum > 0)
		{
			int lianzhicishu = LianDanSystemManager.inst.lianDanPageManager.LianDanSum;
			List<int> list = new List<int> { 3, 4, 5, 6, 7, 8 };
			if (maxpingzhi > 0)
			{
				avatar.AddTime(list[maxpingzhi - 1] * lianzhicishu);
			}
			else
			{
				avatar.AddTime(3 * lianzhicishu);
			}
			Tools.instance.playFader("正在炼制丹药...", (UnityAction)delegate
			{
				costItem(lianzhicishu);
				LianDanSystemManager.inst.DanFangPageManager.updateDanFang();
				LianDanSystemManager.inst.inventory.resterLianDanFinshCell();
				resetAllResultItem();
				item item = LianDanSystemManager.inst.inventory.inventory[30];
				if (LianDanSystemManager.inst.inventory.inventory[30].itemID != -1)
				{
					int num = (int)jsonData.instance.ItemJsonData[item.itemID.ToString()]["quality"].n;
					int num2 = 0;
					num2 = maxpingzhi;
					int num3 = 0;
					if (num2 - num == 0)
					{
						num3 = ReduceNaijiu[1];
					}
					else if (num2 - num == 1)
					{
						num3 = ReduceNaijiu[2];
					}
					else if (num2 - num == 2)
					{
						num3 = ReduceNaijiu[3];
					}
					else if (num2 - num >= 3)
					{
						num3 = ReduceNaijiu[4];
					}
					else if (num2 - num < 0)
					{
						num3 = ReduceNaijiu[0];
					}
					if (maxpingzhi <= 0)
					{
						num3 = 2;
					}
					num3 *= lianzhicishu;
					if (num > num2 && avatar.getStaticSkillAddSum(13) != 0)
					{
						num3 = 0;
					}
					List<int> list2 = new List<int> { 30, 40, 120, 200, 250, 300 };
					if (!item.Seid.HasField("NaiJiu"))
					{
						UIPopTip.Inst.Pop("丹炉错误");
						return;
					}
					int num4 = (int)item.Seid["NaiJiu"].n;
					if (num4 - num3 <= 0)
					{
						avatar.removeItem(item.UUID);
						zhaLuLianDan();
						int num5 = (int)jsonData.instance.ItemJsonData[item.itemID.ToString()]["quality"].n;
						avatar.AllMapAddHP(-list2[num5 - 1], DeathType.炉毁人亡);
						return;
					}
					foreach (ITEM_INFO value in avatar.itemList.values)
					{
						if (value.uuid == item.UUID)
						{
							value.Seid.SetField("NaiJiu", num4 - num3);
						}
					}
				}
				ItemDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				if (danFangItemID != null)
				{
					Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
					Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
					for (int i = 1; i <= 5; i++)
					{
						if (DanYaoItemList[i - 1].ItemID > 0 && (int)danFangItemID["value" + i].n != 0)
						{
							JSONObject jSONObject = jsonData.instance.ItemJsonData[((int)danFangItemID["value" + i].n).ToString()];
							int num6 = (int)jSONObject["yaoZhi" + indexToLeixin[i - 1]].n;
							int num7 = (int)jSONObject["quality"].n;
							int num8 = (int)danFangItemID["num" + i].n * YaoZhi[num7 - 1];
							int key = num6;
							switch (i)
							{
							case 1:
								if (DanYaoItemList[i - 1].YaoZhi - num8 > 0)
								{
									dictionary[DanYaoItemList[i - 1].YaoZhiType] = DanYaoItemList[i - 1].YaoZhi - num8;
								}
								break;
							case 2:
							case 3:
								Tools.dictionaryAddNum(dictionary3, key, num8);
								break;
							case 4:
							case 5:
								Tools.dictionaryAddNum(dictionary2, key, num8);
								break;
							}
						}
					}
					foreach (KeyValuePair<int, int> item2 in fuyaoList)
					{
						if (dictionary2.ContainsKey(item2.Key) && item2.Value > dictionary2[item2.Key])
						{
							Tools.dictionaryAddNum(dictionary, item2.Key, item2.Value - dictionary2[item2.Key]);
						}
					}
					foreach (KeyValuePair<int, int> item3 in zhuyaoList)
					{
						if (dictionary3.ContainsKey(item3.Key) && item3.Value > dictionary3[item3.Key])
						{
							Tools.dictionaryAddNum(dictionary, item3.Key, item3.Value - dictionary3[item3.Key]);
						}
					}
				}
				else
				{
					for (int j = 1; j <= 5; j++)
					{
						if (DanYaoItemList[j - 1].ItemID > 0)
						{
							dictionary[DanYaoItemList[j - 1].YaoZhiType] = DanYaoItemList[j - 1].YaoZhi;
						}
					}
				}
				if (maxNum != 1)
				{
					if (maxpingzhi == 0)
					{
						int num9 = 0;
						foreach (KeyValuePair<int, int> item4 in dictionary)
						{
							if (item4.Key > 0)
							{
								FinishAddItem(num9, component, item4.Key + 6900, item4.Value * lianzhicishu);
								num9++;
							}
						}
						failLianDan(13, lianzhicishu);
					}
					else
					{
						FinishAddItem(0, component, maxpingzhi + 5900, lianzhicishu);
						avatar.addItem(maxpingzhi + 5900, lianzhicishu, Tools.CreateItemSeid(danFangItemID["ItemID"].I));
						failLianDan(15, lianzhicishu);
					}
				}
				else
				{
					List<int> list3 = new List<int> { -1, 1, 0 };
					int num10 = -1;
					if (danFangItemID != null)
					{
						int num11 = 0;
						for (int k = 2; k <= 5; k++)
						{
							if (DanYaoItemList[k - 1].ItemID > 0)
							{
								int num12 = (int)jsonData.instance.ItemJsonData[DanYaoItemList[k - 1].ItemID.ToString()]["yaoZhi1"].n;
								num11 += list3[num12 - 1];
							}
						}
						if (num11 > 0)
						{
							num10 = 1;
						}
						else if (num11 == 0)
						{
							num10 = 3;
						}
						else if (num11 < 0)
						{
							num10 = 2;
						}
					}
					if (DanYaoItemList[0].YaoZhiType != num10)
					{
						FinishAddItem(0, component, maxpingzhi + 5906, lianzhicishu);
						avatar.addItem(maxpingzhi + 5906, lianzhicishu, Tools.CreateItemSeid(maxpingzhi + 5906));
						failLianDan(14, lianzhicishu);
					}
					else
					{
						JSONObject jSONObject2 = jsonData.instance.ItemJsonData[((int)danFangItemID["value1"].n).ToString()];
						_ = jSONObject2["yaoZhi1"].n;
						int num13 = (int)jSONObject2["quality"].n;
						int num14 = (int)danFangItemID["num1"].n * YaoZhi[num13 - 1];
						if (DanYaoItemList[0].YaoZhi < num14)
						{
							FinishAddItem(0, component, maxpingzhi + 5906, lianzhicishu);
							avatar.addItem(maxpingzhi + 5906, lianzhicishu, Tools.CreateItemSeid(maxpingzhi + 5906));
							failLianDan(14, lianzhicishu);
						}
						else if (maxNum == 1 && maxpingzhi > 0)
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
								JSONObject jSONObject3 = new JSONObject(JSONObject.Type.OBJECT);
								JSONObject jSONObject4 = new JSONObject(JSONObject.Type.ARRAY);
								JSONObject jSONObject5 = new JSONObject(JSONObject.Type.ARRAY);
								foreach (int item5 in list4)
								{
									if (item5 < 0)
									{
										jSONObject4.Add(0);
									}
									else
									{
										jSONObject4.Add(item5);
									}
								}
								foreach (int item6 in list5)
								{
									if (item6 < 0)
									{
										jSONObject5.Add(0);
									}
									else
									{
										jSONObject5.Add(item6);
									}
								}
								jSONObject3.AddField("ID", danFangItemID["ItemID"].I);
								jSONObject3.AddField("Type", jSONObject4);
								jSONObject3.AddField("Num", jSONObject5);
								LianDanSystemManager.inst.DanFangPageManager.addDanFang(jSONObject3);
							}
							int num15 = 0;
							if (avatar.getStaticSkillAddSum(17) != 0)
							{
								lianzhicishu *= 2;
							}
							FinishAddItem(num15, component, danFangItemID["ItemID"].I, lianzhicishu);
							num15++;
							foreach (KeyValuePair<int, int> item7 in dictionary)
							{
								if (item7.Key > 0)
								{
									FinishAddItem(num15, component, item7.Key + 6900, item7.Value * lianzhicishu);
									num15++;
								}
							}
							int i2 = danFangItemID["ItemID"].I;
							avatar.addItem(i2, lianzhicishu, Tools.CreateItemSeid(i2));
							PlayTutorial.CheckLianDan2(i2, DanYaoItemList);
							PlayTutorial.CheckLianDan3(i2, DanYaoItemList);
							PlayTutorial.CheckLianDan4(i2);
							if (dictionary.Count > 0)
							{
								successLianDan(2 + (maxpingzhi - 1) * 2, lianzhicishu);
							}
							else
							{
								successLianDan(1 + (maxpingzhi - 1) * 2, lianzhicishu);
							}
							try
							{
								AddWuDaoLianDan(i2, lianzhicishu);
							}
							catch (Exception ex)
							{
								Debug.LogError((object)ex);
							}
						}
					}
				}
			});
		}
		else
		{
			UIPopTip.Inst.Pop("数据异常");
		}
	}

	public void AddWuDaoLianDan(int ItemId, int Num)
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

	public float getItemPercent(int itemQuality)
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

	public void GetYaoLeiList(List<int> indexToLeixin, List<DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList, bool unlockYaoXing = false)
	{
		int num = 0;
		List<PutLianDanCell> putLianDanCellList = LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList;
		for (int i = 0; i < putLianDanCellList.Count - 1; i++)
		{
			PutLianDanCell putLianDanCell = putLianDanCellList[i];
			DanyaoItem danyaoItem = new DanyaoItem();
			danyaoItem.ItemID = putLianDanCell.Item.itemID;
			danyaoItem.ItemNum = putLianDanCell.Item.itemNum;
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

	public string getCostTime(int count)
	{
		int num = 0;
		List<DanyaoItem> danYaoItemList = new List<DanyaoItem>();
		List<int> indexToLeixin = new List<int> { 1, 2, 2, 3, 3 };
		Dictionary<int, int> fuyaoList = new Dictionary<int, int>();
		Dictionary<int, int> zhuyaoList = new Dictionary<int, int>();
		GetYaoLeiList(indexToLeixin, danYaoItemList, fuyaoList, zhuyaoList);
		List<JSONObject> list = new List<JSONObject>();
		GetDanfangList(list, indexToLeixin, danYaoItemList, fuyaoList, zhuyaoList);
		string result = "???";
		if (list.Count > 0)
		{
			result = "";
			int maxpingzhi = 0;
			int maxNum = 0;
			JSONObject danFangItemID = null;
			GetDanFang(out maxNum, out maxpingzhi, out danFangItemID, list, indexToLeixin, danYaoItemList, fuyaoList, zhuyaoList);
			Tools.instance.getPlayer();
			List<int> list2 = new List<int> { 3, 4, 5, 6, 7, 8 };
			num = ((maxpingzhi <= 0) ? (list2[maxpingzhi - 1] * count) : (list2[maxpingzhi - 1] * count));
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			num2 = num / 365;
			num3 = (num - num2 * 365) / 30;
			num4 = num - num2 * 365 - num3 * 30;
			if (num2 > 0)
			{
				result = result + num2 + "年";
			}
			if (num3 > 0)
			{
				result = result + num3 + "月";
			}
			result = result + num4 + "日";
		}
		return result;
	}

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

	private void successLianDan(int index, int num)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		desc.text = Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		((Component)this).gameObject.SetActive(true);
		((UnityEventBase)Btn_QueDing.onClick).RemoveAllListeners();
		((UnityEvent)Btn_QueDing.onClick).AddListener((UnityAction)delegate
		{
			((Component)this).gameObject.SetActive(false);
			LianDanSystemManager.inst.lianDanFinshCallBack();
		});
	}

	private void failLianDan(int index, int num)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		desc.text = Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		((Component)this).gameObject.SetActive(true);
		((UnityEventBase)Btn_QueDing.onClick).RemoveAllListeners();
		((UnityEvent)Btn_QueDing.onClick).AddListener((UnityAction)delegate
		{
			((Component)this).gameObject.SetActive(false);
			LianDanSystemManager.inst.lianDanFinshCallBack();
		});
	}

	public void FinishAddItem(int index, ItemDatebase datebase, int id, int lianzhicishu)
	{
		LianDanSystemManager.inst.inventory.inventory[index + 31] = datebase.items[id].Clone();
		LianDanSystemManager.inst.inventory.inventory[index + 31].itemNum = lianzhicishu;
		LianDanResultCellList[index].updateItem();
	}

	public void resetAllResultItem()
	{
		for (int i = 0; i < LianDanResultCellList.Count; i++)
		{
			LianDanResultCellList[i].updateItem();
		}
	}

	private void zhaLuLianDan()
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		LianDanSystemManager.inst.inventory.inventory[30] = new item();
		LianDanSystemManager.inst.inventory.inventory[30].itemNum = 1;
		LianDanSystemManager.inst.lianDanPageManager.putLianDanCellList[5].updateItem();
		desc.text = Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[16.ToString()]["desc"].str);
		((Component)this).gameObject.SetActive(true);
		((UnityEventBase)Btn_QueDing.onClick).RemoveAllListeners();
		((UnityEvent)Btn_QueDing.onClick).AddListener((UnityAction)delegate
		{
			((Component)this).gameObject.SetActive(false);
			LianDanSystemManager.inst.lianDanFinshCallBack();
		});
		LianDanSystemManager.inst.lianDanPageManager.RemoveAll();
		LianDanSystemManager.inst.putDanLuManager.backPutDanLuPanel();
	}
}
