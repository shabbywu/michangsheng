using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000716 RID: 1814
	public class PaiMaiUiMag : SingletonMono<PaiMaiUiMag>
	{
		// Token: 0x06003A08 RID: 14856 RVA: 0x0018D95C File Offset: 0x0018BB5C
		private void Awake()
		{
			SingletonMono<PaiMaiUiMag>._instance = this;
			this._curRoundAddNum = 0;
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localScale = new Vector3(0.821f, 0.821f, 0.821f);
			base.transform.localPosition = Vector3.zero;
			base.transform.SetAsLastSibling();
			this.WordDict = new Dictionary<int, List<int>>();
			this.AddPriceWordDict = new Dictionary<int, List<int>>();
			int key = 0;
			foreach (PaiMaiDuiHuaBiao paiMaiDuiHuaBiao in PaiMaiDuiHuaBiao.DataList)
			{
				if (paiMaiDuiHuaBiao.huanhua.Count == 2)
				{
					key = paiMaiDuiHuaBiao.huanhua[0] * 10 + paiMaiDuiHuaBiao.huanhua[1];
				}
				else if (paiMaiDuiHuaBiao.huanhua.Count == 1)
				{
					key = paiMaiDuiHuaBiao.huanhua[0] * 10;
				}
				if (this.WordDict.ContainsKey(key))
				{
					this.WordDict[key].Add(paiMaiDuiHuaBiao.id);
				}
				else
				{
					this.WordDict.Add(key, new List<int>
					{
						paiMaiDuiHuaBiao.id
					});
				}
			}
			foreach (PaiMaiNpcAddPriceSay paiMaiNpcAddPriceSay in PaiMaiNpcAddPriceSay.DataList)
			{
				if (this.AddPriceWordDict.ContainsKey(paiMaiNpcAddPriceSay.Type))
				{
					this.AddPriceWordDict[paiMaiNpcAddPriceSay.Type].Add(paiMaiNpcAddPriceSay.id);
				}
				else
				{
					this.AddPriceWordDict.Add(paiMaiNpcAddPriceSay.Type, new List<int>
					{
						paiMaiNpcAddPriceSay.id
					});
				}
			}
			this._round = 0;
			this.Init();
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x0018DB58 File Offset: 0x0018BD58
		public void Init()
		{
			this.ShopIndex = 0;
			PaiMaiShopData paiMaiShopData = (PaiMaiShopData)BindData.Get("PaiMaiData");
			this.PaiMaiId = paiMaiShopData.id;
			this.ShopList = paiMaiShopData.ShopList;
			this._sceneName.text = PaiMaiBiao.DataDict[this.PaiMaiId].Name;
			this.Host.Init();
			this.StateColors = new Dictionary<PaiMaiAvatar.StateType, string>();
			this.StateColors.Add(PaiMaiAvatar.StateType.略感兴趣, "98ffa4");
			this.StateColors.Add(PaiMaiAvatar.StateType.跃跃欲试, "89f7ff");
			this.StateColors.Add(PaiMaiAvatar.StateType.势在必得, "ffb098");
			this.CreatePaiMaiNpcList();
			this.StartPaiMai();
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x0018DC0C File Offset: 0x0018BE0C
		private void CreatePaiMaiNpcList()
		{
			List<int> list = new List<int>();
			PaiMaiCanYuAvatar paiMaiCanYuAvatar = null;
			foreach (PaiMaiCanYuAvatar paiMaiCanYuAvatar2 in PaiMaiCanYuAvatar.DataList)
			{
				if (paiMaiCanYuAvatar2.PaiMaiID == this.PaiMaiId)
				{
					paiMaiCanYuAvatar = paiMaiCanYuAvatar2;
					break;
				}
			}
			if (paiMaiCanYuAvatar == null)
			{
				Debug.LogError(string.Format("找不到拍卖会ID不存在，ID：{0}", this.PaiMaiId));
				return;
			}
			int num = paiMaiCanYuAvatar.AvatrNum;
			if (paiMaiCanYuAvatar.Jie != 0 && (paiMaiCanYuAvatar.Jie == -1 || paiMaiCanYuAvatar.Jie == Tools.instance.getPlayer().StreamData.PaiMaiDataMag.PaiMaiDict[this.PaiMaiId].No))
			{
				foreach (int num2 in paiMaiCanYuAvatar.AvatrID)
				{
					if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(num2))
					{
						list.Add(NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[num2]);
					}
					else
					{
						if (num2 < 20000)
						{
							continue;
						}
						list.Add(num2);
					}
					num--;
				}
			}
			List<int> paiMaiListByPaiMaiId = NpcJieSuanManager.inst.GetPaiMaiListByPaiMaiId(this.PaiMaiId);
			if (paiMaiListByPaiMaiId.Count > 0)
			{
				int num3 = 0;
				while (num3 < paiMaiListByPaiMaiId.Count && num > 0)
				{
					list.Add(paiMaiListByPaiMaiId[num3]);
					num--;
					num3++;
				}
			}
			if (num > 0)
			{
				foreach (PaiMaiOldAvatar paiMaiOldAvatar in PaiMaiOldAvatar.DataList)
				{
					if (num == 0)
					{
						break;
					}
					if (!jsonData.instance.MonstarIsDeath(paiMaiOldAvatar.id) && Tools.instance.GetRandomInt(0, 100) <= paiMaiOldAvatar.GaiLv)
					{
						list.Add(paiMaiOldAvatar.id);
						num--;
					}
				}
			}
			if (num > 0)
			{
				JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
				List<string> list2 = new List<string>();
				using (List<string>.Enumerator enumerator4 = jsonData.instance.AvatarJsonData.keys.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						string text = enumerator4.Current;
						if (int.Parse(text) >= 20000)
						{
							list2.Add(text);
						}
					}
					goto IL_331;
				}
				IL_258:
				string text2 = list2[Tools.instance.GetRandomInt(0, list2.Count - 1)];
				int item = int.Parse(text2);
				if (avatarJsonData[text2]["paimaifenzu"].ToList().Contains(PaiMaiBiao.DataDict[this.PaiMaiId].paimaifenzu) && !avatarJsonData[text2]["isImportant"].b && paiMaiCanYuAvatar.JinJie[0] <= avatarJsonData[text2]["Level"].I && avatarJsonData[text2]["Level"].I <= paiMaiCanYuAvatar.JinJie[1])
				{
					if (!list.Contains(item))
					{
						list.Add(item);
						num--;
					}
					if (num == 0)
					{
						goto IL_338;
					}
				}
				IL_331:
				if (num > 0)
				{
					goto IL_258;
				}
			}
			IL_338:
			int num4 = 0;
			Transform transform = base.transform.Find("NpcList");
			this._avatarCtr.AvatarList = new List<PaiMaiAvatar>();
			foreach (int id in list)
			{
				PaiMaiAvatar paiMaiAvatar = new PaiMaiAvatar(id);
				paiMaiAvatar.UiCtr = transform.GetChild(num4).GetComponent<AvatarUI>();
				paiMaiAvatar.UiCtr.Init(paiMaiAvatar);
				this._avatarCtr.AvatarList.Add(paiMaiAvatar);
				num4++;
			}
			Dictionary<string, PlayerCommand> dictionary = new Dictionary<string, PlayerCommand>();
			Transform transform2 = base.transform.Find("Player/Command");
			Text component = base.transform.Find("Player/Money/Num").GetComponent<Text>();
			CommandTips tips = base.transform.Find("Player/Tips").gameObject.AddComponent<CommandTips>();
			for (int i = 0; i < transform2.childCount; i++)
			{
				dictionary.Add(transform2.GetChild(i).name, transform2.GetChild(i).GetComponent<PlayerCommand>());
			}
			PaiMaiAvatar player = new PaiMaiAvatar(Tools.instance.getPlayer().name);
			this._playerCtr = new PlayerCtr(player, dictionary, component, tips);
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x0018E0D0 File Offset: 0x0018C2D0
		public void PlayerUseCeLue(PlayerCommand command)
		{
			this._ceLueMask.SetActive(true);
			this._ceLueType = command.CeLueType;
			this._avatarCtr.SetCanSelect(this._ceLueType);
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x0018E0FC File Offset: 0x0018C2FC
		public void SelectAvatarCallBack(PaiMaiAvatar avatar)
		{
			this._ceLueMask.SetActive(false);
			this._avatarCtr.StopSelect();
			if (!this.Host.ReduceNaiXin(this._ceLueType))
			{
				this._playerCtr.GiveUpCurShop();
				PaiMaiSayData sayData = new PaiMaiSayData
				{
					Id = 0,
					Msg = "你的恶意行为似乎惹怒了镇守拍卖会的修士，一道冰冷的神识扫过，你顿时动弹不得，惊出一身冷汗。"
				};
				this._fungusSay.Say(sayData);
				return;
			}
			int ceLueType = (int)this._ceLueType;
			int num = 0;
			int num2 = 0;
			if (this._ceLueType == CeLueType.神识威慑)
			{
				if (avatar.ShenShi < Tools.instance.getPlayer().shengShi)
				{
					avatar.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, true);
					num = 1;
					NPCEx.AddFavor(avatar.NpcId, -2, true, true);
				}
				else
				{
					num = 2;
				}
			}
			else if (this._ceLueType == CeLueType.言语恐吓)
			{
				if (avatar.Level < (int)Tools.instance.getPlayer().level)
				{
					avatar.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, true);
					num = 1;
					NPCEx.AddFavor(avatar.NpcId, -2, true, true);
				}
				else
				{
					num = 2;
				}
			}
			else if (this._ceLueType == CeLueType.出言挑衅)
			{
				if (avatar.State != PaiMaiAvatar.StateType.势在必得)
				{
					avatar.UiCtr.SetState(avatar.State + 1, true);
				}
				num = 1;
				NPCEx.AddFavor(avatar.NpcId, -2, true, true);
				avatar.AddMaxMoney(0.05f);
			}
			if (avatar.NpcId < 20000)
			{
				num2 = 0;
			}
			else if (PlayerEx.IsTheather(avatar.NpcId))
			{
				num2 = 13;
			}
			else if (PlayerEx.IsBrother(avatar.NpcId))
			{
				num2 = 12;
			}
			else if (PlayerEx.IsDaoLv(avatar.NpcId))
			{
				num2 = 11;
			}
			else if (UINPCHeadFavor.GetFavorLevel(NPCEx.GetFavor(avatar.NpcId)) > 5)
			{
				num2 = 1;
			}
			using (List<PaiMaiDuiHuaAI>.Enumerator enumerator = PaiMaiDuiHuaAI.DataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PaiMaiDuiHuaAI data = enumerator.Current;
					if (data.CeLue == ceLueType && data.JieGuo == num && data.MuBiao == num2)
					{
						PaiMaiSayData paiMaiSayData = new PaiMaiSayData();
						if (data.DuiHua.Contains("{pangbai}"))
						{
							paiMaiSayData.Id = 0;
							paiMaiSayData.Msg = data.DuiHua.Replace("{pangbai}", "");
						}
						else
						{
							paiMaiSayData.Id = 1;
							paiMaiSayData.Msg = data.DuiHua;
						}
						if (data.HuiFu != "")
						{
							paiMaiSayData.Action = delegate()
							{
								PaiMaiSayData paiMaiSayData2 = new PaiMaiSayData();
								paiMaiSayData2.Id = avatar.NpcId;
								paiMaiSayData2.Msg = data.HuiFu;
								this._fungusSay.Say(paiMaiSayData2);
							};
						}
						this._fungusSay.Say(paiMaiSayData);
						break;
					}
				}
			}
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x0018E440 File Offset: 0x0018C640
		public void CancelUserCelue()
		{
			this._ceLueMask.SetActive(false);
			this._avatarCtr.StopSelect();
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x0018E45C File Offset: 0x0018C65C
		private void StartPaiMai()
		{
			this.ShopIndex = 0;
			this.CurShop = this.ShopList[this.ShopIndex];
			this.ItemUI.UpdateItem();
			this._avatarCtr.AllAvatarThinkItem();
			this.Host.SayWord("很荣幸大家来到本场拍卖会", delegate
			{
				this.Host.SayWord(string.Concat(new string[]
				{
					"下面是本场拍卖会的第",
					this.ShopIndex.ToCNNumber(),
					"件拍品，",
					string.Format("{0}{1}。底价<color=#096272>{2}</color>灵石，", this.GetDesc(), this.CurShop.ShopName, this.CurShop.CurPrice),
					string.Format("每次加价不得少于<color=#096272>{0}</color>灵石。", this.CurShop.MinAddPrice)
				}), delegate
				{
					this._avatarCtr.AvatarSayWord();
					this._avatarCtr.AvatarStart();
				}, 2f);
			}, 1f);
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x0018E4C0 File Offset: 0x0018C6C0
		private string GetDesc()
		{
			string result = "";
			foreach (PaiMaiMiaoShuBiao paiMaiMiaoShuBiao in PaiMaiMiaoShuBiao.DataList)
			{
				if (paiMaiMiaoShuBiao.Type == this.CurShop.Type && paiMaiMiaoShuBiao.Type2 == this.CurShop.BaseQuality)
				{
					result = paiMaiMiaoShuBiao.Text + paiMaiMiaoShuBiao.Text2;
					break;
				}
			}
			return result;
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x0018E54C File Offset: 0x0018C74C
		public bool AddPrice(int type = 0)
		{
			int num2;
			if (this.CurAvatar.IsPlayer)
			{
				int num = PaiMaiChuJia.DataDict[type].ZhanBi * this.CurShop.Price / 100;
				if (num < 1)
				{
					num = 1;
				}
				num2 = this.CurShop.CurPrice + num;
				if (this.CurAvatar.Money < num2)
				{
					UIPopTip.Inst.Pop("灵石不足", PopTipIconType.叹号);
					return false;
				}
			}
			else
			{
				if (this.CurAvatar.State == PaiMaiAvatar.StateType.略感兴趣 && this._curRoundAddNum != 0)
				{
					int key = this.AddPriceWordDict[0][Tools.instance.GetRandomInt(0, this.AddPriceWordDict[0].Count - 1)];
					this.CurAvatar.UiCtr.SayWord(PaiMaiNpcAddPriceSay.DataDict[key].ChuJiaDuiHua);
					return false;
				}
				if (this.CurShop.Owner != null && this.CurShop.Owner.NpcId == this.CurAvatar.NpcId)
				{
					int key2 = this.AddPriceWordDict[99][Tools.instance.GetRandomInt(0, this.AddPriceWordDict[99].Count - 1)];
					this.CurAvatar.UiCtr.SayWord(PaiMaiNpcAddPriceSay.DataDict[key2].ChuJiaDuiHua);
					return false;
				}
				type = this.CurAvatar.State - PaiMaiAvatar.StateType.放弃;
				int num = PaiMaiChuJia.DataDict[type].ZhanBi * this.CurShop.Price / 100;
				if (num < 1)
				{
					num = 1;
				}
				num2 = this.CurShop.CurPrice + num;
				if (num2 > this.CurAvatar.MaxPrice)
				{
					type = 1;
					num = PaiMaiChuJia.DataDict[type].ZhanBi * this.CurShop.Price / 100;
					if (num < 1)
					{
						num = 1;
					}
					num2 = this.CurShop.CurPrice + num;
					if (num2 > this.CurAvatar.MaxPrice)
					{
						this.CurAvatar.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, true);
						return false;
					}
				}
			}
			if (!this.CurAvatar.IsPlayer)
			{
				int key3 = this.AddPriceWordDict[this.CurAvatar.State - PaiMaiAvatar.StateType.放弃][Tools.instance.GetRandomInt(0, this.AddPriceWordDict[this.CurAvatar.State - PaiMaiAvatar.StateType.放弃].Count - 1)];
				this.CurAvatar.UiCtr.SayWord(PaiMaiNpcAddPriceSay.DataDict[key3].ChuJiaDuiHua.Replace("{price}", string.Format("<color=#096272>{0}</color>", num2)));
			}
			else
			{
				string text = string.Format("我出价<color=#096272>{0}</color>灵石", num2);
				if (type >= 3)
				{
					text += "!";
				}
				else
				{
					text += "。";
				}
				this._playerSayCtr.SayWord(text, null, 1f);
			}
			this.CurShop.CurPrice = num2;
			this.CurShop.Owner = this.CurAvatar;
			this._curRoundAddNum++;
			this._avatarCtr.AddTagetStateMaxPrice(this.CurAvatar, PaiMaiAvatar.StateType.势在必得, 20, 0.01f);
			if (type == 4)
			{
				this._avatarCtr.AddTagetStateMaxPrice(this.CurAvatar, PaiMaiAvatar.StateType.所有状态, 100, -0.03f);
			}
			this.AddPriceCallBack(type);
			return true;
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x0018E898 File Offset: 0x0018CA98
		private void AddPriceCallBack(int type)
		{
			this.ItemUI.UpdateUI();
			PaiMaiChuJiaAI paiMaiChuJiaAI = PaiMaiChuJiaAI.DataDict[type];
			if (paiMaiChuJiaAI.Type.Count < 1)
			{
				return;
			}
			foreach (PaiMaiAvatar paiMaiAvatar in this._avatarCtr.AvatarList)
			{
				if (paiMaiAvatar.NpcId != this.CurAvatar.NpcId)
				{
					int num = paiMaiChuJiaAI.Type.IndexOf((int)paiMaiAvatar.State);
					if (num >= 0 && Tools.instance.GetRandomInt(0, 100) <= paiMaiChuJiaAI.GaiLv[num])
					{
						if (paiMaiChuJiaAI.YingXiang[num] == 1)
						{
							paiMaiAvatar.UiCtr.SetState(paiMaiAvatar.State + 1, true);
						}
						else if (paiMaiChuJiaAI.YingXiang[num] == 2)
						{
							paiMaiAvatar.UiCtr.SetState(paiMaiAvatar.State - 1, true);
						}
					}
				}
			}
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x0018E9A4 File Offset: 0x0018CBA4
		public void EndRound()
		{
			if (this.CurAvatar == null)
			{
				Debug.LogError("当前CurAvatar为空");
				return;
			}
			if (this.CurAvatar.IsPlayer)
			{
				if (this._avatarCtr.IsAllGiveUp() && this.CurShop.Owner != null && this.CurShop.Owner.IsPlayer)
				{
					this.EndCurShop();
					return;
				}
				if (this._curRoundAddNum == 0)
				{
					this.EndCurShop();
					return;
				}
				this.NextRound();
				return;
			}
			else if (this._playerCtr.IsCurShopQuickEnd || this._playerCtr.IsAllQuickEnd)
			{
				if (this._curRoundAddNum == 0)
				{
					this.EndCurShop();
					return;
				}
				this.NextRound();
				return;
			}
			else
			{
				if (this.CurShop.Owner != null && this.CurShop.Owner.IsPlayer && this._avatarCtr.IsAllGiveUp())
				{
					this.EndCurShop();
					return;
				}
				this._playerCtr.StartAction();
				return;
			}
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x0018EA88 File Offset: 0x0018CC88
		private void NextRound()
		{
			this.AddRound();
			this._curRoundAddNum = 0;
			this._avatarCtr.AvatarStart();
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x0018EAA4 File Offset: 0x0018CCA4
		private void EndCurShop()
		{
			this.ShopIndex++;
			if (this.ItemUI.Owner == null)
			{
				if (this.CurShop.IsPlayer)
				{
					Tools.instance.getPlayer().addItem(this.CurShop.ShopId, this.CurShop.Count, this.CurShop.Seid, false);
				}
				this.Host.SayWord("由于没有任何人出价，我宣布，" + this.CurShop.ShopName + "流拍。", new UnityAction(this.NextShop), 2f);
				return;
			}
			this.CurAvatar = this.ItemUI.Owner;
			this.CurAvatar.Money -= this.CurShop.CurPrice;
			if (this.ItemUI.Owner.IsPlayer)
			{
				this._playerCtr.BuyShop();
			}
			else
			{
				if (this.CurAvatar.NpcId >= 20000)
				{
					NpcJieSuanManager.inst.npcSetField.AddNpcMoney(this.CurAvatar.NpcId, -this.CurShop.CurPrice);
				}
				NpcJieSuanManager.inst.AddItemToNpcBackpack(this.CurAvatar.NpcId, this.CurShop.ShopId, this.CurShop.Count, this.CurShop.Seid, true);
			}
			Debug.Log("");
			if (this.CurShop.IsPlayer)
			{
				this._playerCtr.MallShop();
			}
			this.Host.SayWord(string.Format("恭喜{0}道友以{1}灵石的价格拍得这件宝物。", this.ItemUI.Owner.Name, this.CurShop.CurPrice), new UnityAction(this.NextShop), 2f);
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x0018EC68 File Offset: 0x0018CE68
		private void NextShop()
		{
			this.RestartRound();
			if (this.ShopIndex >= this.ShopList.Count)
			{
				this.EndPaiMai();
				return;
			}
			this.CurShop = this.ShopList[this.ShopIndex];
			this.ItemUI.UpdateItem();
			this.Host.AddNaiXin();
			this._avatarCtr.AllAvatarThinkItem();
			if (this._playerCtr.IsCurShopQuickEnd)
			{
				this._playerCtr.IsCurShopQuickEnd = false;
				Time.timeScale = 1f;
			}
			this._playerCtr.RestartGiveUpCurShop();
			if (this.ShopIndex == this.ShopList.Count - 1)
			{
				this.Host.SayWord("下面是本场拍卖会的压轴拍品，" + string.Format("{0}{1}。底价{2}灵石，", this.GetDesc(), this.CurShop.ShopName, this.CurShop.CurPrice) + string.Format("每次加价不得少于{0}灵石。", this.CurShop.MinAddPrice), delegate
				{
					this._avatarCtr.AvatarSayWord();
					this._avatarCtr.AvatarStart();
				}, 2f);
				return;
			}
			this.Host.SayWord(string.Concat(new string[]
			{
				"下面是本场拍卖会的第",
				(this.ShopIndex + 1).ToCNNumber(),
				"件拍品，",
				string.Format("{0}{1}。底价{2}灵石，", this.GetDesc(), this.CurShop.ShopName, this.CurShop.CurPrice),
				string.Format("每次加价不得少于{0}灵石。", this.CurShop.MinAddPrice)
			}), delegate
			{
				this._avatarCtr.AvatarSayWord();
				this._avatarCtr.AvatarStart();
			}, 2f);
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x0018EE10 File Offset: 0x0018D010
		private void EndPaiMai()
		{
			foreach (PaiMaiShop paiMaiShop in this.ShopList)
			{
				if (paiMaiShop.Owner != null)
				{
					Text paiMaiJiLu = this._paiMaiJiLu;
					paiMaiJiLu.text = string.Concat(new string[]
					{
						paiMaiJiLu.text,
						"<color=#1c5e4c>",
						paiMaiShop.Owner.Name,
						"</color>",
						string.Format("以<color=#1c5e4c>{0}</color>灵石的价格购买了", paiMaiShop.CurPrice),
						"<color=#1c5e4c>",
						paiMaiShop.ShopName,
						"</color>\n"
					});
				}
				else
				{
					Text paiMaiJiLu2 = this._paiMaiJiLu;
					paiMaiJiLu2.text = paiMaiJiLu2.text + "物品<color=#1c5e4c>" + paiMaiShop.ShopName + "</color>无人竞价已流拍\n";
				}
			}
			Time.timeScale = 1f;
			this.Host.SayWord("本次拍卖到此结束，感谢诸位道友的参与。", delegate
			{
				this._endPanel.SetActive(true);
			}, 2f);
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x0018EF30 File Offset: 0x0018D130
		private void AddRound()
		{
			this._round++;
			this._roundText.text = "第" + this._round.ToCNNumber() + "轮";
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x0018EF65 File Offset: 0x0018D165
		private void RestartRound()
		{
			this._round = 1;
			this._roundText.text = "第" + this._round.ToCNNumber() + "轮";
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x0018EF93 File Offset: 0x0018D193
		public void QuickEnd()
		{
			if (this._playerCtr.IsAllQuickEnd)
			{
				return;
			}
			Time.timeScale = 100f;
			this._playerCtr.IsAllQuickEnd = true;
			if (this._playerCtr.CanAction)
			{
				this.EndRound();
			}
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x0018EFCC File Offset: 0x0018D1CC
		public void Close()
		{
			Object.Destroy(base.gameObject);
			Time.timeScale = 1f;
			Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastScence, true);
		}

		// Token: 0x04003211 RID: 12817
		public int PaiMaiId;

		// Token: 0x04003212 RID: 12818
		[SerializeField]
		private AvatarCtr _avatarCtr;

		// Token: 0x04003213 RID: 12819
		[SerializeField]
		private PlayerCtr _playerCtr;

		// Token: 0x04003214 RID: 12820
		[SerializeField]
		private PaiMaiSay _playerSayCtr;

		// Token: 0x04003215 RID: 12821
		[SerializeField]
		public FungusSay _fungusSay;

		// Token: 0x04003216 RID: 12822
		public List<PaiMaiShop> ShopList;

		// Token: 0x04003217 RID: 12823
		public List<Sprite> StateSprites;

		// Token: 0x04003218 RID: 12824
		public List<Sprite> HostSprites;

		// Token: 0x04003219 RID: 12825
		public Dictionary<PaiMaiAvatar.StateType, string> StateColors;

		// Token: 0x0400321A RID: 12826
		public PaiMaiShop CurShop;

		// Token: 0x0400321B RID: 12827
		public int ShopIndex;

		// Token: 0x0400321C RID: 12828
		public PaiMaiAvatar CurAvatar;

		// Token: 0x0400321D RID: 12829
		public PaiMaiHost Host;

		// Token: 0x0400321E RID: 12830
		public PaiMaiItem ItemUI;

		// Token: 0x0400321F RID: 12831
		public Dictionary<int, List<int>> WordDict;

		// Token: 0x04003220 RID: 12832
		public Dictionary<int, List<int>> AddPriceWordDict;

		// Token: 0x04003221 RID: 12833
		private int _curRoundAddNum;

		// Token: 0x04003222 RID: 12834
		private int _round;

		// Token: 0x04003223 RID: 12835
		private CeLueType _ceLueType;

		// Token: 0x04003224 RID: 12836
		[SerializeField]
		private Text _roundText;

		// Token: 0x04003225 RID: 12837
		[SerializeField]
		private GameObject _ceLueMask;

		// Token: 0x04003226 RID: 12838
		[SerializeField]
		private GameObject _endPanel;

		// Token: 0x04003227 RID: 12839
		[SerializeField]
		private Text _paiMaiJiLu;

		// Token: 0x04003228 RID: 12840
		[SerializeField]
		private Text _sceneName;
	}
}
