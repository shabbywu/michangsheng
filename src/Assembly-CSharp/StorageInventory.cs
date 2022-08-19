using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013A RID: 314
public class StorageInventory : MonoBehaviour
{
	// Token: 0x06000E8C RID: 3724 RVA: 0x000570B4 File Offset: 0x000552B4
	public void addItemToStorage(int id, int value)
	{
		Item itemByID = this.itemDatabase.getItemByID(id);
		itemByID.itemValue = value;
		this.storageItems.Add(itemByID);
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x000570E4 File Offset: 0x000552E4
	private void Start()
	{
		if (this.inputManagerDatabase == null)
		{
			this.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
		}
		this.player = (GameObject)KBEngineApp.app.player().renderObj;
		this.inv = this.inventory.GetComponent<Inventory>();
		ItemDataBaseList itemDataBaseList = (ItemDataBaseList)Resources.Load("ItemDatabase");
		int i = 1;
		int num = Random.Range(1, this.itemAmount);
		while (i < num)
		{
			int index = Random.Range(1, itemDataBaseList.itemList.Count - 1);
			if (Random.Range(1, 100) <= itemDataBaseList.itemList[index].rarity)
			{
				int itemValue = Random.Range(1, itemDataBaseList.itemList[index].getCopy().maxStack);
				Item copy = itemDataBaseList.itemList[index].getCopy();
				copy.itemValue = itemValue;
				this.storageItems.Add(copy);
				i++;
			}
		}
		if (GameObject.FindGameObjectWithTag("Timer") != null)
		{
			StorageInventory.timerImage = GameObject.FindGameObjectWithTag("Timer").GetComponent<Image>();
			StorageInventory.timer = GameObject.FindGameObjectWithTag("Timer");
			StorageInventory.timer.SetActive(false);
		}
		if (GameObject.FindGameObjectWithTag("Tooltip") != null)
		{
			this.tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
		}
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x00057245 File Offset: 0x00055445
	public void setImportantVariables()
	{
		if (this.itemDatabase == null)
		{
			this.itemDatabase = (ItemDataBaseList)Resources.Load("ItemDatabase");
		}
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x0005726C File Offset: 0x0005546C
	private void Update()
	{
		float num = Vector3.Distance(base.gameObject.transform.position, this.player.transform.position);
		if (this.showTimer && StorageInventory.timerImage != null)
		{
			StorageInventory.timer.SetActive(true);
			float fillAmount = (Time.time - this.startTimer) / this.timeToOpenStorage;
			StorageInventory.timerImage.fillAmount = fillAmount;
		}
		if (num <= (float)this.distanceToOpenStorage && Input.GetKeyDown(this.inputManagerDatabase.StorageKeyCode))
		{
			this.showStorage = !this.showStorage;
			base.StartCoroutine(this.OpenInventoryWithTimer());
		}
		if (num > (float)this.distanceToOpenStorage && this.showStorage)
		{
			this.showStorage = false;
			if (this.inventory.activeSelf)
			{
				this.storageItems.Clear();
				this.setListofStorage();
				this.inventory.SetActive(false);
				this.inv.deleteAllItems();
			}
			this.tooltip.deactivateTooltip();
			StorageInventory.timerImage.fillAmount = 0f;
			StorageInventory.timer.SetActive(false);
			this.showTimer = false;
		}
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x0005738E File Offset: 0x0005558E
	private IEnumerator OpenInventoryWithTimer()
	{
		if (this.showStorage)
		{
			this.startTimer = Time.time;
			this.showTimer = true;
			yield return new WaitForSeconds(this.timeToOpenStorage);
			if (this.showStorage)
			{
				this.inv.ItemsInInventory.Clear();
				this.inventory.SetActive(true);
				this.addItemsToInventory();
				this.showTimer = false;
				if (StorageInventory.timer != null)
				{
					StorageInventory.timer.SetActive(false);
				}
			}
		}
		else
		{
			this.storageItems.Clear();
			this.setListofStorage();
			this.inventory.SetActive(false);
			this.inv.deleteAllItems();
			this.tooltip.deactivateTooltip();
		}
		yield break;
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x000573A0 File Offset: 0x000555A0
	private void setListofStorage()
	{
		Inventory component = this.inventory.GetComponent<Inventory>();
		this.storageItems = component.getItemList();
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x000573C8 File Offset: 0x000555C8
	private void addItemsToInventory()
	{
		Inventory component = this.inventory.GetComponent<Inventory>();
		for (int i = 0; i < this.storageItems.Count; i++)
		{
			component.addItemToInventory(this.storageItems[i].itemID, this.storageItems[i].itemValue);
		}
		component.stackableSettings();
	}

	// Token: 0x04000A8B RID: 2699
	[SerializeField]
	public GameObject inventory;

	// Token: 0x04000A8C RID: 2700
	[SerializeField]
	public List<Item> storageItems = new List<Item>();

	// Token: 0x04000A8D RID: 2701
	[SerializeField]
	private ItemDataBaseList itemDatabase;

	// Token: 0x04000A8E RID: 2702
	[SerializeField]
	public int distanceToOpenStorage;

	// Token: 0x04000A8F RID: 2703
	public float timeToOpenStorage;

	// Token: 0x04000A90 RID: 2704
	private InputManager inputManagerDatabase;

	// Token: 0x04000A91 RID: 2705
	private float startTimer;

	// Token: 0x04000A92 RID: 2706
	private float endTimer;

	// Token: 0x04000A93 RID: 2707
	private bool showTimer;

	// Token: 0x04000A94 RID: 2708
	public int itemAmount;

	// Token: 0x04000A95 RID: 2709
	private Tooltip tooltip;

	// Token: 0x04000A96 RID: 2710
	private Inventory inv;

	// Token: 0x04000A97 RID: 2711
	private GameObject player;

	// Token: 0x04000A98 RID: 2712
	private static Image timerImage;

	// Token: 0x04000A99 RID: 2713
	private static GameObject timer;

	// Token: 0x04000A9A RID: 2714
	private bool closeInv;

	// Token: 0x04000A9B RID: 2715
	private bool showStorage;
}
