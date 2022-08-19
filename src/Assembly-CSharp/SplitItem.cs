using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000149 RID: 329
public class SplitItem : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000EBE RID: 3774 RVA: 0x00059ECD File Offset: 0x000580CD
	private void Update()
	{
		if (Input.GetKeyDown(SplitItem.inputManagerDatabase.SplitItem))
		{
			this.pressingButtonToSplit = true;
		}
		if (Input.GetKeyUp(SplitItem.inputManagerDatabase.SplitItem))
		{
			this.pressingButtonToSplit = false;
		}
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x00059EFF File Offset: 0x000580FF
	private void Start()
	{
		SplitItem.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x00059F18 File Offset: 0x00058118
	public void OnPointerDown(PointerEventData data)
	{
		this.inv = base.transform.parent.parent.parent.GetComponent<Inventory>();
		if (base.transform.parent.parent.parent.GetComponent<Hotbar>() == null && data.button == null && this.pressingButtonToSplit && this.inv.stackable && this.inv.ItemsInInventory.Count < this.inv.height * this.inv.width)
		{
			ItemOnObject component = base.GetComponent<ItemOnObject>();
			if (component.item.itemValue > 1)
			{
				int num = component.item.itemValue;
				component.item.itemValue = component.item.itemValue / 2;
				num -= component.item.itemValue;
				this.inv.addItemToInventory(component.item.itemID, num);
				this.inv.stackableSettings();
				if (base.GetComponent<ConsumeItem>().duplication != null)
				{
					GameObject duplication = base.GetComponent<ConsumeItem>().duplication;
					duplication.GetComponent<ItemOnObject>().item.itemValue = component.item.itemValue;
					duplication.GetComponent<SplitItem>().inv.stackableSettings();
				}
				this.inv.updateItemList();
			}
		}
	}

	// Token: 0x04000AEF RID: 2799
	private bool pressingButtonToSplit;

	// Token: 0x04000AF0 RID: 2800
	public Inventory inv;

	// Token: 0x04000AF1 RID: 2801
	private static InputManager inputManagerDatabase;
}
