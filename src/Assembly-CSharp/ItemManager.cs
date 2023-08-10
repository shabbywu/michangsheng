using UnityEngine;

public class ItemManager : MonoBehaviour
{
	public ItemCollector[] Items;

	public GameObject[] PrefabList;

	public GameObject[] PrefabListDrop;

	public Texture2D[] IconTextures;

	private void Awake()
	{
		Items = new ItemCollector[5];
		Items[0].Name = "Golden Axe";
		Items[0].Description = "Great Damage Slow attack speed";
		Items[0].ItemPrefab = PrefabList[0];
		Items[0].ItemPrefabDrop = PrefabListDrop[0];
		Items[0].Icon = IconTextures[0];
		Items[0].ItemType = ItemType.Weapon;
		Items[1].Name = "Board Sword";
		Items[1].Description = "Normal Damage Very fast attack speed";
		Items[1].ItemPrefab = PrefabList[1];
		Items[1].ItemPrefabDrop = PrefabListDrop[1];
		Items[1].Icon = IconTextures[1];
		Items[1].ItemType = ItemType.Weapon;
		Items[2].Name = "Shield";
		Items[2].Description = "Just a basic shield";
		Items[2].ItemPrefab = PrefabList[2];
		Items[2].ItemPrefabDrop = PrefabListDrop[2];
		Items[2].Icon = IconTextures[2];
		Items[2].ItemType = ItemType.Weapon;
		Items[3].Name = "Fire Sword";
		Items[3].Description = "Burning Sword";
		Items[3].ItemPrefab = PrefabList[3];
		Items[3].ItemPrefabDrop = PrefabListDrop[3];
		Items[3].Icon = IconTextures[3];
		Items[3].ItemType = ItemType.Weapon;
		Items[4].Name = "Red Potion";
		Items[4].Description = "Heal +20 HP";
		Items[4].ItemPrefab = PrefabList[4];
		Items[4].ItemPrefabDrop = PrefabListDrop[4];
		Items[4].Icon = IconTextures[4];
		Items[4].ItemType = ItemType.Edible;
	}
}
