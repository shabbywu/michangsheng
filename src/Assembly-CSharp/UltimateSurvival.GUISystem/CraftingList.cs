using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.GUISystem;

public class CraftingList : MonoBehaviour
{
	public Message RecipesGenerated = new Message();

	[SerializeField]
	protected CraftingMenu m_CraftingMenu;

	[SerializeField]
	protected RectTransform m_ListTransform;

	[SerializeField]
	protected RecipeSlot m_RecipeTemplate;

	protected List<ObjectHolder> m_RecipesByCategory = new List<ObjectHolder>();

	public List<ObjectHolder> RecipesByCategory => m_RecipesByCategory;

	private void Awake()
	{
		m_CraftingMenu.SelectedCategory.AddChangeListener(OnChanged_SelectedCategory);
		m_CraftingMenu.SelectedCategory.AddChangeListener(OnChanged_Category);
	}

	private void Start()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		GenerateRecipes();
		m_ListTransform.anchoredPosition = new Vector2(m_ListTransform.anchoredPosition.x, 0f);
	}

	private void OnChanged_SelectedCategory()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		m_ListTransform.anchoredPosition = new Vector2(m_ListTransform.anchoredPosition.x, 0f);
	}

	private void OnChanged_Category()
	{
		CraftingCategory craftingCategory = m_CraftingMenu.SelectedCategory.Get();
		foreach (ObjectHolder item in m_RecipesByCategory)
		{
			item.ActivateObjects(craftingCategory.HasCategory(item.Name));
		}
	}

	private void GenerateRecipes()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
		((Component)m_RecipeTemplate).gameObject.SetActive(true);
		ItemCategory[] categories = database.Categories;
		foreach (ItemCategory obj in categories)
		{
			List<GameObject> list = new List<GameObject>();
			ObjectHolder item = new ObjectHolder(obj.Name, list);
			m_RecipesByCategory.Add(item);
			ItemData[] items = obj.Items;
			foreach (ItemData itemData in items)
			{
				if (itemData.IsCraftable)
				{
					_ = itemData.Recipe;
					RecipeSlot component = Object.Instantiate<GameObject>(((Component)m_RecipeTemplate).gameObject, ((Transform)m_ListTransform).position, ((Transform)m_ListTransform).rotation, (Transform)(object)m_ListTransform).GetComponent<RecipeSlot>();
					((Object)component).name = $"Recipe Slot ({itemData.Name})";
					component.ShowRecipeForItem(itemData);
					list.Add(((Component)component).gameObject);
				}
			}
		}
		((Component)m_RecipeTemplate).gameObject.SetActive(false);
		RecipesGenerated.Send();
	}
}
