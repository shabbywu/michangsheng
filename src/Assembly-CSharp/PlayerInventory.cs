using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FE RID: 510
public class PlayerInventory : MonoBehaviour
{
	// Token: 0x0600102D RID: 4141 RVA: 0x000A3EE8 File Offset: 0x000A20E8
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

	// Token: 0x0600102E RID: 4142 RVA: 0x000A3F6C File Offset: 0x000A216C
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

	// Token: 0x0600102F RID: 4143 RVA: 0x0001036E File Offset: 0x0000E56E
	private void EquipWeapon(Item item)
	{
		ItemType itemType = item.itemType;
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x0001036E File Offset: 0x0000E56E
	private void UnEquipWeapon(Item item)
	{
		ItemType itemType = item.itemType;
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x000A3FF0 File Offset: 0x000A21F0
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

	// Token: 0x06001032 RID: 4146 RVA: 0x00010379 File Offset: 0x0000E579
	private void UnEquipBackpack(Item item)
	{
		if (item.itemType == ItemType.Backpack)
		{
			this.changeInventorySize(this.normalSize);
		}
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x000A407C File Offset: 0x000A227C
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

	// Token: 0x06001034 RID: 4148 RVA: 0x000A41B0 File Offset: 0x000A23B0
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

	// Token: 0x06001035 RID: 4149 RVA: 0x000A4254 File Offset: 0x000A2454
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

	// Token: 0x06001036 RID: 4150 RVA: 0x000A4398 File Offset: 0x000A2598
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

	// Token: 0x06001037 RID: 4151 RVA: 0x000A4570 File Offset: 0x000A2770
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

	// Token: 0x06001038 RID: 4152 RVA: 0x000A468C File Offset: 0x000A288C
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

	// Token: 0x06001039 RID: 4153 RVA: 0x000A47A8 File Offset: 0x000A29A8
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

	// Token: 0x04000CAE RID: 3246
	public GameObject inventory;

	// Token: 0x04000CAF RID: 3247
	public GameObject characterSystem;

	// Token: 0x04000CB0 RID: 3248
	public GameObject craftSystem;

	// Token: 0x04000CB1 RID: 3249
	private Inventory craftSystemInventory;

	// Token: 0x04000CB2 RID: 3250
	private CraftSystem cS;

	// Token: 0x04000CB3 RID: 3251
	private Inventory mainInventory;

	// Token: 0x04000CB4 RID: 3252
	private Inventory characterSystemInventory;

	// Token: 0x04000CB5 RID: 3253
	private Tooltip toolTip;

	// Token: 0x04000CB6 RID: 3254
	private InputManager inputManagerDatabase;

	// Token: 0x04000CB7 RID: 3255
	public GameObject HPMANACanvas;

	// Token: 0x04000CB8 RID: 3256
	private Text hpText;

	// Token: 0x04000CB9 RID: 3257
	private Text manaText;

	// Token: 0x04000CBA RID: 3258
	private Image hpImage;

	// Token: 0x04000CBB RID: 3259
	private Image manaImage;

	// Token: 0x04000CBC RID: 3260
	private float maxHealth = 100f;

	// Token: 0x04000CBD RID: 3261
	private float maxMana = 100f;

	// Token: 0x04000CBE RID: 3262
	private float maxDamage;

	// Token: 0x04000CBF RID: 3263
	private float maxArmor;

	// Token: 0x04000CC0 RID: 3264
	public float currentHealth = 60f;

	// Token: 0x04000CC1 RID: 3265
	private float currentMana = 100f;

	// Token: 0x04000CC2 RID: 3266
	private float currentDamage;

	// Token: 0x04000CC3 RID: 3267
	private float currentArmor;

	// Token: 0x04000CC4 RID: 3268
	private int normalSize = 3;
}
