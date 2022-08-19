using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using script.NewLianDan.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.LianDan
{
	// Token: 0x020009FB RID: 2555
	public class LianDanPanel : BasePanel
	{
		// Token: 0x060046BC RID: 18108 RVA: 0x001DE6D8 File Offset: 0x001DC8D8
		public LianDanPanel(GameObject go)
		{
			this._go = go;
			this.CaoYaoList = new List<LianDanSlot>();
			this.YaoZhi = new List<int>
			{
				1,
				3,
				9,
				36,
				180,
				1080
			};
			this.ReduceNaiJiu = new List<int>
			{
				1,
				2,
				40,
				80,
				120
			};
			this.MaxCaoYao = new List<int>
			{
				9,
				10,
				11,
				12,
				13,
				14
			};
			this.CaoYaoList.Add(base.Get<LianDanSlot>("药引"));
			this.CaoYaoList.Add(base.Get<LianDanSlot>("主药1"));
			this.CaoYaoList.Add(base.Get<LianDanSlot>("主药2"));
			this.CaoYaoList.Add(base.Get<LianDanSlot>("辅药1"));
			this.CaoYaoList.Add(base.Get<LianDanSlot>("辅药2"));
			this.WenHao = base.Get<FpBtn>("药引/Bg/问号");
			this.WenHao.mouseEnterEvent.AddListener(new UnityAction(this.ShowTips));
			this.WenHao.mouseOutEvent.AddListener(new UnityAction(this.HideTips));
			this.WenHao.mouseUpEvent.AddListener(new UnityAction(this.HideTips));
			foreach (LianDanSlot lianDanSlot in this.CaoYaoList)
			{
				lianDanSlot.InitUI();
			}
			this._naiJiu = base.Get<Text>("DanLu/NaiJiuDu/Value");
			this.DanLu = base.Get<DanLuSlot>("丹炉");
			this.StartLianDanBtn = base.Get<FpBtn>("开始炼丹");
			this.StartLianDanBtn.mouseUpEvent.AddListener(new UnityAction(this.ClickLianDan));
			this.Select = base.Get<LianDanSelect>("LianDanSelect");
			this.CheckCanMade();
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x001DE92C File Offset: 0x001DCB2C
		public void ShowTips()
		{
			base.Get("药引提示", true).SetActive(true);
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x001DE940 File Offset: 0x001DCB40
		public void HideTips()
		{
			base.Get("药引提示", true).SetActive(false);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x001DE954 File Offset: 0x001DCB54
		public void PutDanLu(DanLuSlot dragSlot)
		{
			if (!this.DanLu.IsNull())
			{
				LianDanUIMag.Instance.DanLuBag.AddTempItem(this.DanLu.Item, 1);
			}
			this.DanLu.SetSlotData(dragSlot.Item);
			LianDanUIMag.Instance.DanLuBag.RemoveTempItem(dragSlot.Item.Uid, dragSlot.Item.Count);
			this.NaiJiu = this.DanLu.Item.Seid["NaiJiu"].I;
			this._naiJiu.SetText(string.Format("{0}/100", this.NaiJiu));
			int imgQuality = this.DanLu.Item.GetImgQuality();
			this.MaxNum = this.MaxCaoYao[imgQuality - 1];
			this.BackAllCaoYao();
			foreach (LianDanSlot lianDanSlot in this.CaoYaoList)
			{
				lianDanSlot.SetIsLock(false);
			}
			if (imgQuality == 1)
			{
				this.CaoYaoList[2].SetIsLock(true);
				this.CaoYaoList[4].SetIsLock(true);
			}
			else if (imgQuality < 6)
			{
				this.CaoYaoList[2].SetIsLock(true);
			}
			this.UpdateUI();
			this.CheckCanMade();
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x001DEAC0 File Offset: 0x001DCCC0
		public void BackDanLu(DanLuSlot dragSlot)
		{
			LianDanUIMag.Instance.DanLuBag.AddTempItem(dragSlot.Item, 1);
			dragSlot.SetNull();
			foreach (LianDanSlot lianDanSlot in this.CaoYaoList)
			{
				lianDanSlot.SetIsLock(true);
			}
			this._naiJiu.SetText("0/100");
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x001DEB40 File Offset: 0x001DCD40
		public void ZhaLuCallBack()
		{
			this.DanLu.SetNull();
			foreach (LianDanSlot lianDanSlot in this.CaoYaoList)
			{
				lianDanSlot.SetIsLock(true);
			}
			this._naiJiu.SetText("0/100");
			this.DanLu.UpdateNaiJiu();
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x001DEBB8 File Offset: 0x001DCDB8
		public void PutCaoYao(LianDanSlot dragSlot)
		{
			LianDanSlot toSlot = LianDanUIMag.Instance.CaoYaoBag.ToSlot;
			if (!dragSlot.Item.CanSale)
			{
				UIPopTip.Inst.Pop("此物品无法交易", PopTipIconType.叹号);
				return;
			}
			if (dragSlot.Item.Count < 1)
			{
				UIPopTip.Inst.Pop("此物品数量小于0寄卖", PopTipIconType.叹号);
				return;
			}
			int setCount = 0;
			if (Input.GetKey(304) || Input.GetKey(303))
			{
				setCount = 5;
				if (dragSlot.Item.Count < 5)
				{
					setCount = dragSlot.Item.Count;
				}
			}
			if (Input.GetKey(306) || Input.GetKey(305))
			{
				setCount = dragSlot.Item.Count;
			}
			UnityAction unityAction = delegate()
			{
				int num;
				if (setCount > 0)
				{
					num = setCount;
				}
				else
				{
					num = LianDanUIMag.Instance.Select.CurNum;
				}
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
				this.CheckCanMade();
			};
			if (setCount > 0)
			{
				unityAction.Invoke();
				return;
			}
			LianDanUIMag.Instance.Select.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, unityAction, null);
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x001DED00 File Offset: 0x001DCF00
		public void PutCaoYao(int index, int id, int count)
		{
			CaoYaoBag caoYaoBag = LianDanUIMag.Instance.CaoYaoBag;
			BaseItem tempItemById = caoYaoBag.GetTempItemById(id);
			if (tempItemById == null)
			{
				UIPopTip.Inst.Pop("不存该物品", PopTipIconType.叹号);
				Debug.LogError("不存该物品");
				return;
			}
			tempItemById.Count = count;
			this.CaoYaoList[index].SetSlotData(tempItemById);
			caoYaoBag.RemoveTempItem(tempItemById.Uid, count);
			caoYaoBag.UpdateItem(false);
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x001DED6C File Offset: 0x001DCF6C
		public void BackCaoYao(LianDanSlot dragSlot)
		{
			if (!dragSlot.Item.CanSale)
			{
				UIPopTip.Inst.Pop("此物品无法交易", PopTipIconType.叹号);
				return;
			}
			if (dragSlot.Item.Count < 1)
			{
				UIPopTip.Inst.Pop("此物品数量小于0寄卖", PopTipIconType.叹号);
				return;
			}
			int setCount = 0;
			if (Input.GetKey(304) || Input.GetKey(303))
			{
				setCount = 5;
				if (dragSlot.Item.Count < 5)
				{
					setCount = dragSlot.Item.Count;
				}
			}
			if (Input.GetKey(306) || Input.GetKey(305))
			{
				setCount = dragSlot.Item.Count;
			}
			UnityAction unityAction = delegate()
			{
				int num;
				if (setCount > 0)
				{
					num = setCount;
				}
				else
				{
					num = LianDanUIMag.Instance.Select.CurNum;
				}
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
				this.CheckCanMade();
			};
			if (setCount > 0)
			{
				unityAction.Invoke();
				return;
			}
			LianDanUIMag.Instance.Select.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, unityAction, null);
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x001DEEA0 File Offset: 0x001DD0A0
		public void UpdateUI()
		{
			foreach (LianDanSlot lianDanSlot in this.CaoYaoList)
			{
				lianDanSlot.UpdateYaoXin();
			}
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x001DEEF0 File Offset: 0x001DD0F0
		public void DanLuUI()
		{
			if (this.DanLu.IsNull())
			{
				this.NaiJiu = 0;
			}
			else
			{
				this.NaiJiu = this.DanLu.Item.Seid["NaiJiu"].I;
			}
			this.DanLu.UpdateNaiJiu();
			this._naiJiu.SetText(string.Format("{0}/100", this.NaiJiu));
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x001DEF64 File Offset: 0x001DD164
		public void BackAllCaoYao()
		{
			LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
			foreach (LianDanSlot lianDanSlot in this.CaoYaoList)
			{
				lianDanSlot.SetNull();
			}
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x001DEFC4 File Offset: 0x001DD1C4
		public void CheckCanMade()
		{
			if (this.DanLu.IsNull())
			{
				base.Get("开始炼丹/CanClick", true).SetActive(false);
				base.Get("开始炼丹/UnClick", true).SetActive(true);
				this.StartLianDanBtn.SetCanClick(false);
				return;
			}
			bool flag = false;
			using (List<LianDanSlot>.Enumerator enumerator = this.CaoYaoList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsNull())
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				base.Get("开始炼丹/CanClick", true).SetActive(false);
				base.Get("开始炼丹/UnClick", true).SetActive(true);
				this.StartLianDanBtn.SetCanClick(false);
				return;
			}
			base.Get("开始炼丹/CanClick", true).SetActive(true);
			base.Get("开始炼丹/UnClick", true).SetActive(false);
			this.StartLianDanBtn.SetCanClick(true);
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x001DF0C0 File Offset: 0x001DD2C0
		public void ClickLianDan()
		{
			int num = 0;
			foreach (LianDanSlot lianDanSlot in this.CaoYaoList)
			{
				if (!lianDanSlot.IsNull())
				{
					num += lianDanSlot.Item.Count;
				}
			}
			if (num > this.MaxNum)
			{
				UIPopTip.Inst.Pop(string.Format("该品阶丹炉最大药材数{0}个", this.MaxNum), PopTipIconType.叹号);
				return;
			}
			this.SelectNum = 1;
			this.Select.Init(this.GetLianDanName(), this.GetCanMadeNum(), delegate
			{
				this.SelectNum = this.Select.CurNum;
				if (this.SelectNum <= 0)
				{
					return;
				}
				this.StartLianDan();
			}, null);
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x001DF17C File Offset: 0x001DD37C
		public void StartLianDan()
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
			if (this.SelectNum > 0)
			{
				int lianzhicishu = this.SelectNum;
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
					this.CostItem(lianzhicishu);
					DanLuSlot danLu = this.DanLu;
					int maxpingzhi;
					if (!danLu.IsNull())
					{
						int quality = _ItemJsonData.DataDict[this.DanLu.Item.Id].quality;
						maxpingzhi = maxpingzhi;
						int num = 0;
						if (maxpingzhi - quality == 0)
						{
							num = this.ReduceNaiJiu[1];
						}
						else if (maxpingzhi - quality == 1)
						{
							num = this.ReduceNaiJiu[2];
						}
						else if (maxpingzhi - quality == 2)
						{
							num = this.ReduceNaiJiu[3];
						}
						else if (maxpingzhi - quality >= 3)
						{
							num = this.ReduceNaiJiu[4];
						}
						else if (maxpingzhi - quality < 0)
						{
							num = this.ReduceNaiJiu[0];
						}
						if (maxpingzhi <= 0)
						{
							num = 2;
						}
						num *= lianzhicishu;
						if (quality > maxpingzhi && avatar.getStaticSkillAddSum(13) != 0)
						{
							num = 0;
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
						if (!danLu.Item.Seid.HasField("NaiJiu"))
						{
							UIPopTip.Inst.Pop("丹炉错误", PopTipIconType.叹号);
							return;
						}
						int num2 = (int)danLu.Item.Seid["NaiJiu"].n;
						int id = danLu.Item.Id;
						if (num2 - num <= 0)
						{
							avatar.removeItem(danLu.Item.Uid);
							LianDanUIMag.Instance.LianDanResult.ZhaLuLianDan();
							int num3 = (int)jsonData.instance.ItemJsonData[id.ToString()]["quality"].n;
							int num4 = avatar.HP - list2[num3 - 1];
							if (num4 <= 0)
							{
								UIDeath.Inst.Show(DeathType.炉毁人亡);
								return;
							}
							avatar.HP = num4;
							return;
						}
						else
						{
							foreach (ITEM_INFO item_INFO in avatar.itemList.values)
							{
								if (item_INFO.uuid == danLu.Item.Uid)
								{
									item_INFO.Seid.SetField("NaiJiu", num2 - num);
									danLu.Item.Seid.SetField("NaiJiu", num2 - num);
								}
							}
							LianDanUIMag.Instance.DanLuBag.CreateTempList();
							LianDanUIMag.Instance.DanLuBag.UpdateItem(false);
						}
					}
					Dictionary<int, int> dictionary = new Dictionary<int, int>();
					if (danFangItemID != null)
					{
						Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
						Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
						if (DanYaoItemList[0].ItemID > 0)
						{
							int i = danFangItemID["value1"].I;
						}
						for (int j = 1; j <= 5; j++)
						{
							if ((int)danFangItemID["value" + j].n != 0)
							{
								JSONObject jsonobject = jsonData.instance.ItemJsonData[((int)danFangItemID["value" + j].n).ToString()];
								int num5 = (int)jsonobject["yaoZhi" + indexToLeixin[j - 1]].n;
								int num6 = (int)jsonobject["quality"].n;
								int num7 = (int)danFangItemID["num" + j].n * this.YaoZhi[num6 - 1];
								int key = num5;
								if (j == 1)
								{
									if (DanYaoItemList[j - 1].ItemID > 0 && DanYaoItemList[j - 1].YaoZhi - num7 > 0)
									{
										dictionary[DanYaoItemList[j - 1].YaoZhiType] = DanYaoItemList[j - 1].YaoZhi - num7;
									}
								}
								else if (j == 2 || j == 3)
								{
									Tools.dictionaryAddNum(dictionary3, key, num7);
								}
								else if (j == 4 || j == 5)
								{
									Tools.dictionaryAddNum(dictionary2, key, num7);
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
						using (Dictionary<int, int>.KeyCollection.Enumerator enumerator2 = zhuyaoList.Keys.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								int key3 = enumerator2.Current;
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
							goto IL_6EC;
						}
					}
					for (int k = 1; k <= 5; k++)
					{
						if (DanYaoItemList[k - 1].ItemID > 0)
						{
							dictionary[DanYaoItemList[k - 1].YaoZhiType] = DanYaoItemList[k - 1].YaoZhi;
						}
					}
					IL_6EC:
					if (maxNum != 1)
					{
						if (maxpingzhi == 0)
						{
							int num8 = 0;
							foreach (KeyValuePair<int, int> keyValuePair in dictionary)
							{
								if (keyValuePair.Key > 0)
								{
									this.FinishAddItem(num8, keyValuePair.Key + 6900, keyValuePair.Value * lianzhicishu);
									num8++;
								}
							}
							this.Fail(13, lianzhicishu);
							return;
						}
						this.FinishAddItem(0, maxpingzhi + 5900, lianzhicishu);
						avatar.addItem(maxpingzhi + 5900, lianzhicishu, Tools.CreateItemSeid(danFangItemID["ItemID"].I), false);
						this.Fail(15, lianzhicishu);
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
							for (int l = 2; l <= 5; l++)
							{
								if (DanYaoItemList[l - 1].ItemID > 0)
								{
									int num11 = (int)jsonData.instance.ItemJsonData[DanYaoItemList[l - 1].ItemID.ToString()]["yaoZhi1"].n;
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
							this.FinishAddItem(0, maxpingzhi + 5906, lianzhicishu);
							avatar.addItem(maxpingzhi + 5906, lianzhicishu, Tools.CreateItemSeid(maxpingzhi + 5906), false);
							this.Fail(14, lianzhicishu);
							return;
						}
						JSONObject jsonobject2 = jsonData.instance.ItemJsonData[((int)danFangItemID["value1"].n).ToString()];
						float n = jsonobject2["yaoZhi1"].n;
						int num12 = (int)jsonobject2["quality"].n;
						int num13 = (int)danFangItemID["num1"].n * this.YaoZhi[num12 - 1];
						if (DanYaoItemList[0].YaoZhi < num13)
						{
							this.FinishAddItem(0, maxpingzhi + 5906, lianzhicishu);
							avatar.addItem(maxpingzhi + 5906, lianzhicishu, Tools.CreateItemSeid(maxpingzhi + 5906), false);
							this.Fail(14, lianzhicishu);
							return;
						}
						if (maxNum == 1 && maxpingzhi > 0)
						{
							if (danFangItemID != null)
							{
								List<int> list4 = new List<int>();
								List<int> list5 = new List<int>();
								for (int m = 1; m <= 5; m++)
								{
									if (DanYaoItemList[m - 1].ItemID <= 0)
									{
										list4.Add(0);
										list5.Add(0);
									}
									else
									{
										list4.Add(DanYaoItemList[m - 1].ItemID);
										list5.Add(DanYaoItemList[m - 1].ItemNum);
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
								LianDanUIMag.Instance.DanFangPanel.AddDanFang(jsonobject3);
							}
							int num16 = 0;
							if (avatar.getStaticSkillAddSum(17) != 0)
							{
								lianzhicishu *= 2;
							}
							this.FinishAddItem(num16, danFangItemID["ItemID"].I, lianzhicishu);
							num16++;
							foreach (KeyValuePair<int, int> keyValuePair2 in dictionary)
							{
								if (keyValuePair2.Key > 0)
								{
									this.FinishAddItem(num16, keyValuePair2.Key + 6900, keyValuePair2.Value * lianzhicishu);
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
								this.Success(2 + (maxpingzhi - 1) * 2, lianzhicishu);
							}
							else
							{
								this.Success(1 + (maxpingzhi - 1) * 2, lianzhicishu);
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

		// Token: 0x060046CB RID: 18123 RVA: 0x001DF358 File Offset: 0x001DD558
		public void GetYaoLeiList(List<int> indexToLeixin, List<LianDanResultManager.DanyaoItem> DanYaoItemList, Dictionary<int, int> fuyaoList, Dictionary<int, int> zhuyaoList, bool unlockYaoXing = false)
		{
			int num = 0;
			for (int i = 0; i < this.CaoYaoList.Count; i++)
			{
				LianDanSlot lianDanSlot = this.CaoYaoList[i];
				LianDanResultManager.DanyaoItem danyaoItem = new LianDanResultManager.DanyaoItem();
				if (!this.CaoYaoList[i].IsNull())
				{
					danyaoItem.ItemID = lianDanSlot.Item.Id;
					danyaoItem.ItemNum = lianDanSlot.Item.Count;
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
					danyaoItem.ItemID = -1;
					danyaoItem.ItemNum = 0;
					danyaoItem.YaoZhi = 0;
					danyaoItem.YaoZhiType = -1;
				}
				DanYaoItemList.Add(danyaoItem);
				num++;
			}
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x001DF4C4 File Offset: 0x001DD6C4
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

		// Token: 0x060046CD RID: 18125 RVA: 0x001DF724 File Offset: 0x001DD924
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

		// Token: 0x060046CE RID: 18126 RVA: 0x001DF7EC File Offset: 0x001DD9EC
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
					if (!this.CaoYaoList[j].IsNull())
					{
						num2 = this.CaoYaoList[j].Item.Id;
						num3 = this.CaoYaoList[j].Item.Count;
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

		// Token: 0x060046CF RID: 18127 RVA: 0x001DF934 File Offset: 0x001DDB34
		public int GetCanMadeNum()
		{
			int num = 10000000;
			Avatar player = Tools.instance.getPlayer();
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (LianDanSlot lianDanSlot in this.CaoYaoList)
			{
				if (!lianDanSlot.IsNull())
				{
					if (dictionary.ContainsKey(lianDanSlot.Item.Id))
					{
						Dictionary<int, int> dictionary2 = dictionary;
						int id = lianDanSlot.Item.Id;
						dictionary2[id] += lianDanSlot.Item.Count;
					}
					else
					{
						dictionary.Add(lianDanSlot.Item.Id, lianDanSlot.Item.Count);
					}
				}
			}
			foreach (int num2 in dictionary.Keys)
			{
				int num3 = player.getItemNum(num2) / dictionary[num2];
				if (num3 < num)
				{
					num = num3;
				}
			}
			return num;
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x001DFA60 File Offset: 0x001DDC60
		public void CostItem(int lianzhicishu)
		{
			Avatar player = Tools.instance.getPlayer();
			int num = 0;
			foreach (LianDanSlot lianDanSlot in this.CaoYaoList)
			{
				if (!lianDanSlot.IsNull())
				{
					if (num == 0)
					{
						player.AddYaoCaiShuXin(lianDanSlot.Item.Id, 1);
					}
					else if (num <= 2)
					{
						player.AddYaoCaiShuXin(lianDanSlot.Item.Id, 2);
					}
					else if (num <= 4)
					{
						player.AddYaoCaiShuXin(lianDanSlot.Item.Id, 3);
					}
					player.removeItem(lianDanSlot.Item.Id, lianDanSlot.Item.Count * lianzhicishu);
				}
				lianDanSlot.SetNull();
				num++;
			}
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x001DFB34 File Offset: 0x001DDD34
		public void FinishAddItem(int index, int id, int lianzhicishu)
		{
			BaseItem slotData = BaseItem.Create(id, lianzhicishu, Tools.getUUID(), Tools.CreateItemSeid(id));
			LianDanUIMag.Instance.LianDanResult.SlotList[index].SetSlotData(slotData);
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x001DFB6F File Offset: 0x001DDD6F
		private void Fail(int index, int num)
		{
			LianDanUIMag.Instance.LianDanResult.Fail(index, num);
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x001DFB82 File Offset: 0x001DDD82
		private void Success(int index, int num)
		{
			LianDanUIMag.Instance.LianDanResult.Success(index, num);
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x001DFB98 File Offset: 0x001DDD98
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
			float itemPercent = this.GetItemPercent(i);
			if (!player.HasLianZhiDanYao.HasItem(ItemId))
			{
				player.HasLianZhiDanYao.Add(ItemId);
				player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * 4f * itemPercent));
			}
			player.wuDaoMag.addWuDaoEx(21, (int)((float)i2 / (float)list[i - 1] * (float)Num * itemPercent));
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x001DFC88 File Offset: 0x001DDE88
		public float GetItemPercent(int itemQuality)
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

		// Token: 0x060046D6 RID: 18134 RVA: 0x001DFCD4 File Offset: 0x001DDED4
		public string GetCostTime(int count)
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

		// Token: 0x04004817 RID: 18455
		public int NaiJiu;

		// Token: 0x04004818 RID: 18456
		public int MaxNum;

		// Token: 0x04004819 RID: 18457
		public List<LianDanSlot> CaoYaoList;

		// Token: 0x0400481A RID: 18458
		public DanLuSlot DanLu;

		// Token: 0x0400481B RID: 18459
		private Text _naiJiu;

		// Token: 0x0400481C RID: 18460
		public List<int> YaoZhi;

		// Token: 0x0400481D RID: 18461
		public List<int> ReduceNaiJiu;

		// Token: 0x0400481E RID: 18462
		public List<int> MaxCaoYao;

		// Token: 0x0400481F RID: 18463
		public FpBtn StartLianDanBtn;

		// Token: 0x04004820 RID: 18464
		public LianDanSelect Select;

		// Token: 0x04004821 RID: 18465
		public FpBtn WenHao;

		// Token: 0x04004822 RID: 18466
		public int SelectNum = 1;
	}
}
