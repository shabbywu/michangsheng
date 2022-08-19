using System;
using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200064A RID: 1610
	public class RecipeSlot : Selectable
	{
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600334C RID: 13132 RVA: 0x00168AC0 File Offset: 0x00166CC0
		// (remove) Token: 0x0600334D RID: 13133 RVA: 0x00168AF8 File Offset: 0x00166CF8
		public event RecipeSlot.BaseAction E_Deselect;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x0600334E RID: 13134 RVA: 0x00168B30 File Offset: 0x00166D30
		// (remove) Token: 0x0600334F RID: 13135 RVA: 0x00168B68 File Offset: 0x00166D68
		public event RecipeSlot.PointerAction PointerUp;

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x00168B9D File Offset: 0x00166D9D
		// (set) Token: 0x06003351 RID: 13137 RVA: 0x00168BA5 File Offset: 0x00166DA5
		public ItemData Result { get; private set; }

		// Token: 0x06003352 RID: 13138 RVA: 0x00168BB0 File Offset: 0x00166DB0
		public void ShowRecipeForItem(ItemData item)
		{
			ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
			this.Result = item;
			this.m_RecipeName.text = ((item.DisplayName == string.Empty) ? item.Name : item.DisplayName);
			this.m_Icon.sprite = item.Icon;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x00168C0C File Offset: 0x00166E0C
		public void ShowRecipeForItem(ItemData item, ItemDatabase database)
		{
			this.Result = item;
			this.m_RecipeName.text = ((item.DisplayName == string.Empty) ? item.Name : item.DisplayName);
			this.m_Icon.sprite = item.Icon;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x00168C5C File Offset: 0x00166E5C
		public override void OnDeselect(BaseEventData data)
		{
			if (MonoSingleton<GUIController>.Instance && MonoSingleton<GUIController>.Instance.MouseOverSelectionKeeper())
			{
				base.StartCoroutine(this.C_WaitAndSelect(1));
				return;
			}
			Event.fireOut("ShopOnDeselect", new object[]
			{
				this
			});
			if (this.E_Deselect != null)
			{
				this.E_Deselect(data, this);
			}
			base.OnDeselect(data);
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x00168CC0 File Offset: 0x00166EC0
		public override void OnPointerUp(PointerEventData data)
		{
			base.OnPointerUp(data);
			if (this.PointerUp != null)
			{
				this.PointerUp(data, this);
			}
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x00168CDE File Offset: 0x00166EDE
		protected IEnumerator C_WaitAndSelect(int waitFrameCount)
		{
			int num;
			for (int i = 0; i < waitFrameCount; i = num + 1)
			{
				yield return null;
				num = i;
			}
			this.Select();
			yield break;
		}

		// Token: 0x04002D89 RID: 11657
		[Header("Setup")]
		[SerializeField]
		private Text m_RecipeName;

		// Token: 0x04002D8A RID: 11658
		[SerializeField]
		private Image m_Icon;

		// Token: 0x020014E6 RID: 5350
		// (Invoke) Token: 0x06008245 RID: 33349
		public delegate void BaseAction(BaseEventData data, RecipeSlot slot);

		// Token: 0x020014E7 RID: 5351
		// (Invoke) Token: 0x06008249 RID: 33353
		public delegate void PointerAction(PointerEventData data, RecipeSlot slot);
	}
}
