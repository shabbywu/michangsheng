using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020D RID: 525
public class StorageInventory : MonoBehaviour
{
	// Token: 0x060010A2 RID: 4258 RVA: 0x000A742C File Offset: 0x000A562C
	public void addItemToStorage(int id, int value)
	{
		Item itemByID = this.itemDatabase.getItemByID(id);
		itemByID.itemValue = value;
		this.storageItems.Add(itemByID);
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x000A745C File Offset: 0x000A565C
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

	// Token: 0x060010A4 RID: 4260 RVA: 0x00010693 File Offset: 0x0000E893
	public void setImportantVariables()
	{
		if (this.itemDatabase == null)
		{
			this.itemDatabase = (ItemDataBaseList)Resources.Load("ItemDatabase");
		}
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x000A75C0 File Offset: 0x000A57C0
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

	// Token: 0x060010A6 RID: 4262 RVA: 0x000106B8 File Offset: 0x0000E8B8
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

	// Token: 0x060010A7 RID: 4263 RVA: 0x000A76E4 File Offset: 0x000A58E4
	private void setListofStorage()
	{
		Inventory component = this.inventory.GetComponent<Inventory>();
		this.storageItems = component.getItemList();
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x000A770C File Offset: 0x000A590C
	private void addItemsToInventory()
	{
		Inventory component = this.inventory.GetComponent<Inventory>();
		for (int i = 0; i < this.storageItems.Count; i++)
		{
			component.addItemToInventory(this.storageItems[i].itemID, this.storageItems[i].itemValue);
		}
		component.stackableSettings();
	}

	// Token: 0x04000D23 RID: 3363
	[SerializeField]
	public GameObject inventory;

	// Token: 0x04000D24 RID: 3364
	[SerializeField]
	public List<Item> storageItems = new List<Item>();

	// Token: 0x04000D25 RID: 3365
	[SerializeField]
	private ItemDataBaseList itemDatabase;

	// Token: 0x04000D26 RID: 3366
	[SerializeField]
	public int distanceToOpenStorage;

	// Token: 0x04000D27 RID: 3367
	public float timeToOpenStorage;

	// Token: 0x04000D28 RID: 3368
	private InputManager inputManagerDatabase;

	// Token: 0x04000D29 RID: 3369
	private float startTimer;

	// Token: 0x04000D2A RID: 3370
	private float endTimer;

	// Token: 0x04000D2B RID: 3371
	private bool showTimer;

	// Token: 0x04000D2C RID: 3372
	public int itemAmount;

	// Token: 0x04000D2D RID: 3373
	private Tooltip tooltip;

	// Token: 0x04000D2E RID: 3374
	private Inventory inv;

	// Token: 0x04000D2F RID: 3375
	private GameObject player;

	// Token: 0x04000D30 RID: 3376
	private static Image timerImage;

	// Token: 0x04000D31 RID: 3377
	private static GameObject timer;

	// Token: 0x04000D32 RID: 3378
	private bool closeInv;

	// Token: 0x04000D33 RID: 3379
	private bool showStorage;
}
