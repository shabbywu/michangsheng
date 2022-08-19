using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000644 RID: 1604
	public class CraftingList : MonoBehaviour
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x00167F69 File Offset: 0x00166169
		public List<ObjectHolder> RecipesByCategory
		{
			get
			{
				return this.m_RecipesByCategory;
			}
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x00167F71 File Offset: 0x00166171
		private void Awake()
		{
			this.m_CraftingMenu.SelectedCategory.AddChangeListener(new Action(this.OnChanged_SelectedCategory));
			this.m_CraftingMenu.SelectedCategory.AddChangeListener(new Action(this.OnChanged_Category));
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x00167FAB File Offset: 0x001661AB
		private void Start()
		{
			this.GenerateRecipes();
			this.m_ListTransform.anchoredPosition = new Vector2(this.m_ListTransform.anchoredPosition.x, 0f);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x00167FD8 File Offset: 0x001661D8
		private void OnChanged_SelectedCategory()
		{
			this.m_ListTransform.anchoredPosition = new Vector2(this.m_ListTransform.anchoredPosition.x, 0f);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x00168000 File Offset: 0x00166200
		private void OnChanged_Category()
		{
			CraftingCategory craftingCategory = this.m_CraftingMenu.SelectedCategory.Get();
			foreach (ObjectHolder objectHolder in this.m_RecipesByCategory)
			{
				objectHolder.ActivateObjects(craftingCategory.HasCategory(objectHolder.Name));
			}
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x00168070 File Offset: 0x00166270
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

		// Token: 0x04002D5C RID: 11612
		public Message RecipesGenerated = new Message();

		// Token: 0x04002D5D RID: 11613
		[SerializeField]
		protected CraftingMenu m_CraftingMenu;

		// Token: 0x04002D5E RID: 11614
		[SerializeField]
		protected RectTransform m_ListTransform;

		// Token: 0x04002D5F RID: 11615
		[SerializeField]
		protected RecipeSlot m_RecipeTemplate;

		// Token: 0x04002D60 RID: 11616
		protected List<ObjectHolder> m_RecipesByCategory = new List<ObjectHolder>();
	}
}
