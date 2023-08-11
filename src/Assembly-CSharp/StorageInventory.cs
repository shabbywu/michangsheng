using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class StorageInventory : MonoBehaviour
{
	[SerializeField]
	public GameObject inventory;

	[SerializeField]
	public List<Item> storageItems = new List<Item>();

	[SerializeField]
	private ItemDataBaseList itemDatabase;

	[SerializeField]
	public int distanceToOpenStorage;

	public float timeToOpenStorage;

	private InputManager inputManagerDatabase;

	private float startTimer;

	private float endTimer;

	private bool showTimer;

	public int itemAmount;

	private Tooltip tooltip;

	private Inventory inv;

	private GameObject player;

	private static Image timerImage;

	private static GameObject timer;

	private bool closeInv;

	private bool showStorage;

	public void addItemToStorage(int id, int value)
	{
		Item itemByID = itemDatabase.getItemByID(id);
		itemByID.itemValue = value;
		storageItems.Add(itemByID);
	}

	private void Start()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		if ((Object)(object)inputManagerDatabase == (Object)null)
		{
			inputManagerDatabase = (InputManager)(object)Resources.Load("InputManager");
		}
		player = (GameObject)KBEngineApp.app.player().renderObj;
		inv = inventory.GetComponent<Inventory>();
		ItemDataBaseList itemDataBaseList = (ItemDataBaseList)(object)Resources.Load("ItemDatabase");
		int num = 1;
		int num2 = Random.Range(1, itemAmount);
		while (num < num2)
		{
			int index = Random.Range(1, itemDataBaseList.itemList.Count - 1);
			if (Random.Range(1, 100) <= itemDataBaseList.itemList[index].rarity)
			{
				int itemValue = Random.Range(1, itemDataBaseList.itemList[index].getCopy().maxStack);
				Item copy = itemDataBaseList.itemList[index].getCopy();
				copy.itemValue = itemValue;
				storageItems.Add(copy);
				num++;
			}
		}
		if ((Object)(object)GameObject.FindGameObjectWithTag("Timer") != (Object)null)
		{
			timerImage = GameObject.FindGameObjectWithTag("Timer").GetComponent<Image>();
			timer = GameObject.FindGameObjectWithTag("Timer");
			timer.SetActive(false);
		}
		if ((Object)(object)GameObject.FindGameObjectWithTag("Tooltip") != (Object)null)
		{
			tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
		}
	}

	public void setImportantVariables()
	{
		if ((Object)(object)itemDatabase == (Object)null)
		{
			itemDatabase = (ItemDataBaseList)(object)Resources.Load("ItemDatabase");
		}
	}

	private void Update()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector3.Distance(((Component)this).gameObject.transform.position, player.transform.position);
		if (showTimer && (Object)(object)timerImage != (Object)null)
		{
			timer.SetActive(true);
			float fillAmount = (Time.time - startTimer) / timeToOpenStorage;
			timerImage.fillAmount = fillAmount;
		}
		if (num <= (float)distanceToOpenStorage && Input.GetKeyDown(inputManagerDatabase.StorageKeyCode))
		{
			showStorage = !showStorage;
			((MonoBehaviour)this).StartCoroutine(OpenInventoryWithTimer());
		}
		if (num > (float)distanceToOpenStorage && showStorage)
		{
			showStorage = false;
			if (inventory.activeSelf)
			{
				storageItems.Clear();
				setListofStorage();
				inventory.SetActive(false);
				inv.deleteAllItems();
			}
			tooltip.deactivateTooltip();
			timerImage.fillAmount = 0f;
			timer.SetActive(false);
			showTimer = false;
		}
	}

	private IEnumerator OpenInventoryWithTimer()
	{
		if (showStorage)
		{
			startTimer = Time.time;
			showTimer = true;
			yield return (object)new WaitForSeconds(timeToOpenStorage);
			if (showStorage)
			{
				inv.ItemsInInventory.Clear();
				inventory.SetActive(true);
				addItemsToInventory();
				showTimer = false;
				if ((Object)(object)timer != (Object)null)
				{
					timer.SetActive(false);
				}
			}
		}
		else
		{
			storageItems.Clear();
			setListofStorage();
			inventory.SetActive(false);
			inv.deleteAllItems();
			tooltip.deactivateTooltip();
		}
	}

	private void setListofStorage()
	{
		Inventory component = inventory.GetComponent<Inventory>();
		storageItems = component.getItemList();
	}

	private void addItemsToInventory()
	{
		Inventory component = inventory.GetComponent<Inventory>();
		for (int i = 0; i < storageItems.Count; i++)
		{
			component.addItemToInventory(storageItems[i].itemID, storageItems[i].itemValue);
		}
		component.stackableSettings();
	}
}
