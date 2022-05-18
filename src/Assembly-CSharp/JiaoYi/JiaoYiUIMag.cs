using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JiaoYi
{
	// Token: 0x02000A91 RID: 2705
	public class JiaoYiUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x0600455D RID: 17757 RVA: 0x000318B8 File Offset: 0x0002FAB8
		private void Awake()
		{
			JiaoYiUIMag.Inst = this;
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x000318CB File Offset: 0x0002FACB
		public void Init(int npcId)
		{
			this.NpcId = npcId;
			this.InitPlayerData();
			this.InitNpcData();
			base.transform.SetAsLastSibling();
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x000318EB File Offset: 0x0002FAEB
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

		// Token: 0x06004560 RID: 17760 RVA: 0x00031928 File Offset: 0x0002FB28
		private void InitPlayerData()
		{
			this.PlayerName.SetText(Tools.GetPlayerName());
			this.PlayerTitle.SetText(Tools.GetPlayerTitle());
			this.PlayerBag.Init(this.NpcId, true);
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x001DABF4 File Offset: 0x001D8DF4
		private void InitNpcData()
		{
			NpcJieSuanManager.inst.SortNpcPack(this.NpcId);
			this.NpcName.text = jsonData.instance.AvatarRandomJsonData[this.NpcId.ToString()]["Name"].Str;
			this.NpcTitle.text = jsonData.instance.AvatarJsonData[this.NpcId.ToString()]["Title"].Str;
			this.NpcFace.SetNPCFace(this.NpcId);
			this.NpcBag.Init(this.NpcId, false);
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x001DAC9C File Offset: 0x001D8E9C
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

		// Token: 0x06004563 RID: 17763 RVA: 0x001DB074 File Offset: 0x001D9274
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

		// Token: 0x06004564 RID: 17764 RVA: 0x001DB1C0 File Offset: 0x001D93C0
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

		// Token: 0x06004565 RID: 17765 RVA: 0x001DB5AC File Offset: 0x001D97AC
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

		// Token: 0x06004566 RID: 17766 RVA: 0x001DB770 File Offset: 0x001D9970
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

		// Token: 0x06004567 RID: 17767 RVA: 0x0003195C File Offset: 0x0002FB5C
		public void CloseSay()
		{
			this.NpcSayPanel.gameObject.SetActive(false);
		}

		// Token: 0x06004568 RID: 17768 RVA: 0x0003196F File Offset: 0x0002FB6F
		public void Close()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			if (this.CloseAction != null)
			{
				this.CloseAction.Invoke();
			}
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x0003199A File Offset: 0x0002FB9A
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003D83 RID: 15747
		public static JiaoYiUIMag Inst;

		// Token: 0x04003D84 RID: 15748
		public int NpcId;

		// Token: 0x04003D85 RID: 15749
		public JiaoBag PlayerBag;

		// Token: 0x04003D86 RID: 15750
		public PlayerSetRandomFace PlayerFace;

		// Token: 0x04003D87 RID: 15751
		public BagItemSelect bagItemSelect;

		// Token: 0x04003D88 RID: 15752
		public Text PlayerName;

		// Token: 0x04003D89 RID: 15753
		public Text PlayerTitle;

		// Token: 0x04003D8A RID: 15754
		public JiaoBag NpcBag;

		// Token: 0x04003D8B RID: 15755
		public PlayerSetRandomFace NpcFace;

		// Token: 0x04003D8C RID: 15756
		public Text NpcName;

		// Token: 0x04003D8D RID: 15757
		public Text NpcTitle;

		// Token: 0x04003D8E RID: 15758
		public int PlayerGetMoney;

		// Token: 0x04003D8F RID: 15759
		public Text PlayerGetMoneyText;

		// Token: 0x04003D90 RID: 15760
		public GameObject NpcSayPanel;

		// Token: 0x04003D91 RID: 15761
		public Text NpcSayText;

		// Token: 0x04003D92 RID: 15762
		public UnityAction CloseAction;
	}
}
