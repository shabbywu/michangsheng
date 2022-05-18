using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200093C RID: 2364
	public class CraftingCategory : Selectable
	{
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x0002BA63 File Offset: 0x00029C63
		public string DisplayName
		{
			get
			{
				return this.m_DisplayName;
			}
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x001B0EE0 File Offset: 0x001AF0E0
		public bool HasCategory(string categoryName)
		{
			for (int i = 0; i < this.m_CorrespondingCategories.Count; i++)
			{
				if (this.m_CorrespondingCategories[i] == categoryName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x0002BA6B File Offset: 0x00029C6B
		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			this.Selected.Send(this);
		}

		// Token: 0x040036C0 RID: 14016
		public Message<CraftingCategory> Selected = new Message<CraftingCategory>();

		// Token: 0x040036C1 RID: 14017
		[Header("Settings")]
		[SerializeField]
		private string m_DisplayName = "None";

		// Token: 0x040036C2 RID: 14018
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_CorrespondingCategories;
	}
}
