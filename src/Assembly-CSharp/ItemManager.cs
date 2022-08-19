using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class ItemManager : MonoBehaviour
{
	// Token: 0x06000B71 RID: 2929 RVA: 0x000457C8 File Offset: 0x000439C8
	private void Awake()
	{
		this.Items = new ItemCollector[5];
		this.Items[0].Name = "Golden Axe";
		this.Items[0].Description = "Great Damage Slow attack speed";
		this.Items[0].ItemPrefab = this.PrefabList[0];
		this.Items[0].ItemPrefabDrop = this.PrefabListDrop[0];
		this.Items[0].Icon = this.IconTextures[0];
		this.Items[0].ItemType = ItemType.Weapon;
		this.Items[1].Name = "Board Sword";
		this.Items[1].Description = "Normal Damage Very fast attack speed";
		this.Items[1].ItemPrefab = this.PrefabList[1];
		this.Items[1].ItemPrefabDrop = this.PrefabListDrop[1];
		this.Items[1].Icon = this.IconTextures[1];
		this.Items[1].ItemType = ItemType.Weapon;
		this.Items[2].Name = "Shield";
		this.Items[2].Description = "Just a basic shield";
		this.Items[2].ItemPrefab = this.PrefabList[2];
		this.Items[2].ItemPrefabDrop = this.PrefabListDrop[2];
		this.Items[2].Icon = this.IconTextures[2];
		this.Items[2].ItemType = ItemType.Weapon;
		this.Items[3].Name = "Fire Sword";
		this.Items[3].Description = "Burning Sword";
		this.Items[3].ItemPrefab = this.PrefabList[3];
		this.Items[3].ItemPrefabDrop = this.PrefabListDrop[3];
		this.Items[3].Icon = this.IconTextures[3];
		this.Items[3].ItemType = ItemType.Weapon;
		this.Items[4].Name = "Red Potion";
		this.Items[4].Description = "Heal +20 HP";
		this.Items[4].ItemPrefab = this.PrefabList[4];
		this.Items[4].ItemPrefabDrop = this.PrefabListDrop[4];
		this.Items[4].Icon = this.IconTextures[4];
		this.Items[4].ItemType = ItemType.Edible;
	}

	// Token: 0x040007A5 RID: 1957
	public ItemCollector[] Items;

	// Token: 0x040007A6 RID: 1958
	public GameObject[] PrefabList;

	// Token: 0x040007A7 RID: 1959
	public GameObject[] PrefabListDrop;

	// Token: 0x040007A8 RID: 1960
	public Texture2D[] IconTextures;
}
