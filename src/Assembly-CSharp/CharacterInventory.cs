using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class CharacterInventory : MonoBehaviour
{
	// Token: 0x06000B07 RID: 2823 RVA: 0x00042E64 File Offset: 0x00041064
	private void Start()
	{
		this.character = base.gameObject.GetComponent<CharacterStatus>();
		this.characterAttack = base.gameObject.GetComponent<CharacterAttack>();
		this.characterSystem = base.gameObject.GetComponent<CharacterSystem>();
		this.itemManager = (ItemManager)Object.FindObjectOfType(typeof(ItemManager));
		this.ItemsEquiped = new ItemSlot[this.ItemEmbedSlot.Length];
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00042ED4 File Offset: 0x000410D4
	private void removeAllChild(GameObject parent)
	{
		foreach (object obj in parent.transform)
		{
			Transform transform = (Transform)obj;
			if (transform != null)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00042F3C File Offset: 0x0004113C
	public void AddItem(int index, int num)
	{
		foreach (ItemSlot itemSlot in this.ItemSlots)
		{
			if (itemSlot != null && itemSlot.Index == index)
			{
				itemSlot.Num += num;
				return;
			}
		}
		ItemSlot itemSlot2 = new ItemSlot();
		itemSlot2.Index = index;
		itemSlot2.Num = num;
		this.ItemSlots.Add(itemSlot2);
		this.EquipItem(itemSlot2);
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00042FCC File Offset: 0x000411CC
	public void RemoveItem(ItemSlot item, int num)
	{
		if (item != null)
		{
			if (item.Num >= num)
			{
				item.Num -= num;
			}
			if (item.Num <= 0)
			{
				this.ItemSlots.Remove(item);
			}
		}
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00043000 File Offset: 0x00041200
	public void EquipItem(ItemSlot indexEquip)
	{
		if (this.itemManager != null && indexEquip.Index >= this.itemManager.Items.Length)
		{
			return;
		}
		ItemCollector itemCollector = this.itemManager.Items[indexEquip.Index];
		if (itemCollector.ItemPrefab != null)
		{
			ItemInventory component = itemCollector.ItemPrefab.GetComponent<ItemInventory>();
			if (component != null)
			{
				int itemEmbedSlotIndex = component.ItemEmbedSlotIndex;
				GameObject gameObject = Object.Instantiate<GameObject>(itemCollector.ItemPrefab, this.ItemEmbedSlot[itemEmbedSlotIndex].transform.position, this.ItemEmbedSlot[itemEmbedSlotIndex].transform.rotation);
				this.removeAllChild(this.ItemEmbedSlot[itemEmbedSlotIndex]);
				gameObject.transform.parent = this.ItemEmbedSlot[itemEmbedSlotIndex].transform;
				this.ItemsEquiped[itemEmbedSlotIndex] = indexEquip;
				Debug.Log("Equiped " + component);
			}
		}
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x000430E4 File Offset: 0x000412E4
	public void UnEquipItem(ItemSlot indexEquip)
	{
		if (indexEquip.Index < this.itemManager.Items.Length)
		{
			ItemInventory component = this.itemManager.Items[indexEquip.Index].ItemPrefab.GetComponent<ItemInventory>();
			int itemEmbedSlotIndex = component.GetComponent<ItemInventory>().ItemEmbedSlotIndex;
			this.removeAllChild(this.ItemEmbedSlot[itemEmbedSlotIndex]);
			this.ItemsEquiped[itemEmbedSlotIndex] = null;
			Debug.Log("UnEquipped " + component);
		}
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x0004315C File Offset: 0x0004135C
	public void UseItem(ItemSlot indexItem)
	{
		if (indexItem.Num > 0 && this.itemManager.Items[indexItem.Index].ItemPrefab)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.itemManager.Items[indexItem.Index].ItemPrefab, base.transform.position, base.transform.rotation);
			gameObject.transform.parent = base.gameObject.transform;
			this.RemoveItem(indexItem, 1);
			Debug.Log(gameObject + " Removed");
		}
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x000431FC File Offset: 0x000413FC
	public bool CheckEquiped(ItemSlot indexEquip)
	{
		int itemEmbedSlotIndex = this.itemManager.Items[indexEquip.Index].ItemPrefab.GetComponent<ItemInventory>().GetComponent<ItemInventory>().ItemEmbedSlotIndex;
		return this.ItemsEquiped[itemEmbedSlotIndex] != null && this.ItemsEquiped[itemEmbedSlotIndex].Index == indexEquip.Index;
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00043258 File Offset: 0x00041458
	private void Update()
	{
		if (this.ItemEmbedSlot.Length == 0)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.ItemsEquiped.Length; i++)
		{
			if (this.ItemsEquiped[i] != null)
			{
				num += this.itemManager.Items[this.ItemsEquiped[i].Index].ItemPrefab.GetComponent<ItemInventory>().Damage;
				num2 += this.itemManager.Items[this.ItemsEquiped[i].Index].ItemPrefab.GetComponent<ItemInventory>().Defend;
			}
		}
		if (this.character)
		{
			this.character.Damage = num;
			this.character.Defend = num2;
		}
		if (this.itemManager != null && this.ItemsEquiped[0] != null)
		{
			int index = this.ItemsEquiped[0].Index;
			ItemInventory component = this.itemManager.Items[index].ItemPrefab.GetComponent<ItemInventory>();
			if (this.characterSystem)
			{
				this.characterSystem.SpeedAttack = component.SpeedAttack;
			}
			if (this.characterAttack)
			{
				this.characterAttack.SoundHit = component.SoundHit;
			}
		}
	}

	// Token: 0x04000732 RID: 1842
	public GameObject[] ItemEmbedSlot;

	// Token: 0x04000733 RID: 1843
	public ItemSlot[] ItemsEquiped;

	// Token: 0x04000734 RID: 1844
	public List<ItemSlot> ItemSlots = new List<ItemSlot>();

	// Token: 0x04000735 RID: 1845
	public ItemManager itemManager;

	// Token: 0x04000736 RID: 1846
	private CharacterStatus character;

	// Token: 0x04000737 RID: 1847
	private CharacterAttack characterAttack;

	// Token: 0x04000738 RID: 1848
	private CharacterSystem characterSystem;
}
