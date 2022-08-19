using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using script.MenPaiTask.ZhangLao.UI.Base;
using script.MenPaiTask.ZhangLao.UI.UI;
using UnityEngine;

namespace script.MenPaiTask.ZhangLao.UI.Ctr
{
	// Token: 0x02000A10 RID: 2576
	public class CreateElderTaskCtr
	{
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600474F RID: 18255 RVA: 0x001E2E9A File Offset: 0x001E109A
		public CreateElderTaskUI UI
		{
			get
			{
				return ElderTaskUIMag.Inst.CreateElderTaskUI;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06004750 RID: 18256 RVA: 0x0006EC50 File Offset: 0x0006CE50
		private Avatar player
		{
			get
			{
				return Tools.instance.getPlayer();
			}
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x001E2EA8 File Offset: 0x001E10A8
		public void CreateItemList()
		{
			this.SlotList = new List<ElderTaskSlot>();
			int num = (int)this.UI.ItemPrefab.transform.localPosition.x;
			int num2 = (int)this.UI.ItemPrefab.transform.localPosition.y;
			for (int i = this.ItemIndex; i <= this.ItemCount; i++)
			{
				GameObject gameObject = this.UI.ItemPrefab.Inst(this.UI.ItemParent);
				gameObject.transform.localPosition = new Vector2((float)num, (float)num2);
				ElderTaskSlot component = gameObject.GetComponent<ElderTaskSlot>();
				component.SetNull();
				this.SlotList.Add(component);
				gameObject.SetActive(true);
				num += this.X;
			}
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x001E2F6C File Offset: 0x001E116C
		public void PutItem(ElderTaskSlot dragSlot)
		{
			ElderTaskSlot toSlot = ElderTaskUIMag.Inst.Bag.ToSlot;
			if (!dragSlot.Item.IsEqual(toSlot.Item))
			{
				USelectNum.Show(dragSlot.Item.GetName() + " x{num}", 1, this.MaxNum, delegate(int num)
				{
					BaseItem baseItem = dragSlot.Item.Clone();
					baseItem.Count = num;
					toSlot.SetSlotData(baseItem);
					ElderTaskUIMag.Inst.Bag.Hide();
					this.UpdateData();
					this.UI.UpdateUI();
				}, null);
				return;
			}
			if (toSlot.Item.Count >= this.MaxNum)
			{
				UIPopTip.Inst.Pop("每个格子最多只能放" + this.MaxNum + "个", PopTipIconType.叹号);
				return;
			}
			USelectNum.Show(dragSlot.Item.GetName() + " x{num}", 1, this.MaxNum - toSlot.Item.Count, delegate(int num)
			{
				toSlot.Item.Count += num;
				toSlot.UpdateUI();
				ElderTaskUIMag.Inst.Bag.Hide();
				this.UpdateData();
				this.UI.UpdateUI();
			}, null);
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x001E3078 File Offset: 0x001E1278
		public void BackItem(ElderTaskSlot dragSlot)
		{
			if (dragSlot.IsNull() || dragSlot.Item.Count <= 0)
			{
				UIPopTip.Inst.Pop("物品数目为空", PopTipIconType.叹号);
				return;
			}
			if (dragSlot.Item.Count == 1)
			{
				dragSlot.SetNull();
				this.UI.UpdateUI();
				return;
			}
			USelectNum.Show(dragSlot.Item.GetName() + " x{num}", 1, dragSlot.Item.Count, delegate(int num)
			{
				if (num <= dragSlot.Item.Count)
				{
					dragSlot.Item.Count -= num;
					if (dragSlot.Item.Count == 0)
					{
						dragSlot.SetNull();
					}
					else
					{
						dragSlot.UpdateUI();
					}
					this.UpdateData();
					this.UI.UpdateUI();
					return;
				}
				UIPopTip.Inst.Pop("超过当前物品数目", PopTipIconType.叹号);
			}, null);
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x001E3134 File Offset: 0x001E1334
		private void UpdateData()
		{
			this.NeedMoney = 0;
			this.NeedReputation = 0;
			foreach (ElderTaskSlot elderTaskSlot in this.SlotList)
			{
				if (!elderTaskSlot.IsNull())
				{
					this.NeedMoney += this.player.ElderTaskMag.GetNeedMoney(elderTaskSlot.Item);
					this.NeedReputation++;
				}
			}
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x001E31C8 File Offset: 0x001E13C8
		public void PublishTask()
		{
			if (this.player.ElderTaskMag.PlayerAllotTask(this.SlotList))
			{
				ElderTaskUIMag.Inst.ElderTaskUI.Ctr.CreateTaskList();
				this.ClearItemList();
				ElderTaskUIMag.Inst.OpenElderTaskUI();
				UIPopTip.Inst.Pop("发布任务成功", PopTipIconType.叹号);
			}
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x001E3224 File Offset: 0x001E1424
		private bool IsNull()
		{
			using (List<ElderTaskSlot>.Enumerator enumerator = this.SlotList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsNull())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x001E3280 File Offset: 0x001E1480
		public string CreateTaskDesc()
		{
			if (this.IsNull())
			{
				return "";
			}
			string text = string.Format("本门长老愿以{0}灵石求购", this.NeedMoney);
			foreach (ElderTaskSlot elderTaskSlot in this.SlotList)
			{
				if (!elderTaskSlot.IsNull())
				{
					text += string.Format("{0} x{1},", elderTaskSlot.Item.GetName(), elderTaskSlot.Item.Count);
				}
			}
			text = text.Remove(text.Length - 1, 1);
			text += "。";
			return text;
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x001E3344 File Offset: 0x001E1544
		public void ClearItemList()
		{
			foreach (ElderTaskSlot elderTaskSlot in this.SlotList)
			{
				elderTaskSlot.SetNull();
			}
			this.UpdateData();
			this.UI.UpdateUI();
		}

		// Token: 0x04004875 RID: 18549
		public List<ElderTaskSlot> SlotList;

		// Token: 0x04004876 RID: 18550
		public int ItemCount = 5;

		// Token: 0x04004877 RID: 18551
		public int ItemIndex = 1;

		// Token: 0x04004878 RID: 18552
		public int X = 176;

		// Token: 0x04004879 RID: 18553
		public int NeedMoney;

		// Token: 0x0400487A RID: 18554
		public int NeedReputation;

		// Token: 0x0400487B RID: 18555
		public int MaxNum = 10;
	}
}
