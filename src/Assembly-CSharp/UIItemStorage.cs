using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000049 RID: 73
[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000461 RID: 1121 RVA: 0x00007DF9 File Offset: 0x00005FF9
	public List<InvGameItem> items
	{
		get
		{
			while (this.mItems.Count < this.maxItemCount)
			{
				this.mItems.Add(null);
			}
			return this.mItems;
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x00007E22 File Offset: 0x00006022
	public InvGameItem GetItem(int slot)
	{
		if (slot >= this.items.Count)
		{
			return null;
		}
		return this.mItems[slot];
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00007E40 File Offset: 0x00006040
	public InvGameItem Replace(int slot, InvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			InvGameItem result = this.items[slot];
			this.mItems[slot] = item;
			return result;
		}
		return item;
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0006E4A4 File Offset: 0x0006C6A4
	private void Start()
	{
		if (this.template != null)
		{
			int num = 0;
			Bounds bounds = default(Bounds);
			for (int i = 0; i < this.maxRows; i++)
			{
				for (int j = 0; j < this.maxColumns; j++)
				{
					GameObject gameObject = NGUITools.AddChild(base.gameObject, this.template);
					gameObject.transform.localPosition = new Vector3((float)this.padding + ((float)j + 0.5f) * (float)this.spacing, (float)(-(float)this.padding) - ((float)i + 0.5f) * (float)this.spacing, 0f);
					UIStorageSlot component = gameObject.GetComponent<UIStorageSlot>();
					if (component != null)
					{
						component.storage = this;
						component.slot = num;
					}
					bounds.Encapsulate(new Vector3((float)this.padding * 2f + (float)((j + 1) * this.spacing), (float)(-(float)this.padding) * 2f - (float)((i + 1) * this.spacing), 0f));
					if (++num >= this.maxItemCount)
					{
						if (this.background != null)
						{
							this.background.transform.localScale = bounds.size;
						}
						return;
					}
				}
			}
			if (this.background != null)
			{
				this.background.transform.localScale = bounds.size;
			}
		}
	}

	// Token: 0x04000289 RID: 649
	public int maxItemCount = 8;

	// Token: 0x0400028A RID: 650
	public int maxRows = 4;

	// Token: 0x0400028B RID: 651
	public int maxColumns = 4;

	// Token: 0x0400028C RID: 652
	public GameObject template;

	// Token: 0x0400028D RID: 653
	public UIWidget background;

	// Token: 0x0400028E RID: 654
	public int spacing = 128;

	// Token: 0x0400028F RID: 655
	public int padding = 10;

	// Token: 0x04000290 RID: 656
	private List<InvGameItem> mItems = new List<InvGameItem>();
}
