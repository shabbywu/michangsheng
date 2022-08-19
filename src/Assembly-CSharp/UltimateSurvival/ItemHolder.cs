using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005D8 RID: 1496
	[Serializable]
	public class ItemHolder
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x0015A11B File Offset: 0x0015831B
		public bool HasItem
		{
			get
			{
				return this.CurrentItem != null;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x0015A126 File Offset: 0x00158326
		// (set) Token: 0x06003038 RID: 12344 RVA: 0x0015A12E File Offset: 0x0015832E
		public SavableItem CurrentItem { get; private set; }

		// Token: 0x06003039 RID: 12345 RVA: 0x00014667 File Offset: 0x00012867
		public static implicit operator bool(ItemHolder holder)
		{
			return holder != null;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0015A138 File Offset: 0x00158338
		public bool TryAddItem(ItemData itemData, int amount, out int added, List<ItemProperty.Value> customPropertyValues = null, ulong _uuid = 0UL, int _index = 0)
		{
			added = 0;
			if (this.HasItem && itemData.Id != this.CurrentItem.Id)
			{
				return false;
			}
			if (!this.HasItem)
			{
				this.CurrentItem = new SavableItem(itemData, 1, customPropertyValues);
				this.CurrentItem.CurrentInStack = 0;
				this.CurrentItem.PropertyChanged.AddListener(new Action<ItemProperty.Value>(this.On_PropertyChanged));
				this.CurrentItem.StackChanged.AddListener(new Action(this.On_StackChanged));
				this.uuid = _uuid;
				this.index = _index;
				this.itemID = itemData.Id;
			}
			int currentInStack = this.CurrentItem.CurrentInStack;
			int num = amount + currentInStack - itemData.StackSize;
			int num2 = currentInStack;
			if (num <= 0)
			{
				num2 += amount;
			}
			else
			{
				num2 = itemData.StackSize;
			}
			this.CurrentItem.CurrentInStack = num2;
			added = num2 - currentInStack;
			this.Updated.Send(this);
			return added > 0;
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0015A228 File Offset: 0x00158428
		public void SetItem(SavableItem item)
		{
			if (this.CurrentItem)
			{
				this.CurrentItem.PropertyChanged.RemoveListener(new Action<ItemProperty.Value>(this.On_PropertyChanged));
				this.CurrentItem.StackChanged.RemoveListener(new Action(this.On_StackChanged));
			}
			this.CurrentItem = item;
			if (this.CurrentItem)
			{
				this.CurrentItem.PropertyChanged.AddListener(new Action<ItemProperty.Value>(this.On_PropertyChanged));
				this.CurrentItem.StackChanged.AddListener(new Action(this.On_StackChanged));
			}
			this.Updated.Send(this);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x0015A2D4 File Offset: 0x001584D4
		public void RemoveFromStack(int amount, out int removed)
		{
			removed = 0;
			if (!this.HasItem)
			{
				return;
			}
			if (amount >= this.CurrentItem.CurrentInStack)
			{
				removed = this.CurrentItem.CurrentInStack;
				this.SetItem(null);
				return;
			}
			int currentInStack = this.CurrentItem.CurrentInStack;
			this.CurrentItem.CurrentInStack = Mathf.Max(this.CurrentItem.CurrentInStack - amount, 0);
			removed = currentInStack - this.CurrentItem.CurrentInStack;
			this.Updated.Send(this);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0015A358 File Offset: 0x00158558
		public void RemoveFromStack(int amount)
		{
			if (!this.HasItem)
			{
				return;
			}
			int currentInStack = this.CurrentItem.CurrentInStack;
			this.CurrentItem.CurrentInStack = Mathf.Max(this.CurrentItem.CurrentInStack - amount, 0);
			this.Updated.Send(this);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x0015A3A4 File Offset: 0x001585A4
		private void On_PropertyChanged(ItemProperty.Value propertyValue)
		{
			this.Updated.Send(this);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0015A3A4 File Offset: 0x001585A4
		private void On_StackChanged()
		{
			this.Updated.Send(this);
		}

		// Token: 0x04002A7E RID: 10878
		public Message<ItemHolder> Updated = new Message<ItemHolder>();

		// Token: 0x04002A80 RID: 10880
		public ulong uuid;

		// Token: 0x04002A81 RID: 10881
		public int index;

		// Token: 0x04002A82 RID: 10882
		public int itemID;
	}
}
