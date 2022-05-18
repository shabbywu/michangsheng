using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class CharacterInventory : MonoBehaviour
{
	// Token: 0x06000BEA RID: 3050 RVA: 0x00094BE4 File Offset: 0x00092DE4
	private void Start()
	{
		this.character = base.gameObject.GetComponent<CharacterStatus>();
		this.characterAttack = base.gameObject.GetComponent<CharacterAttack>();
		this.characterSystem = base.gameObject.GetComponent<CharacterSystem>();
		this.itemManager = (ItemManager)Object.FindObjectOfType(typeof(ItemManager));
		this.ItemsEquiped = new ItemSlot[this.ItemEmbedSlot.Length];
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x00094C54 File Offset: 0x00092E54
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

	// Token: 0x06000BEC RID: 3052 RVA: 0x00094CBC File Offset: 0x00092EBC
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

	// Token: 0x06000BED RID: 3053 RVA: 0x0000DFFA File Offset: 0x0000C1FA
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

	// Token: 0x06000BEE RID: 3054 RVA: 0x00094D4C File Offset: 0x00092F4C
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

	// Token: 0x06000BEF RID: 3055 RVA: 0x00094E30 File Offset: 0x00093030
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

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00094EA8 File Offset: 0x000930A8
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

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00094F48 File Offset: 0x00093148
	public bool CheckEquiped(ItemSlot indexEquip)
	{
		int itemEmbedSlotIndex = this.itemManager.Items[indexEquip.Index].ItemPrefab.GetComponent<ItemInventory>().GetComponent<ItemInventory>().ItemEmbedSlotIndex;
		return this.ItemsEquiped[itemEmbedSlotIndex] != null && this.ItemsEquiped[itemEmbedSlotIndex].Index == indexEquip.Index;
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00094FA4 File Offset: 0x000931A4
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

	// Token: 0x040008DD RID: 2269
	public GameObject[] ItemEmbedSlot;

	// Token: 0x040008DE RID: 2270
	public ItemSlot[] ItemsEquiped;

	// Token: 0x040008DF RID: 2271
	public List<ItemSlot> ItemSlots = new List<ItemSlot>();

	// Token: 0x040008E0 RID: 2272
	public ItemManager itemManager;

	// Token: 0x040008E1 RID: 2273
	private CharacterStatus character;

	// Token: 0x040008E2 RID: 2274
	private CharacterAttack characterAttack;

	// Token: 0x040008E3 RID: 2275
	private CharacterSystem characterSystem;
}
