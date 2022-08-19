using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012D RID: 301
public class PlayerInventory : MonoBehaviour
{
	// Token: 0x06000E1F RID: 3615 RVA: 0x000537CC File Offset: 0x000519CC
	public void OnEnable()
	{
		Inventory.ItemEquip += this.OnBackpack;
		Inventory.UnEquipItem += this.UnEquipBackpack;
		Inventory.ItemEquip += this.OnGearItem;
		Inventory.ItemConsumed += this.OnConsumeItem;
		Inventory.UnEquipItem += this.OnUnEquipItem;
		Inventory.ItemEquip += this.EquipWeapon;
		Inventory.UnEquipItem += this.UnEquipWeapon;
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x00053850 File Offset: 0x00051A50
	public void OnDisable()
	{
		Inventory.ItemEquip -= this.OnBackpack;
		Inventory.UnEquipItem -= this.UnEquipBackpack;
		Inventory.ItemEquip -= this.OnGearItem;
		Inventory.ItemConsumed -= this.OnConsumeItem;
		Inventory.UnEquipItem -= this.OnUnEquipItem;
		Inventory.UnEquipItem -= this.UnEquipWeapon;
		Inventory.ItemEquip -= this.EquipWeapon;
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x000538D4 File Offset: 0x00051AD4
	private void EquipWeapon(Item item)
	{
		ItemType itemType = item.itemType;
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x000538D4 File Offset: 0x00051AD4
	private void UnEquipWeapon(Item item)
	{
		ItemType itemType = item.itemType;
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x000538E0 File Offset: 0x00051AE0
	private void OnBackpack(Item item)
	{
		if (item.itemType == ItemType.Backpack)
		{
			for (int i = 0; i < item.itemAttributes.Count; i++)
			{
				if (this.mainInventory == null)
				{
					this.mainInventory = this.inventory.GetComponent<Inventory>();
				}
				this.mainInventory.sortItems();
				if (item.itemAttributes[i].attributeName == "Slots")
				{
					this.changeInventorySize(item.itemAttributes[i].attributeValue);
				}
			}
		}
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x0005396B File Offset: 0x00051B6B
	private void UnEquipBackpack(Item item)
	{
		if (item.itemType == ItemType.Backpack)
		{
			this.changeInventorySize(this.normalSize);
		}
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x00053984 File Offset: 0x00051B84
	private void changeInventorySize(int size)
	{
		this.dropTheRestItems(size);
		if (this.mainInventory == null)
		{
			this.mainInventory = this.inventory.GetComponent<Inventory>();
		}
		if (size == 3)
		{
			this.mainInventory.width = 3;
			this.mainInventory.height = 1;
			this.mainInventory.updateSlotAmount();
			this.mainInventory.adjustInventorySize();
		}
		if (size == 6)
		{
			this.mainInventory.width = 3;
			this.mainInventory.height = 2;
			this.mainInventory.updateSlotAmount();
			this.mainInventory.adjustInventorySize();
			return;
		}
		if (size == 12)
		{
			this.mainInventory.width = 4;
			this.mainInventory.height = 3;
			this.mainInventory.updateSlotAmount();
			this.mainInventory.adjustInventorySize();
			return;
		}
		if (size == 16)
		{
			this.mainInventory.width = 4;
			this.mainInventory.height = 4;
			this.mainInventory.updateSlotAmount();
			this.mainInventory.adjustInventorySize();
			return;
		}
		if (size == 24)
		{
			this.mainInventory.width = 6;
			this.mainInventory.height = 4;
			this.mainInventory.updateSlotAmount();
			this.mainInventory.adjustInventorySize();
		}
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x00053AB8 File Offset: 0x00051CB8
	private void dropTheRestItems(int size)
	{
		if (size < this.mainInventory.ItemsInInventory.Count)
		{
			for (int i = size; i < this.mainInventory.ItemsInInventory.Count; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.mainInventory.ItemsInInventory[i].itemModel);
				gameObject.AddComponent<PickUpItem>();
				gameObject.GetComponent<PickUpItem>().item = this.mainInventory.ItemsInInventory[i];
				gameObject.transform.localPosition = ((GameObject)KBEngineApp.app.player().renderObj).transform.localPosition;
			}
		}
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x00053B5C File Offset: 0x00051D5C
	private void Start()
	{
		if (this.inputManagerDatabase == null)
		{
			this.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
		}
		if (this.craftSystem != null)
		{
			this.cS = this.craftSystem.GetComponent<CraftSystem>();
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
		if (gameObject.transform.Find("Panel - Inventory(Clone)") != null)
		{
			this.inventory = gameObject.transform.Find("Panel - Inventory(Clone)").gameObject;
		}
		if (gameObject.transform.Find("Panel - EquipmentSystem(Clone)") != null)
		{
			this.characterSystem = gameObject.transform.Find("Panel - EquipmentSystem(Clone)").gameObject;
		}
		if (GameObject.FindGameObjectWithTag("Tooltip") != null)
		{
			this.toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
		}
		if (this.inventory != null)
		{
			this.mainInventory = this.inventory.GetComponent<Inventory>();
		}
		if (this.characterSystem != null)
		{
			this.characterSystemInventory = this.characterSystem.GetComponent<Inventory>();
		}
		if (this.craftSystem != null)
		{
			this.craftSystemInventory = this.craftSystem.GetComponent<Inventory>();
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x00053CA0 File Offset: 0x00051EA0
	public void OnConsumeItem(Item item)
	{
		for (int i = 0; i < item.itemAttributes.Count; i++)
		{
			if (item.itemAttributes[i].attributeName == "Health")
			{
				if (this.currentHealth + (float)item.itemAttributes[i].attributeValue > this.maxHealth)
				{
					this.currentHealth = this.maxHealth;
				}
				else
				{
					this.currentHealth += (float)item.itemAttributes[i].attributeValue;
				}
			}
			if (item.itemAttributes[i].attributeName == "Mana")
			{
				if (this.currentMana + (float)item.itemAttributes[i].attributeValue > this.maxMana)
				{
					this.currentMana = this.maxMana;
				}
				else
				{
					this.currentMana += (float)item.itemAttributes[i].attributeValue;
				}
			}
			if (item.itemAttributes[i].attributeName == "Armor")
			{
				if (this.currentArmor + (float)item.itemAttributes[i].attributeValue > this.maxArmor)
				{
					this.currentArmor = this.maxArmor;
				}
				else
				{
					this.currentArmor += (float)item.itemAttributes[i].attributeValue;
				}
			}
			if (item.itemAttributes[i].attributeName == "Damage")
			{
				if (this.currentDamage + (float)item.itemAttributes[i].attributeValue > this.maxDamage)
				{
					this.currentDamage = this.maxDamage;
				}
				else
				{
					this.currentDamage += (float)item.itemAttributes[i].attributeValue;
				}
			}
		}
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x00053E78 File Offset: 0x00052078
	public void OnGearItem(Item item)
	{
		for (int i = 0; i < item.itemAttributes.Count; i++)
		{
			if (item.itemAttributes[i].attributeName == "Health")
			{
				this.maxHealth += (float)item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Mana")
			{
				this.maxMana += (float)item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Armor")
			{
				this.maxArmor += (float)item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Damage")
			{
				this.maxDamage += (float)item.itemAttributes[i].attributeValue;
			}
		}
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x00053F94 File Offset: 0x00052194
	public void OnUnEquipItem(Item item)
	{
		for (int i = 0; i < item.itemAttributes.Count; i++)
		{
			if (item.itemAttributes[i].attributeName == "Health")
			{
				this.maxHealth -= (float)item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Mana")
			{
				this.maxMana -= (float)item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Armor")
			{
				this.maxArmor -= (float)item.itemAttributes[i].attributeValue;
			}
			if (item.itemAttributes[i].attributeName == "Damage")
			{
				this.maxDamage -= (float)item.itemAttributes[i].attributeValue;
			}
		}
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x000540B0 File Offset: 0x000522B0
	private void Update()
	{
		if (Input.GetKeyDown(this.inputManagerDatabase.CharacterSystemKeyCode))
		{
			if (!this.characterSystem.activeSelf)
			{
				this.characterSystemInventory.openInventory();
			}
			else
			{
				if (this.toolTip != null)
				{
					this.toolTip.deactivateTooltip();
				}
				this.characterSystemInventory.closeInventory();
			}
		}
		if (Input.GetKeyDown(this.inputManagerDatabase.InventoryKeyCode))
		{
			if (!this.inventory.activeSelf)
			{
				this.mainInventory.openInventory();
			}
			else
			{
				if (this.toolTip != null)
				{
					this.toolTip.deactivateTooltip();
				}
				this.mainInventory.closeInventory();
			}
		}
		if (Input.GetKeyDown(this.inputManagerDatabase.CraftSystemKeyCode))
		{
			if (!this.craftSystem.activeSelf)
			{
				this.craftSystemInventory.openInventory();
				return;
			}
			if (this.cS != null)
			{
				this.cS.backToInventory();
			}
			if (this.toolTip != null)
			{
				this.toolTip.deactivateTooltip();
			}
			this.craftSystemInventory.closeInventory();
		}
	}

	// Token: 0x04000A16 RID: 2582
	public GameObject inventory;

	// Token: 0x04000A17 RID: 2583
	public GameObject characterSystem;

	// Token: 0x04000A18 RID: 2584
	public GameObject craftSystem;

	// Token: 0x04000A19 RID: 2585
	private Inventory craftSystemInventory;

	// Token: 0x04000A1A RID: 2586
	private CraftSystem cS;

	// Token: 0x04000A1B RID: 2587
	private Inventory mainInventory;

	// Token: 0x04000A1C RID: 2588
	private Inventory characterSystemInventory;

	// Token: 0x04000A1D RID: 2589
	private Tooltip toolTip;

	// Token: 0x04000A1E RID: 2590
	private InputManager inputManagerDatabase;

	// Token: 0x04000A1F RID: 2591
	public GameObject HPMANACanvas;

	// Token: 0x04000A20 RID: 2592
	private Text hpText;

	// Token: 0x04000A21 RID: 2593
	private Text manaText;

	// Token: 0x04000A22 RID: 2594
	private Image hpImage;

	// Token: 0x04000A23 RID: 2595
	private Image manaImage;

	// Token: 0x04000A24 RID: 2596
	private float maxHealth = 100f;

	// Token: 0x04000A25 RID: 2597
	private float maxMana = 100f;

	// Token: 0x04000A26 RID: 2598
	private float maxDamage;

	// Token: 0x04000A27 RID: 2599
	private float maxArmor;

	// Token: 0x04000A28 RID: 2600
	public float currentHealth = 60f;

	// Token: 0x04000A29 RID: 2601
	private float currentMana = 100f;

	// Token: 0x04000A2A RID: 2602
	private float currentDamage;

	// Token: 0x04000A2B RID: 2603
	private float currentArmor;

	// Token: 0x04000A2C RID: 2604
	private int normalSize = 3;
}
