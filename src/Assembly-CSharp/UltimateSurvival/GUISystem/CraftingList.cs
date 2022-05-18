using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200093E RID: 2366
	public class CraftingList : MonoBehaviour
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06003C86 RID: 15494 RVA: 0x0002BAC4 File Offset: 0x00029CC4
		public List<ObjectHolder> RecipesByCategory
		{
			get
			{
				return this.m_RecipesByCategory;
			}
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x0002BACC File Offset: 0x00029CCC
		private void Awake()
		{
			this.m_CraftingMenu.SelectedCategory.AddChangeListener(new Action(this.OnChanged_SelectedCategory));
			this.m_CraftingMenu.SelectedCategory.AddChangeListener(new Action(this.OnChanged_Category));
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x0002BB06 File Offset: 0x00029D06
		private void Start()
		{
			this.GenerateRecipes();
			this.m_ListTransform.anchoredPosition = new Vector2(this.m_ListTransform.anchoredPosition.x, 0f);
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x0002BB33 File Offset: 0x00029D33
		private void OnChanged_SelectedCategory()
		{
			this.m_ListTransform.anchoredPosition = new Vector2(this.m_ListTransform.anchoredPosition.x, 0f);
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x001B0F54 File Offset: 0x001AF154
		private void OnChanged_Category()
		{
			CraftingCategory craftingCategory = this.m_CraftingMenu.SelectedCategory.Get();
			foreach (ObjectHolder objectHolder in this.m_RecipesByCategory)
			{
				objectHolder.ActivateObjects(craftingCategory.HasCategory(objectHolder.Name));
			}
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x001B0FC4 File Offset: 0x001AF1C4
		private void GenerateRecipes()
		{
			ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
			this.m_RecipeTemplate.gameObject.SetActive(true);
			foreach (ItemCategory itemCategory in database.Categories)
			{
				List<GameObject> list = new List<GameObject>();
				ObjectHolder item = new ObjectHolder(itemCategory.Name, list);
				this.m_RecipesByCategory.Add(item);
				foreach (ItemData itemData in itemCategory.Items)
				{
					if (itemData.IsCraftable)
					{
						Recipe recipe = itemData.Recipe;
						RecipeSlot component = Object.Instantiate<GameObject>(this.m_RecipeTemplate.gameObject, this.m_ListTransform.position, this.m_ListTransform.rotation, this.m_ListTransform).GetComponent<RecipeSlot>();
						component.name = string.Format("Recipe Slot ({0})", itemData.Name);
						component.ShowRecipeForItem(itemData);
						list.Add(component.gameObject);
					}
				}
			}
			this.m_RecipeTemplate.gameObject.SetActive(false);
			this.RecipesGenerated.Send();
		}

		// Token: 0x040036C5 RID: 14021
		public Message RecipesGenerated = new Message();

		// Token: 0x040036C6 RID: 14022
		[SerializeField]
		protected CraftingMenu m_CraftingMenu;

		// Token: 0x040036C7 RID: 14023
		[SerializeField]
		protected RectTransform m_ListTransform;

		// Token: 0x040036C8 RID: 14024
		[SerializeField]
		protected RecipeSlot m_RecipeTemplate;

		// Token: 0x040036C9 RID: 14025
		protected List<ObjectHolder> m_RecipesByCategory = new List<ObjectHolder>();
	}
}
