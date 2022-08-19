using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JiaoYi
{
	// Token: 0x02000731 RID: 1841
	public class JiaoYiUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x06003AA3 RID: 15011 RVA: 0x001930C4 File Offset: 0x001912C4
		private void Awake()
		{
			JiaoYiUIMag.Inst = this;
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x001930D7 File Offset: 0x001912D7
		public void Init(int npcId)
		{
			this.NpcId = NPCEx.NPCIDToNew(npcId);
			this.InitPlayerData();
			this.InitNpcData();
			base.transform.SetAsLastSibling();
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x001930FC File Offset: 0x001912FC
		public void Init(int npcId, UnityAction action)
		{
			this.NpcId = npcId;
			this.InitPlayerData();
			this.InitNpcData();
			base.transform.SetAsLastSibling();
			this.CloseAction = action;
			this.PlayerBag.UpdateMoney();
			this.NpcBag.UpdateMoney();
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x00193139 File Offset: 0x00191339
		private void InitPlayerData()
		{
			this.PlayerName.SetText(Tools.GetPlayerName());
			this.PlayerTitle.SetText(Tools.GetPlayerTitle());
			this.PlayerBag.Init(this.NpcId, true);
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x00193170 File Offset: 0x00191370
		private void InitNpcData()
		{
			NpcJieSuanManager.inst.SortNpcPack(this.NpcId);
			this.NpcName.text = jsonData.instance.AvatarRandomJsonData[this.NpcId.ToString()]["Name"].Str;
			this.NpcTitle.text = jsonData.instance.AvatarJsonData[this.NpcId.ToString()]["Title"].Str;
			this.NpcFace.SetNPCFace(this.NpcId);
			this.NpcBag.Init(this.NpcId, false);
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x00193218 File Offset: 0x00191418
		public void SellItem(JiaoYiSlot dragSlot, JiaoYiSlot toSlot = null)
		{
			if (!dragSlot.Item.CanSale)
			{
				UIPopTip.Inst.Pop("此物品无法交易", PopTipIconType.叹号);
				return;
			}
			if (toSlot != null && !toSlot.IsNull() && (dragSlot.Item.Id != toSlot.Item.Id || dragSlot.Item.MaxNum < 2))
			{
				return;
			}
			bool isPlayer = dragSlot.IsPlayer;
			JiaoBag jiaoBag = null;
			if (isPlayer)
			{
				jiaoBag = this.PlayerBag;
			}
			else
			{
				jiaoBag = this.NpcBag;
			}
			if (dragSlot.Item.Count < 1)
			{
				UIPopTip.Inst.Pop("此物品数量小于0，无法交易", PopTipIconType.叹号);
				return;
			}
			if (dragSlot.Item.Count == 1)
			{
				if (toSlot == null)
				{
					toSlot = jiaoBag.GetNullSellList(dragSlot.Item.Uid);
				}
				if (toSlot == null)
				{
					UIPopTip.Inst.Pop("没有空的格子", PopTipIconType.叹号);
					return;
				}
				if (toSlot.IsNull())
				{
					toSlot.SetSlotData(dragSlot.Item.Clone());
					toSlot.Item.Count = 1;
				}
				else
				{
					toSlot.Item.Count++;
				}
				toSlot.UpdateUI();
				jiaoBag.RemoveTempItem(dragSlot.Item.Uid, 1);
				dragSlot.Item.Count--;
				if (dragSlot.Item.Count <= 0)
				{
					jiaoBag.UpdateItem(false);
				}
				this.UpdatePlayerGetMoney();
				return;
			}
			else
			{
				int num = 0;
				if (dragSlot.Item.Count > 1)
				{
					if (Input.GetKey(304) || Input.GetKey(303))
					{
						num = 5;
						if (dragSlot.Item.Count < 5)
						{
							num = dragSlot.Item.Count;
						}
					}
					if (Input.GetKey(306) || Input.GetKey(305))
					{
						num = dragSlot.Item.Count;
					}
				}
				if (num <= 0)
				{
					this.bagItemSelect.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, delegate
					{
						int curNum = this.bagItemSelect.CurNum;
						if (toSlot == null)
						{
							toSlot = jiaoBag.GetNullSellList(dragSlot.Item.Uid);
						}
						if (toSlot == null)
						{
							UIPopTip.Inst.Pop("没有空的格子", PopTipIconType.叹号);
							return;
						}
						if (toSlot.IsNull())
						{
							toSlot.SetSlotData(dragSlot.Item.Clone());
							toSlot.Item.Count = curNum;
						}
						else
						{
							toSlot.Item.Count += curNum;
						}
						toSlot.UpdateUI();
						jiaoBag.RemoveTempItem(dragSlot.Item.Uid, curNum);
						dragSlot.Item.Count -= curNum;
						if (dragSlot.Item.Count <= 0)
						{
							jiaoBag.UpdateItem(false);
						}
						else
						{
							dragSlot.UpdateUI();
						}
						this.UpdatePlayerGetMoney();
					}, null);
					return;
				}
				if (toSlot == null)
				{
					toSlot = jiaoBag.GetNullSellList(dragSlot.Item.Uid);
				}
				if (toSlot == null)
				{
					UIPopTip.Inst.Pop("没有空的格子", PopTipIconType.叹号);
					return;
				}
				if (toSlot.IsNull())
				{
					toSlot.SetSlotData(dragSlot.Item.Clone());
					toSlot.Item.Count = num;
				}
				else
				{
					toSlot.Item.Count += num;
				}
				toSlot.UpdateUI();
				jiaoBag.RemoveTempItem(dragSlot.Item.Uid, num);
				dragSlot.Item.Count -= num;
				if (dragSlot.Item.Count <= 0)
				{
					jiaoBag.UpdateItem(false);
				}
				else
				{
					dragSlot.UpdateUI();
				}
				this.UpdatePlayerGetMoney();
				return;
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x001935F0 File Offset: 0x001917F0
		public void UpdatePlayerGetMoney()
		{
			this.PlayerGetMoney = 0;
			foreach (JiaoYiSlot jiaoYiSlot in this.PlayerBag.SellList)
			{
				if (!jiaoYiSlot.IsNull())
				{
					this.PlayerGetMoney += jiaoYiSlot.Item.GetJiaoYiPrice(this.NpcId, true, false) * jiaoYiSlot.Item.Count;
				}
			}
			foreach (JiaoYiSlot jiaoYiSlot2 in this.NpcBag.SellList)
			{
				if (!jiaoYiSlot2.IsNull())
				{
					this.PlayerGetMoney -= jiaoYiSlot2.Item.GetJiaoYiPrice(this.NpcId, false, false) * jiaoYiSlot2.Item.Count;
				}
			}
			if (this.PlayerGetMoney >= 0)
			{
				this.PlayerGetMoneyText.SetText(string.Format("+{0}", this.PlayerGetMoney));
				return;
			}
			this.PlayerGetMoneyText.SetText(string.Format("{0}", this.PlayerGetMoney));
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x0019373C File Offset: 0x0019193C
		public void BackItem(JiaoYiSlot dragSlot, JiaoYiSlot toSlot = null)
		{
			if (!dragSlot.Item.CanSale)
			{
				UIPopTip.Inst.Pop("此物品无法交易", PopTipIconType.叹号);
				return;
			}
			bool isPlayer = dragSlot.IsPlayer;
			JiaoBag jiaoBag = null;
			if (isPlayer)
			{
				jiaoBag = this.PlayerBag;
			}
			else
			{
				jiaoBag = this.NpcBag;
			}
			if (dragSlot.Item.Count < 1)
			{
				UIPopTip.Inst.Pop("此物品数量小于0，无法交易", PopTipIconType.叹号);
				return;
			}
			if (toSlot == null)
			{
				toSlot = (JiaoYiSlot)jiaoBag.GetNullBagSlot(dragSlot.Item.Uid);
			}
			if (dragSlot.Item.Count == 1)
			{
				jiaoBag.AddTempItem(dragSlot.Item, 1);
				if (toSlot == null)
				{
					jiaoBag.UpdateItem(false);
				}
				else
				{
					if (toSlot.IsNull())
					{
						toSlot.SetSlotData(dragSlot.Item.Clone());
						toSlot.Item.Count = 1;
					}
					else if (toSlot.Item.Id == dragSlot.Item.Id && toSlot.Item.MaxNum > 1)
					{
						toSlot.Item.Count++;
					}
					else
					{
						jiaoBag.UpdateItem(false);
					}
					toSlot.UpdateUI();
				}
				dragSlot.Item.Count--;
				if (dragSlot.Item.Count <= 0)
				{
					dragSlot.SetNull();
				}
				jiaoBag.UpdateItem(false);
				this.UpdatePlayerGetMoney();
				return;
			}
			int num = 0;
			if (dragSlot.Item.Count > 1)
			{
				if (Input.GetKey(304) || Input.GetKey(303))
				{
					num = 5;
					if (dragSlot.Item.Count < 5)
					{
						num = dragSlot.Item.Count;
					}
				}
				if (Input.GetKey(306) || Input.GetKey(305))
				{
					num = dragSlot.Item.Count;
				}
			}
			if (num <= 0)
			{
				this.bagItemSelect.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, delegate
				{
					int curNum = this.bagItemSelect.CurNum;
					jiaoBag.AddTempItem(dragSlot.Item, curNum);
					if (toSlot == null)
					{
						jiaoBag.UpdateItem(false);
					}
					else
					{
						if (toSlot.IsNull())
						{
							toSlot.SetSlotData(dragSlot.Item.Clone());
							toSlot.Item.Count = curNum;
						}
						else if (toSlot.Item.Id == dragSlot.Item.Id && toSlot.Item.MaxNum > 1)
						{
							toSlot.Item.Count += curNum;
						}
						else
						{
							jiaoBag.UpdateItem(false);
						}
						toSlot.UpdateUI();
					}
					dragSlot.Item.Count -= curNum;
					if (dragSlot.Item.Count <= 0)
					{
						dragSlot.SetNull();
					}
					else
					{
						dragSlot.UpdateUI();
					}
					jiaoBag.UpdateItem(false);
					this.UpdatePlayerGetMoney();
				}, null);
				return;
			}
			jiaoBag.AddTempItem(dragSlot.Item, num);
			if (toSlot == null)
			{
				jiaoBag.UpdateItem(false);
			}
			else
			{
				if (toSlot.IsNull())
				{
					toSlot.SetSlotData(dragSlot.Item.Clone());
					toSlot.Item.Count = num;
				}
				else if (toSlot.Item.Id == dragSlot.Item.Id && toSlot.Item.MaxNum > 1)
				{
					toSlot.Item.Count += num;
				}
				else
				{
					jiaoBag.UpdateItem(false);
				}
				toSlot.UpdateUI();
			}
			dragSlot.Item.Count -= num;
			if (dragSlot.Item.Count <= 0)
			{
				dragSlot.SetNull();
			}
			else
			{
				dragSlot.UpdateUI();
			}
			jiaoBag.UpdateItem(false);
			this.UpdatePlayerGetMoney();
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x00193B28 File Offset: 0x00191D28
		public void JiaoYiBtn()
		{
			if (this.NpcSay())
			{
				Tools.instance.getPlayer().AddMoney(this.PlayerGetMoney);
				NpcJieSuanManager.inst.npcSetField.AddNpcMoney(this.NpcId, -this.PlayerGetMoney);
				foreach (JiaoYiSlot jiaoYiSlot in this.PlayerBag.SellList)
				{
					if (!jiaoYiSlot.IsNull())
					{
						Tools.instance.RemoveItem(jiaoYiSlot.Item.Uid, jiaoYiSlot.Item.Count);
						NpcJieSuanManager.inst.AddItemToNpcBackpack(this.NpcId, jiaoYiSlot.Item.Id, jiaoYiSlot.Item.Count, jiaoYiSlot.Item.Seid.Copy(), false);
					}
				}
				foreach (JiaoYiSlot jiaoYiSlot2 in this.NpcBag.SellList)
				{
					if (!jiaoYiSlot2.IsNull())
					{
						Tools.instance.NewAddItem(jiaoYiSlot2.Item.Id, jiaoYiSlot2.Item.Count, jiaoYiSlot2.Item.Seid.Copy(), jiaoYiSlot2.Item.Uid, false);
						NpcJieSuanManager.inst.RemoveItem(this.NpcId, jiaoYiSlot2.Item.Id, jiaoYiSlot2.Item.Count, jiaoYiSlot2.Item.Uid);
					}
				}
				this.NpcBag.JiaoYiCallBack();
				this.PlayerBag.JiaoYiCallBack();
				this.UpdatePlayerGetMoney();
			}
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x00193CEC File Offset: 0x00191EEC
		public bool NpcSay()
		{
			bool result = true;
			int money = this.NpcBag.GetMoney();
			int money2 = this.PlayerBag.GetMoney();
			if (this.PlayerGetMoney >= 0 && money < this.PlayerGetMoney)
			{
				result = false;
				int num = jsonData.instance.getRandom() % 10;
				this.NpcSayText.SetText(Tools.getStr("exchengePlayer" + num));
				this.NpcSayPanel.gameObject.SetActive(true);
				base.Invoke("CloseSay", 1.5f);
			}
			else if (this.PlayerGetMoney < 0 && money2 + this.PlayerGetMoney < 0)
			{
				result = false;
				int num2 = jsonData.instance.getRandom() % 10;
				this.NpcSayText.SetText(Tools.getStr("exchengeMonstar" + num2));
				this.NpcSayPanel.gameObject.SetActive(true);
				base.Invoke("CloseSay", 1.5f);
			}
			return result;
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x00193DE0 File Offset: 0x00191FE0
		public void CloseSay()
		{
			this.NpcSayPanel.gameObject.SetActive(false);
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x00193DF3 File Offset: 0x00191FF3
		public void Close()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			if (this.CloseAction != null)
			{
				this.CloseAction.Invoke();
			}
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x00193E1E File Offset: 0x0019201E
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x040032D2 RID: 13010
		public static JiaoYiUIMag Inst;

		// Token: 0x040032D3 RID: 13011
		public int NpcId;

		// Token: 0x040032D4 RID: 13012
		public JiaoBag PlayerBag;

		// Token: 0x040032D5 RID: 13013
		public PlayerSetRandomFace PlayerFace;

		// Token: 0x040032D6 RID: 13014
		public BagItemSelect bagItemSelect;

		// Token: 0x040032D7 RID: 13015
		public Text PlayerName;

		// Token: 0x040032D8 RID: 13016
		public Text PlayerTitle;

		// Token: 0x040032D9 RID: 13017
		public JiaoBag NpcBag;

		// Token: 0x040032DA RID: 13018
		public PlayerSetRandomFace NpcFace;

		// Token: 0x040032DB RID: 13019
		public Text NpcName;

		// Token: 0x040032DC RID: 13020
		public Text NpcTitle;

		// Token: 0x040032DD RID: 13021
		public int PlayerGetMoney;

		// Token: 0x040032DE RID: 13022
		public Text PlayerGetMoneyText;

		// Token: 0x040032DF RID: 13023
		public GameObject NpcSayPanel;

		// Token: 0x040032E0 RID: 13024
		public Text NpcSayText;

		// Token: 0x040032E1 RID: 13025
		public UnityAction CloseAction;
	}
}
