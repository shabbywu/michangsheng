using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
	public GameObject inventory;

	public GameObject characterSystem;

	public GameObject craftSystem;

	private Inventory craftSystemInventory;

	private CraftSystem cS;

	private Inventory mainInventory;

	private Inventory characterSystemInventory;

	private Tooltip toolTip;

	private InputManager inputManagerDatabase;

	public GameObject HPMANACanvas;

	private Text hpText;

	private Text manaText;

	private Image hpImage;

	private Image manaImage;

	private float maxHealth = 100f;

	private float maxMana = 100f;

	private float maxDamage;

	private float maxArmor;

	public float currentHealth = 60f;

	private float currentMana = 100f;

	private float currentDamage;

	private float currentArmor;

	private int normalSize = 3;

	public void OnEnable()
	{
		Inventory.ItemEquip += OnBackpack;
		Inventory.UnEquipItem += UnEquipBackpack;
		Inventory.ItemEquip += OnGearItem;
		Inventory.ItemConsumed += OnConsumeItem;
		Inventory.UnEquipItem += OnUnEquipItem;
		Inventory.ItemEquip += EquipWeapon;
		Inventory.UnEquipItem += UnEquipWeapon;
	}

	public void OnDisable()
	{
		Inventory.ItemEquip -= OnBackpack;
		Inventory.UnEquipItem -= UnEquipBackpack;
		Inventory.ItemEquip -= OnGearItem;
		Inventory.ItemConsumed -= OnConsumeItem;
		Inventory.UnEquipItem -= OnUnEquipItem;
		Inventory.UnEquipItem -= UnEquipWeapon;
		Inventory.ItemEquip -= EquipWeapon;
	}

	private void EquipWeapon(Item item)
	{
		_ = item.itemType;
		_ = 1;
	}

	private void UnEquipWeapon(Item item)
	{
		_ = item.itemType;
		_ = 1;
	}

	private void OnBackpack(Item item)
	{
		if (item.itemType != ItemType.Backpack)
		{
			return;
		}
		for (int i = 0; i < item.itemAttributes.Count; i++)
		{
			if ((Object)(object)mainInventory == (Object)null)
			{
				mainInventory = inventory.GetComponent<Inventory>();
			}
			mainInventory.sortItems();
			if (item.itemAttributes[i].attributeName == "Slots")
			{
				changeInventorySize(item.itemAttributes[i].attributeValue);
			}
		}
	}

	private void UnEquipBackpack(Item item)
	{
		if (item.itemType == ItemType.Backpack)
		{
			changeInventorySize(normalSize);
		}
	}

	private void changeInventorySize(int size)
	{
		dropTheRestItems(size);
		if ((Object)(object)mainInventory == (Object)null)
		{
			mainInventory = inventory.GetComponent<Inventory>();
		}
		if (size == 3)
		{
			mainInventory.width = 3;
			mainInventory.height = 1;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
		}
		switch (size)
		{
		case 6:
			mainInventory.width = 3;
			mainInventory.height = 2;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
			break;
		case 12:
			mainInventory.width = 4;
			mainInventory.height = 3;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
			break;
		case 16:
			mainInventory.width = 4;
			mainInventory.height = 4;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
			break;
		case 24:
			mainInventory.width = 6;
			mainInventory.height = 4;
			mainInventory.updateSlotAmount();
			mainInventory.adjustInventorySize();
			break;
		}
	}

	private void dropTheRestItems(int size)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		if (size < mainInventory.ItemsInInventory.Count)
		{
			for (int i = size; i < mainInventory.ItemsInInventory.Count; i++)
			{
				GameObject obj = Object.Instantiate<GameObject>(mainInventory.ItemsInInventory[i].itemModel);
				obj.AddComponent<PickUpItem>();
				obj.GetComponent<PickUpItem>().item = mainInventory.ItemsInInventory[i];
				obj.transform.localPosition = ((GameObject)KBEngineApp.app.player().renderObj).transform.localPosition;
			}
		}
	}

	private void Start()
	{
		if ((Object)(object)inputManagerDatabase == (Object)null)
		{
			inputManagerDatabase = (InputManager)(object)Resources.Load("InputManager");
		}
		if ((Object)(object)craftSystem != (Object)null)
		{
			cS = craftSystem.GetComponent<CraftSystem>();
		}
		GameObject val = GameObject.FindGameObjectWithTag("Canvas");
		if ((Object)(object)val.transform.Find("Panel - Inventory(Clone)") != (Object)null)
		{
			inventory = ((Component)val.transform.Find("Panel - Inventory(Clone)")).gameObject;
		}
		if ((Object)(object)val.transform.Find("Panel - EquipmentSystem(Clone)") != (Object)null)
		{
			characterSystem = ((Component)val.transform.Find("Panel - EquipmentSystem(Clone)")).gameObject;
		}
		if ((Object)(object)GameObject.FindGameObjectWithTag("Tooltip") != (Object)null)
		{
			toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
		}
		if ((Object)(object)inventory != (Object)null)
		{
			mainInventory = inventory.GetComponent<Inventory>();
		}
		if ((Object)(object)characterSystem != (Object)null)
		{
			characterSystemInventory = characterSystem.GetComponent<Inventory>();
		}
		if ((Object)(object)craftSystem != (Object)null)
		{
			craftSystemInventory = craftSystem.GetComponent<Inventory>();
		}
	}

	public void OnConsumeItem(Item item)
	{
		for (int i = 0; i < item.itemAttributes.Count; i++)
		{
			if (item.itemAttributes[i].attributeName == "Health")
			{
				if (currentHealth + (float)item.itemAttributes[i].attributeValue > maxHealth)
				{
					currentHealth = maxHealth;
				}
				else
				{
					currentHealth += item.itemAttributes[i].attributeValue;
				}
			}
			if (item.itemAttributes[i].attributeName == "Mana")
			{
				if (currentMana + (float)item.itemAttributes[i].attributeValue > maxMana)
				{
					currentMana = maxMana;
				}
				else
				{
					currentMana += item.itemAttributes[i].attributeValue;
				}
			}
			if (item.itemAttributes[i].attributeName == "Armor")
			{
				if (currentArmor + (float)item.itemAttributes[i].attributeValue > maxArmor)
				{
					currentArmor = maxArmor;
				}
				else
				{
					currentArmor += item.itemAttributes[i].attributeValue;
				}
			}
			if (item.itemAttributes[i].attributeName == "Damage")
			{
				if (currentDamage + (float)item.itemAttributes[i].attributeValue > maxDamage)
				{
					currentDamage = maxDamage;
				}
				else
				{
					currentDamage += item.itemAttributes[i].attributeValue;
				}
			}
		}
	}

	public void OnGearItem(Item item)
	{
		for (int i = 0; i < item.itemAttributes.Count; i++)
		{
			if (item.itemAttributes[i].attributeName == "Health")
			{
				maxHealth += item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Mana")
			{
				maxMana += item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Armor")
			{
				maxArmor += item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Damage")
			{
				maxDamage += item.itemAttributes[i].attributeValue;
			}
		}
	}

	public void OnUnEquipItem(Item item)
	{
		for (int i = 0; i < item.itemAttributes.Count; i++)
		{
			if (item.itemAttributes[i].attributeName == "Health")
			{
				maxHealth -= item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Mana")
			{
				maxMana -= item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Armor")
			{
				maxArmor -= item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Damage")
			{
				maxDamage -= item.itemAttributes[i].attributeValue;
			}
		}
	}

	private void Update()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyDown(inputManagerDatabase.CharacterSystemKeyCode))
		{
			if (!characterSystem.activeSelf)
			{
				characterSystemInventory.openInventory();
			}
			else
			{
				if ((Object)(object)toolTip != (Object)null)
				{
					toolTip.deactivateTooltip();
				}
				characterSystemInventory.closeInventory();
			}
		}
		if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
		{
			if (!inventory.activeSelf)
			{
				mainInventory.openInventory();
			}
			else
			{
				if ((Object)(object)toolTip != (Object)null)
				{
					toolTip.deactivateTooltip();
				}
				mainInventory.closeInventory();
			}
		}
		if (!Input.GetKeyDown(inputManagerDatabase.CraftSystemKeyCode))
		{
			return;
		}
		if (!craftSystem.activeSelf)
		{
			craftSystemInventory.openInventory();
			return;
		}
		if ((Object)(object)cS != (Object)null)
		{
			cS.backToInventory();
		}
		if ((Object)(object)toolTip != (Object)null)
		{
			toolTip.deactivateTooltip();
		}
		craftSystemInventory.closeInventory();
	}
}
