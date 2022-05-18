using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200021E RID: 542
public class SplitItem : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x060010DE RID: 4318 RVA: 0x00010812 File Offset: 0x0000EA12
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

	// Token: 0x060010DF RID: 4319 RVA: 0x00010844 File Offset: 0x0000EA44
	private void Start()
	{
		SplitItem.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x000AA1C4 File Offset: 0x000A83C4
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

	// Token: 0x04000D8A RID: 3466
	private bool pressingButtonToSplit;

	// Token: 0x04000D8B RID: 3467
	public Inventory inv;

	// Token: 0x04000D8C RID: 3468
	private static InputManager inputManagerDatabase;
}
