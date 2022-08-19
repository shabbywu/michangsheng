using System;
using System.Collections.Generic;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Submit
{
	// Token: 0x020009D3 RID: 2515
	public class SubmitUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x060045D8 RID: 17880 RVA: 0x001D9390 File Offset: 0x001D7590
		private void Awake()
		{
			this.ChengHao.text = PlayerEx.GetMenPaiChengHao();
			this.Name.text = Tools.instance.getPlayer().name;
			SubmitUIMag.Inst = this;
			SubmitUIMag.Inst.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			SubmitUIMag.Inst.transform.localScale = Vector3.one;
			SubmitUIMag.Inst.transform.localPosition = Vector3.zero;
			SubmitUIMag.Inst.transform.SetAsLastSibling();
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x001D9430 File Offset: 0x001D7630
		public void OpenLianQi(Func<BaseItem, bool> canPut, UnityAction submit, string title, int num)
		{
			this.SubmitAction = submit;
			this.CanPut = canPut;
			this.Title.SetText(title);
			this.Bag.ItemType = global::Bag.ItemType.法宝;
			this.Bag.EquipType = EquipType.装备;
			this.Bag.Open();
			this.InitSlotList(num);
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x001D9484 File Offset: 0x001D7684
		private void InitSlotList(int num)
		{
			Transform transform = base.transform.Find(string.Format("提交/物品列表{0}", num));
			transform.gameObject.SetActive(true);
			this.SlotList = new List<SubmitSlot>();
			for (int i = 0; i < transform.childCount; i++)
			{
				this.SlotList.Add(transform.GetChild(i).GetComponent<SubmitSlot>());
			}
			foreach (SubmitSlot submitSlot in this.SlotList)
			{
				submitSlot.InitUI();
				submitSlot.SetNull();
			}
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x001D9538 File Offset: 0x001D7738
		public void PutItem(SubmitSlot dragSlot, SubmitSlot toSlot = null)
		{
			if (!dragSlot.Item.CanSale)
			{
				UIPopTip.Inst.Pop("此物品无法交易", PopTipIconType.叹号);
			}
			if (toSlot == null)
			{
				toSlot = this.GetNullSlot();
				if (toSlot == null)
				{
					UIPopTip.Inst.Pop("已达提交上限", PopTipIconType.叹号);
					return;
				}
			}
			if (!toSlot.IsNull())
			{
				this.Bag.AddTempItem(toSlot.Item, 1);
			}
			toSlot.SetSlotData(dragSlot.Item.Clone());
			toSlot.Item.Count = 1;
			toSlot.UpdateUI();
			this.Bag.RemoveTempItem(dragSlot.Item.Uid, 1);
			this.Bag.UpdateItem(true);
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x001D95F0 File Offset: 0x001D77F0
		public void BackItem(SubmitSlot dragSlot, SubmitSlot toSlot = null)
		{
			if (toSlot == null)
			{
				SlotBase nullBagSlot = this.Bag.GetNullBagSlot(dragSlot.Item.Uid);
				if (nullBagSlot != null)
				{
					toSlot = (SubmitSlot)nullBagSlot;
				}
			}
			if (toSlot == null || toSlot.IsNull())
			{
				if (toSlot != null)
				{
					toSlot.SetSlotData(dragSlot.Item);
				}
				this.Bag.AddTempItem(dragSlot.Item, 1);
				dragSlot.SetNull();
			}
			else
			{
				BaseItem slotData = dragSlot.Item.Clone();
				dragSlot.SetSlotData(toSlot.Item);
				toSlot.SetSlotData(slotData);
				this.Bag.AddTempItem(dragSlot.Item, 1);
				this.Bag.RemoveTempItem(toSlot.Item.Uid, 1);
			}
			this.Bag.UpdateItem(true);
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x001D96C4 File Offset: 0x001D78C4
		public SubmitSlot GetNullSlot()
		{
			foreach (SubmitSlot submitSlot in this.SlotList)
			{
				if (submitSlot.IsNull())
				{
					return submitSlot;
				}
			}
			return null;
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x0005C928 File Offset: 0x0005AB28
		public void Close()
		{
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x001D9720 File Offset: 0x001D7920
		public void Submit()
		{
			if (!this.CheckPutAll())
			{
				UIPopTip.Inst.Pop("数量不足", PopTipIconType.叹号);
				return;
			}
			foreach (SubmitSlot submitSlot in this.SlotList)
			{
				Tools.instance.RemoveItem(submitSlot.Item.Uid, 1);
			}
			UnityAction submitAction = this.SubmitAction;
			if (submitAction != null)
			{
				submitAction.Invoke();
			}
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x001D97B8 File Offset: 0x001D79B8
		public bool CheckPutAll()
		{
			using (List<SubmitSlot>.Enumerator enumerator = this.SlotList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsNull())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x001D9814 File Offset: 0x001D7A14
		private void OnDestroy()
		{
			SubmitUIMag.Inst = null;
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x001D9827 File Offset: 0x001D7A27
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04004743 RID: 18243
		public static SubmitUIMag Inst;

		// Token: 0x04004744 RID: 18244
		public SubmitBag Bag;

		// Token: 0x04004745 RID: 18245
		public UnityAction SubmitAction;

		// Token: 0x04004746 RID: 18246
		public List<SubmitSlot> SlotList;

		// Token: 0x04004747 RID: 18247
		public Func<BaseItem, bool> CanPut;

		// Token: 0x04004748 RID: 18248
		public Text ChengHao;

		// Token: 0x04004749 RID: 18249
		public Text Name;

		// Token: 0x0400474A RID: 18250
		public Text Title;
	}
}
