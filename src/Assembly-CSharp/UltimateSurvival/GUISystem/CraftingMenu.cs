using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200093F RID: 2367
	public class CraftingMenu : MonoBehaviour
	{
		// Token: 0x06003C8D RID: 15501 RVA: 0x001B10DC File Offset: 0x001AF2DC
		private void Awake()
		{
			this.m_Categories = base.GetComponentsInChildren<CraftingCategory>();
			if (this.m_Categories.Length != 0)
			{
				CraftingCategory[] categories = this.m_Categories;
				for (int i = 0; i < categories.Length; i++)
				{
					categories[i].Selected.AddListener(new Action<CraftingCategory>(this.On_CategorySelected));
				}
				return;
			}
			Debug.LogWarning("No categories were found as children, this menu is useless!", this);
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x001B1138 File Offset: 0x001AF338
		private void Start()
		{
			if (this.m_SelectionHighlight)
			{
				this.m_SpawnedBackground = Object.Instantiate<GameObject>(this.m_SelectionHighlight, this.m_Categories[0].transform.parent);
				this.m_SpawnedBackground.transform.localScale = Vector3.one;
				this.m_SpawnedBackground.SetActive(false);
			}
			if (this.m_FirstSelected)
			{
				this.SelectedCategory.Set(this.m_FirstSelected);
				this.On_CategorySelected(this.m_FirstSelected);
			}
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x001B11C0 File Offset: 0x001AF3C0
		private void On_CategorySelected(CraftingCategory selectedCategory)
		{
			if (this.m_SelectionHighlight)
			{
				if (!this.m_SpawnedBackground.activeSelf)
				{
					this.m_SpawnedBackground.SetActive(true);
				}
				if (this.m_SpawnedBackground.transform.GetSiblingIndex() > 0)
				{
					this.m_SpawnedBackground.transform.SetAsFirstSibling();
				}
				this.m_SpawnedBackground.transform.position = selectedCategory.transform.position;
			}
			if (this.m_CategoryName)
			{
				this.m_CategoryName.text = selectedCategory.DisplayName;
			}
			this.SelectedCategory.Set(selectedCategory);
		}

		// Token: 0x040036CA RID: 14026
		public Value<CraftingCategory> SelectedCategory = new Value<CraftingCategory>(null);

		// Token: 0x040036CB RID: 14027
		[SerializeField]
		private CraftingCategory m_FirstSelected;

		// Token: 0x040036CC RID: 14028
		[SerializeField]
		private GameObject m_SelectionHighlight;

		// Token: 0x040036CD RID: 14029
		[SerializeField]
		private Text m_CategoryName;

		// Token: 0x040036CE RID: 14030
		private CraftingCategory[] m_Categories;

		// Token: 0x040036CF RID: 14031
		private GameObject m_SpawnedBackground;
	}
}
