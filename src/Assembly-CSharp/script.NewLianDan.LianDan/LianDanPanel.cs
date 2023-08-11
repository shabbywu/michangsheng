using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.NewLianDan.Base;

namespace script.NewLianDan.LianDan;

public class LianDanPanel : BasePanel
{
	public int NaiJiu;

	public int MaxNum;

	public List<LianDanSlot> CaoYaoList;

	public DanLuSlot DanLu;

	private Text _naiJiu;

	public List<int> YaoZhi;

	public List<int> ReduceNaiJiu;

	public List<int> MaxCaoYao;

	public FpBtn StartLianDanBtn;

	public LianDanSelect Select;

	public FpBtn WenHao;

	public int SelectNum = 1;

	public LianDanPanel(GameObject go)
	{
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		_go = go;
		CaoYaoList = new List<LianDanSlot>();
		YaoZhi = new List<int> { 1, 3, 9, 36, 180, 1080 };
		ReduceNaiJiu = new List<int> { 1, 2, 40, 80, 120 };
		MaxCaoYao = new List<int> { 9, 10, 11, 12, 13, 14 };
		CaoYaoList.Add(Get<LianDanSlot>("药引"));
		CaoYaoList.Add(Get<LianDanSlot>("主药1"));
		CaoYaoList.Add(Get<LianDanSlot>("主药2"));
		CaoYaoList.Add(Get<LianDanSlot>("辅药1"));
		CaoYaoList.Add(Get<LianDanSlot>("辅药2"));
		WenHao = Get<FpBtn>("药引/Bg/问号");
		WenHao.mouseEnterEvent.AddListener(new UnityAction(ShowTips));
		WenHao.mouseOutEvent.AddListener(new UnityAction(HideTips));
		WenHao.mouseUpEvent.AddListener(new UnityAction(HideTips));
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			caoYao.InitUI();
		}
		_naiJiu = Get<Text>("DanLu/NaiJiuDu/Value");
		DanLu = Get<DanLuSlot>("丹炉");
		StartLianDanBtn = Get<FpBtn>("开始炼丹");
		StartLianDanBtn.mouseUpEvent.AddListener(new UnityAction(ClickLianDan));
		Select = Get<LianDanSelect>("LianDanSelect");
		CheckCanMade();
	}

	public void ShowTips()
	{
		Get("药引提示").SetActive(true);
	}

	public void HideTips()
	{
		Get("药引提示").SetActive(false);
	}

	public void PutDanLu(DanLuSlot dragSlot)
	{
		if (!DanLu.IsNull())
		{
			LianDanUIMag.Instance.DanLuBag.AddTempItem(DanLu.Item, 1);
		}
		DanLu.SetSlotData(dragSlot.Item);
		LianDanUIMag.Instance.DanLuBag.RemoveTempItem(dragSlot.Item.Uid, dragSlot.Item.Count);
		NaiJiu = DanLu.Item.Seid["NaiJiu"].I;
		_naiJiu.SetText($"{NaiJiu}/100");
		int imgQuality = DanLu.Item.GetImgQuality();
		MaxNum = MaxCaoYao[imgQuality - 1];
		BackAllCaoYao();
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			caoYao.SetIsLock(value: false);
		}
		if (imgQuality == 1)
		{
			CaoYaoList[2].SetIsLock(value: true);
			CaoYaoList[4].SetIsLock(value: true);
		}
		else if (imgQuality < 6)
		{
			CaoYaoList[2].SetIsLock(value: true);
		}
		UpdateUI();
		CheckCanMade();
	}

	public void BackDanLu(DanLuSlot dragSlot)
	{
		LianDanUIMag.Instance.DanLuBag.AddTempItem(dragSlot.Item, 1);
		dragSlot.SetNull();
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			caoYao.SetIsLock(value: true);
		}
		_naiJiu.SetText("0/100");
	}

	public void ZhaLuCallBack()
	{
		DanLu.SetNull();
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			caoYao.SetIsLock(value: true);
		}
		_naiJiu.SetText("0/100");
		DanLu.UpdateNaiJiu();
	}

	public void PutCaoYao(LianDanSlot dragSlot)
	{
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		LianDanSlot toSlot = LianDanUIMag.Instance.CaoYaoBag.ToSlot;
		if (!dragSlot.Item.CanSale)
		{
			UIPopTip.Inst.Pop("此物品无法交易");
			return;
		}
		if (dragSlot.Item.Count < 1)
		{
			UIPopTip.Inst.Pop("此物品数量小于0寄卖");
			return;
		}
		int setCount = 0;
		if (Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303))
		{
			setCount = 5;
			if (dragSlot.Item.Count < 5)
			{
				setCount = dragSlot.Item.Count;
			}
		}
		if (Input.GetKey((KeyCode)306) || Input.GetKey((KeyCode)305))
		{
			setCount = dragSlot.Item.Count;
		}
		UnityAction val = (UnityAction)delegate
		{
			int num = 0;
			num = ((setCount <= 0) ? LianDanUIMag.Instance.Select.CurNum : setCount);
			if (toSlot.IsNull())
			{
				toSlot.SetSlotData(dragSlot.Item.Clone());
				toSlot.Item.Count = num;
			}
			else if (dragSlot.Item.Id == toSlot.Item.Id)
			{
				toSlot.Item.Count += num;
			}
			else
			{
				LianDanUIMag.Instance.CaoYaoBag.AddTempItem(toSlot.Item, toSlot.Item.Count);
				toSlot.SetSlotData(dragSlot.Item.Clone());
				toSlot.Item.Count = num;
			}
			toSlot.UpdateUI();
			LianDanUIMag.Instance.CaoYaoBag.RemoveTempItem(dragSlot.Item.Uid, num);
			CheckCanMade();
		};
		if (setCount > 0)
		{
			val.Invoke();
		}
		else
		{
			LianDanUIMag.Instance.Select.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, val);
		}
	}

	public void PutCaoYao(int index, int id, int count)
	{
		CaoYaoBag caoYaoBag = LianDanUIMag.Instance.CaoYaoBag;
		BaseItem tempItemById = caoYaoBag.GetTempItemById(id);
		if (tempItemById == null)
		{
			UIPopTip.Inst.Pop("不存该物品");
			Debug.LogError((object)"不存该物品");
			return;
		}
		tempItemById.Count = count;
		CaoYaoList[index].SetSlotData(tempItemById);
		caoYaoBag.RemoveTempItem(tempItemById.Uid, count);
		caoYaoBag.UpdateItem();
	}

	public void BackCaoYao(LianDanSlot dragSlot)
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		if (!dragSlot.Item.CanSale)
		{
			UIPopTip.Inst.Pop("此物品无法交易");
			return;
		}
		if (dragSlot.Item.Count < 1)
		{
			UIPopTip.Inst.Pop("此物品数量小于0寄卖");
			return;
		}
		int setCount = 0;
		if (Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303))
		{
			setCount = 5;
			if (dragSlot.Item.Count < 5)
			{
				setCount = dragSlot.Item.Count;
			}
		}
		if (Input.GetKey((KeyCode)306) || Input.GetKey((KeyCode)305))
		{
			setCount = dragSlot.Item.Count;
		}
		UnityAction val = (UnityAction)delegate
		{
			int num = 0;
			num = ((setCount <= 0) ? LianDanUIMag.Instance.Select.CurNum : setCount);
			LianDanUIMag.Instance.CaoYaoBag.AddTempItem(dragSlot.Item, num);
			dragSlot.Item.Count -= num;
			if (dragSlot.Item.Count <= 0)
			{
				dragSlot.SetNull();
			}
			else
			{
				dragSlot.UpdateUI();
			}
			CheckCanMade();
		};
		if (setCount > 0)
		{
			val.Invoke();
		}
		else
		{
			LianDanUIMag.Instance.Select.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, val);
		}
	}

	public void UpdateUI()
	{
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			caoYao.UpdateYaoXin();
		}
	}

	public void DanLuUI()
	{
		if (DanLu.IsNull())
		{
			NaiJiu = 0;
		}
		else
		{
			NaiJiu = DanLu.Item.Seid["NaiJiu"].I;
		}
		DanLu.UpdateNaiJiu();
		_naiJiu.SetText($"{NaiJiu}/100");
	}

	public void BackAllCaoYao()
	{
		LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			caoYao.SetNull();
		}
	}

	public void CheckCanMade()
	{
		if (DanLu.IsNull())
		{
			Get("开始炼丹/CanClick").SetActive(false);
			Get("开始炼丹/UnClick").SetActive(true);
			StartLianDanBtn.SetCanClick(flag: false);
			return;
		}
		bool flag = false;
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			if (!caoYao.IsNull())
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			Get("开始炼丹/CanClick").SetActive(false);
			Get("开始炼丹/UnClick").SetActive(true);
			StartLianDanBtn.SetCanClick(flag: false);
		}
		else
		{
			Get("开始炼丹/CanClick").SetActive(true);
			Get("开始炼丹/UnClick").SetActive(false);
			StartLianDanBtn.SetCanClick(flag: true);
		}
	}

	public void ClickLianDan()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		int num = 0;
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			if (!caoYao.IsNull())
			{
				num += caoYao.Item.Count;
			}
		}
		if (num > MaxNum)
		{
			UIPopTip.Inst.Pop($"该品阶丹炉最大药材数{MaxNum}个");
			return;
		}
		SelectNum = 1;
		Select.Init(GetLianDanName(), GetCanMadeNum(), (UnityAction)delegate
		{
			SelectNum = Select.CurNum;
			if (SelectNum > 0)
			{
				StartLianDan();
			}
		});
	}

	public void StartLianDan()
	{
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		List<LianDanResultManager.DanyaoItem> DanYaoItemList = new List<LianDanResultManager.DanyaoItem>();
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
		if (SelectNum > 0)
		{
			int lianzhicishu = SelectNum;
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
				CostItem(lianzhicishu);
				DanLuSlot danLu = DanLu;
				if (!danLu.IsNull())
				{
					int quality = _ItemJsonData.DataDict[DanLu.Item.Id].quality;
					int num = 0;
					num = maxpingzhi;
					int num2 = 0;
					if (num - quality == 0)
					{
						num2 = ReduceNaiJiu[1];
					}
					else if (num - quality == 1)
					{
						num2 = ReduceNaiJiu[2];
					}
					else if (num - quality == 2)
					{
						num2 = ReduceNaiJiu[3];
					}
					else if (num - quality >= 3)
					{
						num2 = ReduceNaiJiu[4];
					}
					else if (num - quality < 0)
					{
						num2 = ReduceNaiJiu[0];
					}
					if (maxpingzhi <= 0)
					{
						num2 = 2;
					}
					num2 *= lianzhicishu;
					if (quality > num && avatar.getStaticSkillAddSum(13) != 0)
					{
						num2 = 0;
					}
					List<int> list2 = new List<int> { 30, 40, 120, 200, 250, 300 };
					if (!danLu.Item.Seid.HasField("NaiJiu"))
					{
						UIPopTip.Inst.Pop("丹炉错误");
						return;
					}
					int num3 = (int)danLu.Item.Seid["NaiJiu"].n;
					int id = danLu.Item.Id;
					if (num3 - num2 <= 0)
					{
						avatar.removeItem(danLu.Item.Uid);
						LianDanUIMag.Instance.LianDanResult.ZhaLuLianDan();
						int num4 = (int)jsonData.instance.ItemJsonData[id.ToString()]["quality"].n;
						int num5 = avatar.HP - list2[num4 - 1];
						if (num5 <= 0)
						{
							UIDeath.Inst.Show(DeathType.炉毁人亡);
						}
						else
						{
							avatar.HP = num5;
						}
						return;
					}
					foreach (ITEM_INFO value in avatar.itemList.values)
					{
						if (value.uuid == danLu.Item.Uid)
						{
							value.Seid.SetField("NaiJiu", num3 - num2);
							danLu.Item.Seid.SetField("NaiJiu", num3 - num2);
						}
					}
					LianDanUIMag.Instance.DanLuBag.CreateTempList();
					LianDanUIMag.Instance.DanLuBag.UpdateItem();
				}
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				if (danFangItemID != null)
				{
					Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
					Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
					if (DanYaoItemList[0].ItemID > 0)
					{
						_ = danFangItemID["value1"].I;
						_ = 0;
					}
					for (int i = 1; i <= 5; i++)
					{
						if ((int)danFangItemID["value" + i].n != 0)
						{
							JSONObject jSONObject = jsonData.instance.ItemJsonData[((int)danFangItemID["value" + i].n).ToString()];
							int num6 = (int)jSONObject["yaoZhi" + indexToLeixin[i - 1]].n;
							int num7 = (int)jSONObject["quality"].n;
							int num8 = (int)danFangItemID["num" + i].n * YaoZhi[num7 - 1];
							int key = num6;
							switch (i)
							{
							case 1:
								if (DanYaoItemList[i - 1].ItemID > 0 && DanYaoItemList[i - 1].YaoZhi - num8 > 0)
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
					foreach (int key2 in fuyaoList.Keys)
					{
						if (dictionary2.ContainsKey(key2))
						{
							if (fuyaoList[key2] > dictionary2[key2])
							{
								Tools.dictionaryAddNum(dictionary, key2, fuyaoList[key2] - dictionary2[key2]);
							}
						}
						else
						{
							Tools.dictionaryAddNum(dictionary, key2, fuyaoList[key2]);
						}
					}
					foreach (int key3 in zhuyaoList.Keys)
					{
						if (dictionary3.ContainsKey(key3))
						{
							if (zhuyaoList[key3] > dictionary3[key3])
							{
								Tools.dictionaryAddNum(dictionary, key3, zhuyaoList[key3] - dictionary3[key3]);
							}
						}
						else
						{
							Tools.dictionaryAddNum(dictionary, key3, zhuyaoList[key3]);
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
						foreach (KeyValuePair<int, int> item in dictionary)
						{
							if (item.Key > 0)
							{
								FinishAddItem(num9, item.Key + 6900, item.Value * lianzhicishu);
								num9++;
							}
						}
						Fail(13, lianzhicishu);
					}
					else
					{
						FinishAddItem(0, maxpingzhi + 5900, lianzhicishu);
						avatar.addItem(maxpingzhi + 5900, lianzhicishu, Tools.CreateItemSeid(danFangItemID["ItemID"].I));
						Fail(15, lianzhicishu);
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
						FinishAddItem(0, maxpingzhi + 5906, lianzhicishu);
						avatar.addItem(maxpingzhi + 5906, lianzhicishu, Tools.CreateItemSeid(maxpingzhi + 5906));
						Fail(14, lianzhicishu);
					}
					else
					{
						JSONObject jSONObject2 = jsonData.instance.ItemJsonData[((int)danFangItemID["value1"].n).ToString()];
						_ = jSONObject2["yaoZhi1"].n;
						int num13 = (int)jSONObject2["quality"].n;
						int num14 = (int)danFangItemID["num1"].n * YaoZhi[num13 - 1];
						if (DanYaoItemList[0].YaoZhi < num14)
						{
							FinishAddItem(0, maxpingzhi + 5906, lianzhicishu);
							avatar.addItem(maxpingzhi + 5906, lianzhicishu, Tools.CreateItemSeid(maxpingzhi + 5906));
							Fail(14, lianzhicishu);
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
								foreach (int item2 in list4)
								{
									if (item2 < 0)
									{
										jSONObject4.Add(0);
									}
									else
									{
										jSONObject4.Add(item2);
									}
								}
								foreach (int item3 in list5)
								{
									if (item3 < 0)
									{
										jSONObject5.Add(0);
									}
									else
									{
										jSONObject5.Add(item3);
									}
								}
								jSONObject3.AddField("ID", danFangItemID["ItemID"].I);
								jSONObject3.AddField("Type", jSONObject4);
								jSONObject3.AddField("Num", jSONObject5);
								LianDanUIMag.Instance.DanFangPanel.AddDanFang(jSONObject3);
							}
							int num15 = 0;
							if (avatar.getStaticSkillAddSum(17) != 0)
							{
								lianzhicishu *= 2;
							}
							FinishAddItem(num15, danFangItemID["ItemID"].I, lianzhicishu);
							num15++;
							foreach (KeyValuePair<int, int> item4 in dictionary)
							{
								if (item4.Key > 0)
								{
									FinishAddItem(num15, item4.Key + 6900, item4.Value * lianzhicishu);
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
								Success(2 + (maxpingzhi - 1) * 2, lianzhicishu);
							}
							else
							{
								Success(1 + (maxpingzhi - 1) * 2, lianzhicishu);
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

	public void GetYaoLeiList(List<int> indexToLeixin, List<LianDanResultManager.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList, bool unlockYaoXing = false)
	{
		int num = 0;
		for (int i = 0; i < CaoYaoList.Count; i++)
		{
			LianDanSlot lianDanSlot = CaoYaoList[i];
			LianDanResultManager.DanyaoItem danyaoItem = new LianDanResultManager.DanyaoItem();
			if (!CaoYaoList[i].IsNull())
			{
				danyaoItem.ItemID = lianDanSlot.Item.Id;
				danyaoItem.ItemNum = lianDanSlot.Item.Count;
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
				danyaoItem.ItemID = -1;
				danyaoItem.ItemNum = 0;
				danyaoItem.YaoZhi = 0;
				danyaoItem.YaoZhiType = -1;
			}
			DanYaoItemList.Add(danyaoItem);
			num++;
		}
	}

	public void GetDanfangList(List<JSONObject> DanFans, List<int> indexToLeixin, List<LianDanResultManager.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
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

	public void GetDanFang(out int maxNum, out int maxpingzhi, out JSONObject danFangItemID, List<JSONObject> DanFans, List<int> indexToLeixin, List<LianDanResultManager.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList)
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

	public string GetLianDanName()
	{
		List<JSONObject> list = Tools.instance.getPlayer().DanFang.list;
		for (int i = 0; i < list.Count; i++)
		{
			int num = 0;
			for (int j = 0; j < 5; j++)
			{
				int num2 = 0;
				int num3 = 0;
				if (!CaoYaoList[j].IsNull())
				{
					num2 = CaoYaoList[j].Item.Id;
					num3 = CaoYaoList[j].Item.Count;
				}
				if (list[i]["Type"][j].I == num2 && list[i]["Num"][j].I == num3)
				{
					num++;
				}
			}
			if (num == 5)
			{
				return Tools.Code64(Tools.setColorByID(jsonData.instance.ItemJsonData[list[i]["ID"].I.ToString()]["name"].str, list[i]["ID"].I));
			}
		}
		return Tools.setColorByID("???", 1);
	}

	public int GetCanMadeNum()
	{
		int num = 10000000;
		Avatar player = Tools.instance.getPlayer();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			if (!caoYao.IsNull())
			{
				if (dictionary.ContainsKey(caoYao.Item.Id))
				{
					dictionary[caoYao.Item.Id] += caoYao.Item.Count;
				}
				else
				{
					dictionary.Add(caoYao.Item.Id, caoYao.Item.Count);
				}
			}
		}
		foreach (int key in dictionary.Keys)
		{
			int num2 = player.getItemNum(key) / dictionary[key];
			if (num2 < num)
			{
				num = num2;
			}
		}
		return num;
	}

	public void CostItem(int lianzhicishu)
	{
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		foreach (LianDanSlot caoYao in CaoYaoList)
		{
			if (!caoYao.IsNull())
			{
				if (num == 0)
				{
					player.AddYaoCaiShuXin(caoYao.Item.Id, 1);
				}
				else if (num <= 2)
				{
					player.AddYaoCaiShuXin(caoYao.Item.Id, 2);
				}
				else if (num <= 4)
				{
					player.AddYaoCaiShuXin(caoYao.Item.Id, 3);
				}
				player.removeItem(caoYao.Item.Id, caoYao.Item.Count * lianzhicishu);
			}
			caoYao.SetNull();
			num++;
		}
	}

	public void FinishAddItem(int index, int id, int lianzhicishu)
	{
		BaseItem slotData = BaseItem.Create(id, lianzhicishu, Tools.getUUID(), Tools.CreateItemSeid(id));
		LianDanUIMag.Instance.LianDanResult.SlotList[index].SetSlotData(slotData);
	}

	private void Fail(int index, int num)
	{
		LianDanUIMag.Instance.LianDanResult.Fail(index, num);
	}

	private void Success(int index, int num)
	{
		LianDanUIMag.Instance.LianDanResult.Success(index, num);
	}

	public void AddWuDaoLianDan(int ItemId, int Num)
	{
		JSONObject jSONObject = jsonData.instance.ItemJsonData[ItemId.ToString()];
		int i = jSONObject["quality"].I;
		int i2 = jSONObject["price"].I;
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int> { 10, 15, 30, 50, 70, 100 };
		float itemPercent = GetItemPercent(i);
		if (!player.HasLianZhiDanYao.HasItem(ItemId))
		{
			player.HasLianZhiDanYao.Add(ItemId);
			player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * 4f * itemPercent));
		}
		player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * (float)Num * itemPercent));
	}

	public float GetItemPercent(int itemQuality)
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

	public string GetCostTime(int count)
	{
		int num = 0;
		List<LianDanResultManager.DanyaoItem> danYaoItemList = new List<LianDanResultManager.DanyaoItem>();
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
}
