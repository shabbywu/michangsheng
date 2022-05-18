using System;
using System.Collections.Generic;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Submit
{
	// Token: 0x02000ABC RID: 2748
	public class SubmitUIMag : MonoBehaviour, IESCClose
	{
		// Token: 0x06004639 RID: 17977 RVA: 0x001DF114 File Offset: 0x001DD314
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

		// Token: 0x0600463A RID: 17978 RVA: 0x001DF1B4 File Offset: 0x001DD3B4
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

		// Token: 0x0600463B RID: 17979 RVA: 0x001DF208 File Offset: 0x001DD408
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

		// Token: 0x0600463C RID: 17980 RVA: 0x001DF2BC File Offset: 0x001DD4BC
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

		// Token: 0x0600463D RID: 17981 RVA: 0x001DF374 File Offset: 0x001DD574
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

		// Token: 0x0600463E RID: 17982 RVA: 0x001DF448 File Offset: 0x001DD648
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

		// Token: 0x0600463F RID: 17983 RVA: 0x000111B3 File Offset: 0x0000F3B3
		public void Close()
		{
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x001DF4A4 File Offset: 0x001DD6A4
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

		// Token: 0x06004641 RID: 17985 RVA: 0x001DF53C File Offset: 0x001DD73C
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

		// Token: 0x06004642 RID: 17986 RVA: 0x00032376 File Offset: 0x00030576
		private void OnDestroy()
		{
			SubmitUIMag.Inst = null;
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x00032389 File Offset: 0x00030589
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04003E59 RID: 15961
		public static SubmitUIMag Inst;

		// Token: 0x04003E5A RID: 15962
		public SubmitBag Bag;

		// Token: 0x04003E5B RID: 15963
		public UnityAction SubmitAction;

		// Token: 0x04003E5C RID: 15964
		public List<SubmitSlot> SlotList;

		// Token: 0x04003E5D RID: 15965
		public Func<BaseItem, bool> CanPut;

		// Token: 0x04003E5E RID: 15966
		public Text ChengHao;

		// Token: 0x04003E5F RID: 15967
		public Text Name;

		// Token: 0x04003E60 RID: 15968
		public Text Title;
	}
}
