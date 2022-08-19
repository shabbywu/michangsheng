using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000036 RID: 54
[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000419 RID: 1049 RVA: 0x00016DE2 File Offset: 0x00014FE2
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

	// Token: 0x0600041A RID: 1050 RVA: 0x00016E0B File Offset: 0x0001500B
	public InvGameItem GetItem(int slot)
	{
		if (slot >= this.items.Count)
		{
			return null;
		}
		return this.mItems[slot];
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00016E29 File Offset: 0x00015029
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

	// Token: 0x0600041C RID: 1052 RVA: 0x00016E50 File Offset: 0x00015050
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

	// Token: 0x04000243 RID: 579
	public int maxItemCount = 8;

	// Token: 0x04000244 RID: 580
	public int maxRows = 4;

	// Token: 0x04000245 RID: 581
	public int maxColumns = 4;

	// Token: 0x04000246 RID: 582
	public GameObject template;

	// Token: 0x04000247 RID: 583
	public UIWidget background;

	// Token: 0x04000248 RID: 584
	public int spacing = 128;

	// Token: 0x04000249 RID: 585
	public int padding = 10;

	// Token: 0x0400024A RID: 586
	private List<InvGameItem> mItems = new List<InvGameItem>();
}
