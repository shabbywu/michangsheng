using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000642 RID: 1602
	public class CraftingCategory : Selectable
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x00167E98 File Offset: 0x00166098
		public string DisplayName
		{
			get
			{
				return this.m_DisplayName;
			}
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x00167EA0 File Offset: 0x001660A0
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

		// Token: 0x06003323 RID: 13091 RVA: 0x00167EDA File Offset: 0x001660DA
		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			this.Selected.Send(this);
		}

		// Token: 0x04002D57 RID: 11607
		public Message<CraftingCategory> Selected = new Message<CraftingCategory>();

		// Token: 0x04002D58 RID: 11608
		[Header("Settings")]
		[SerializeField]
		private string m_DisplayName = "None";

		// Token: 0x04002D59 RID: 11609
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_CorrespondingCategories;
	}
}
