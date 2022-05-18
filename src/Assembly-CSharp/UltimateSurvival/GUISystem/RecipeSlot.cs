using System;
using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000946 RID: 2374
	public class RecipeSlot : Selectable
	{
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06003CB4 RID: 15540 RVA: 0x001B1AD0 File Offset: 0x001AFCD0
		// (remove) Token: 0x06003CB5 RID: 15541 RVA: 0x001B1B08 File Offset: 0x001AFD08
		public event RecipeSlot.BaseAction E_Deselect;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06003CB6 RID: 15542 RVA: 0x001B1B40 File Offset: 0x001AFD40
		// (remove) Token: 0x06003CB7 RID: 15543 RVA: 0x001B1B78 File Offset: 0x001AFD78
		public event RecipeSlot.PointerAction PointerUp;

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x0002BCF5 File Offset: 0x00029EF5
		// (set) Token: 0x06003CB9 RID: 15545 RVA: 0x0002BCFD File Offset: 0x00029EFD
		public ItemData Result { get; private set; }

		// Token: 0x06003CBA RID: 15546 RVA: 0x001B1BB0 File Offset: 0x001AFDB0
		public void ShowRecipeForItem(ItemData item)
		{
			ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
			this.Result = item;
			this.m_RecipeName.text = ((item.DisplayName == string.Empty) ? item.Name : item.DisplayName);
			this.m_Icon.sprite = item.Icon;
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x001B1C0C File Offset: 0x001AFE0C
		public void ShowRecipeForItem(ItemData item, ItemDatabase database)
		{
			this.Result = item;
			this.m_RecipeName.text = ((item.DisplayName == string.Empty) ? item.Name : item.DisplayName);
			this.m_Icon.sprite = item.Icon;
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x001B1C5C File Offset: 0x001AFE5C
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

		// Token: 0x06003CBD RID: 15549 RVA: 0x0002BD06 File Offset: 0x00029F06
		public override void OnPointerUp(PointerEventData data)
		{
			base.OnPointerUp(data);
			if (this.PointerUp != null)
			{
				this.PointerUp(data, this);
			}
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x0002BD24 File Offset: 0x00029F24
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

		// Token: 0x040036FB RID: 14075
		[Header("Setup")]
		[SerializeField]
		private Text m_RecipeName;

		// Token: 0x040036FC RID: 14076
		[SerializeField]
		private Image m_Icon;

		// Token: 0x02000947 RID: 2375
		// (Invoke) Token: 0x06003CC1 RID: 15553
		public delegate void BaseAction(BaseEventData data, RecipeSlot slot);

		// Token: 0x02000948 RID: 2376
		// (Invoke) Token: 0x06003CC5 RID: 15557
		public delegate void PointerAction(PointerEventData data, RecipeSlot slot);
	}
}
