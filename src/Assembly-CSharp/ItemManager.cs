using System;
using UnityEngine;

// Token: 0x02000160 RID: 352
public class ItemManager : MonoBehaviour
{
	// Token: 0x06000C60 RID: 3168 RVA: 0x000972E4 File Offset: 0x000954E4
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

	// Token: 0x04000980 RID: 2432
	public ItemCollector[] Items;

	// Token: 0x04000981 RID: 2433
	public GameObject[] PrefabList;

	// Token: 0x04000982 RID: 2434
	public GameObject[] PrefabListDrop;

	// Token: 0x04000983 RID: 2435
	public Texture2D[] IconTextures;
}
