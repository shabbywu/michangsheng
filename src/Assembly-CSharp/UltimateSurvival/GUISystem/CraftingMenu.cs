using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000645 RID: 1605
	public class CraftingMenu : MonoBehaviour
	{
		// Token: 0x06003331 RID: 13105 RVA: 0x001681A4 File Offset: 0x001663A4
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

		// Token: 0x06003332 RID: 13106 RVA: 0x00168200 File Offset: 0x00166400
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

		// Token: 0x06003333 RID: 13107 RVA: 0x00168288 File Offset: 0x00166488
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

		// Token: 0x04002D61 RID: 11617
		public Value<CraftingCategory> SelectedCategory = new Value<CraftingCategory>(null);

		// Token: 0x04002D62 RID: 11618
		[SerializeField]
		private CraftingCategory m_FirstSelected;

		// Token: 0x04002D63 RID: 11619
		[SerializeField]
		private GameObject m_SelectionHighlight;

		// Token: 0x04002D64 RID: 11620
		[SerializeField]
		private Text m_CategoryName;

		// Token: 0x04002D65 RID: 11621
		private CraftingCategory[] m_Categories;

		// Token: 0x04002D66 RID: 11622
		private GameObject m_SpawnedBackground;
	}
}
