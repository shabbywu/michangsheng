using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000649 RID: 1609
	public class RecipeInspector : MonoBehaviour
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x00168699 File Offset: 0x00166899
		// (set) Token: 0x06003341 RID: 13121 RVA: 0x001686A1 File Offset: 0x001668A1
		public RecipeSlot InspectedSlot { get; private set; }

		// Token: 0x06003342 RID: 13122 RVA: 0x001686AC File Offset: 0x001668AC
		public void Try_StartCrafting()
		{
			bool flag = this.m_CurrentDesiredAmount > 0;
			foreach (RequiredItem requiredItem in this.m_InspectedItem.Recipe.RequiredItems)
			{
				int num = MonoSingleton<InventoryController>.Instance.findItemCount(requiredItem.Name);
				if (requiredItem.Amount * this.m_CurrentDesiredAmount > num)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				CraftData arg = new CraftData
				{
					Result = this.m_InspectedItem,
					Amount = this.m_CurrentDesiredAmount
				};
				if (MonoSingleton<InventoryController>.Instance.CraftItem.Try(arg))
				{
					this.m_CurrentDesiredAmount = 1;
					this.ShowRecipeInfo(this.m_InspectedItem);
				}
			}
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x00168755 File Offset: 0x00166955
		public void TryIncreaseAmount()
		{
			if (this.m_InspectedItem == null)
			{
				return;
			}
			this.m_CurrentDesiredAmount++;
			this.ShowRecipeInfo(this.m_InspectedItem);
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x0016877A File Offset: 0x0016697A
		public void TryDecreaseAmount()
		{
			if (this.m_InspectedItem == null)
			{
				return;
			}
			this.m_CurrentDesiredAmount = Mathf.Clamp(this.m_CurrentDesiredAmount - 1, 1, this.m_MaxCraftAmount);
			this.ShowRecipeInfo(this.m_InspectedItem);
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x001687AC File Offset: 0x001669AC
		private void Awake()
		{
			if (!this.m_CraftingList)
			{
				Debug.LogError("Please assign the Crafting List in the inspector!", this);
			}
			this.m_CraftingList.RecipesGenerated.AddListener(new Action(this.On_RecipesGenerated));
			this.m_TotalTime.text = "";
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x00168800 File Offset: 0x00166A00
		private void On_RecipesGenerated()
		{
			foreach (ObjectHolder objectHolder in this.m_CraftingList.RecipesByCategory)
			{
				foreach (GameObject gameObject in objectHolder.ObjectList)
				{
					RecipeSlot component = gameObject.GetComponent<RecipeSlot>();
					if (component)
					{
						component.PointerUp += new RecipeSlot.PointerAction(this.On_Slot_PointerUp);
					}
				}
			}
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x001688AC File Offset: 0x00166AAC
		private void On_Slot_PointerUp(BaseEventData data, RecipeSlot slot)
		{
			if (!MonoSingleton<InventoryController>.Instance.IsClosed && EventSystem.current.currentSelectedGameObject == slot.gameObject)
			{
				if (this.m_Window)
				{
					this.m_Window.Open();
				}
				this.InspectedSlot = slot;
				this.m_InspectedItem = slot.Result;
				this.m_CurrentDesiredAmount = 1;
				this.ShowRecipeInfo(this.m_InspectedItem);
				slot.E_Deselect += this.On_Slot_Deselect;
				return;
			}
			this.On_Slot_Deselect(data, slot);
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x00168938 File Offset: 0x00166B38
		private void ShowRecipeInfo(ItemData itemData)
		{
			ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
			this.m_ItemName.text = ((itemData.DisplayName == string.Empty) ? itemData.Name : itemData.DisplayName);
			this.m_Description.text = itemData.Descriptions[0];
			this.m_Icon.sprite = itemData.Icon;
			for (int i = 0; i < this.m_RequiredItemsTable.Length; i++)
			{
				RequiredItemRow requiredItemRow = this.m_RequiredItemsTable[i];
				if (itemData.Recipe.RequiredItems.Length > i)
				{
					RequiredItem requiredItem = itemData.Recipe.RequiredItems[i];
					int total = requiredItem.Amount * Mathf.Clamp(this.m_CurrentDesiredAmount, 1, int.MaxValue);
					int have = MonoSingleton<InventoryController>.Instance.findItemCount(requiredItem.Name);
					requiredItemRow.Set(requiredItem.Amount, requiredItem.Name, total, have);
				}
				else
				{
					requiredItemRow.Set(0, "", 0, 0);
				}
			}
			this.m_TotalTime.text = string.Format("耗时: {0}s", (itemData.Recipe.Duration * this.m_CurrentDesiredAmount).ToString());
			this.m_DesiredAmount.text = ((this.m_CurrentDesiredAmount > 0) ? "x" : "") + this.m_CurrentDesiredAmount.ToString();
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x00168A8C File Offset: 0x00166C8C
		private void On_Slot_Deselect(BaseEventData data, RecipeSlot deselectedSlot)
		{
			base.StartCoroutine(this.C_CheckNextSelection());
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x00168A9B File Offset: 0x00166C9B
		private IEnumerator C_CheckNextSelection()
		{
			yield return null;
			RecipeSlot recipeSlot = null;
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			if (currentSelectedGameObject)
			{
				recipeSlot = currentSelectedGameObject.GetComponent<RecipeSlot>();
			}
			if (!currentSelectedGameObject || !recipeSlot)
			{
				if (this.m_Window)
				{
					this.m_Window.Close(false);
				}
				this.InspectedSlot = null;
			}
			yield break;
		}

		// Token: 0x04002D7A RID: 11642
		[Header("Setup")]
		[SerializeField]
		private ItemContainer m_Inventory;

		// Token: 0x04002D7B RID: 11643
		[SerializeField]
		private CraftingList m_CraftingList;

		// Token: 0x04002D7C RID: 11644
		[SerializeField]
		private Window m_Window;

		// Token: 0x04002D7D RID: 11645
		[SerializeField]
		private int m_MaxCraftAmount = 999;

		// Token: 0x04002D7E RID: 11646
		[Header("GUI Elements")]
		[SerializeField]
		private Text m_ItemName;

		// Token: 0x04002D7F RID: 11647
		[SerializeField]
		private Text m_Description;

		// Token: 0x04002D80 RID: 11648
		[SerializeField]
		private Image m_Icon;

		// Token: 0x04002D81 RID: 11649
		[SerializeField]
		private RequiredItemRow[] m_RequiredItemsTable;

		// Token: 0x04002D82 RID: 11650
		[SerializeField]
		private Text m_TotalTime;

		// Token: 0x04002D83 RID: 11651
		[SerializeField]
		private Text m_DesiredAmount;

		// Token: 0x04002D84 RID: 11652
		private ItemData m_InspectedItem;

		// Token: 0x04002D85 RID: 11653
		private int m_CurrentDesiredAmount;
	}
}
