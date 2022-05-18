using System;
using GUIPackage;
using UnityEngine;

// Token: 0x0200029C RID: 668
public class selectItemType : MonoBehaviour
{
	// Token: 0x0600146C RID: 5228 RVA: 0x00012E62 File Offset: 0x00011062
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x000B9B00 File Offset: 0x000B7D00
	public int getInputID(string name)
	{
		int num = 0;
		foreach (string b in this.mList.items)
		{
			if (name == b)
			{
				break;
			}
			num++;
		}
		return num;
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x000B9B64 File Offset: 0x000B7D64
	private void OnChange()
	{
		this.inventory.inventoryItemType = this.getInputID(this.mList.value);
		if (!this.inventory.ISPlayer)
		{
			ExchangePlan component = GameObject.Find("UI Root (2D)/exchangePlan").GetComponent<ExchangePlan>();
			this.inventory.MonstarLoadInventory(component.MonstarID);
			return;
		}
		if (this.caiLiaoInventory != null)
		{
			this.caiLiaoInventory.Quaily = this.getInputID(this.mList.value);
			this.caiLiaoInventory.LoadCaiLiaoInventory();
			return;
		}
		this.inventory.LoadInventory();
	}

	// Token: 0x04000FDA RID: 4058
	private UIPopupList mList;

	// Token: 0x04000FDB RID: 4059
	public Inventory2 inventory;

	// Token: 0x04000FDC RID: 4060
	public CaiLiaoInventory caiLiaoInventory;
}
