using System;
using GUIPackage;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public class selectItemType : MonoBehaviour
{
	// Token: 0x060011C5 RID: 4549 RVA: 0x0006B976 File Offset: 0x00069B76
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x0006B9A4 File Offset: 0x00069BA4
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

	// Token: 0x060011C7 RID: 4551 RVA: 0x0006BA08 File Offset: 0x00069C08
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

	// Token: 0x04000CB6 RID: 3254
	private UIPopupList mList;

	// Token: 0x04000CB7 RID: 3255
	public Inventory2 inventory;

	// Token: 0x04000CB8 RID: 3256
	public CaiLiaoInventory caiLiaoInventory;
}
