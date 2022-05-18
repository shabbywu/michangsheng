using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000944 RID: 2372
	public class RecipeInspector : MonoBehaviour
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x0002BC46 File Offset: 0x00029E46
		// (set) Token: 0x06003CA3 RID: 15523 RVA: 0x0002BC4E File Offset: 0x00029E4E
		public RecipeSlot InspectedSlot { get; private set; }

		// Token: 0x06003CA4 RID: 15524 RVA: 0x001B16B4 File Offset: 0x001AF8B4
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

		// Token: 0x06003CA5 RID: 15525 RVA: 0x0002BC57 File Offset: 0x00029E57
		public void TryIncreaseAmount()
		{
			if (this.m_InspectedItem == null)
			{
				return;
			}
			this.m_CurrentDesiredAmount++;
			this.ShowRecipeInfo(this.m_InspectedItem);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x0002BC7C File Offset: 0x00029E7C
		public void TryDecreaseAmount()
		{
			if (this.m_InspectedItem == null)
			{
				return;
			}
			this.m_CurrentDesiredAmount = Mathf.Clamp(this.m_CurrentDesiredAmount - 1, 1, this.m_MaxCraftAmount);
			this.ShowRecipeInfo(this.m_InspectedItem);
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x001B1760 File Offset: 0x001AF960
		private void Awake()
		{
			if (!this.m_CraftingList)
			{
				Debug.LogError("Please assign the Crafting List in the inspector!", this);
			}
			this.m_CraftingList.RecipesGenerated.AddListener(new Action(this.On_RecipesGenerated));
			this.m_TotalTime.text = "";
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x001B17B4 File Offset: 0x001AF9B4
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

		// Token: 0x06003CA9 RID: 15529 RVA: 0x001B1860 File Offset: 0x001AFA60
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

		// Token: 0x06003CAA RID: 15530 RVA: 0x001B18EC File Offset: 0x001AFAEC
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

		// Token: 0x06003CAB RID: 15531 RVA: 0x0002BCAD File Offset: 0x00029EAD
		private void On_Slot_Deselect(BaseEventData data, RecipeSlot deselectedSlot)
		{
			base.StartCoroutine(this.C_CheckNextSelection());
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x0002BCBC File Offset: 0x00029EBC
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

		// Token: 0x040036E9 RID: 14057
		[Header("Setup")]
		[SerializeField]
		private ItemContainer m_Inventory;

		// Token: 0x040036EA RID: 14058
		[SerializeField]
		private CraftingList m_CraftingList;

		// Token: 0x040036EB RID: 14059
		[SerializeField]
		private Window m_Window;

		// Token: 0x040036EC RID: 14060
		[SerializeField]
		private int m_MaxCraftAmount = 999;

		// Token: 0x040036ED RID: 14061
		[Header("GUI Elements")]
		[SerializeField]
		private Text m_ItemName;

		// Token: 0x040036EE RID: 14062
		[SerializeField]
		private Text m_Description;

		// Token: 0x040036EF RID: 14063
		[SerializeField]
		private Image m_Icon;

		// Token: 0x040036F0 RID: 14064
		[SerializeField]
		private RequiredItemRow[] m_RequiredItemsTable;

		// Token: 0x040036F1 RID: 14065
		[SerializeField]
		private Text m_TotalTime;

		// Token: 0x040036F2 RID: 14066
		[SerializeField]
		private Text m_DesiredAmount;

		// Token: 0x040036F3 RID: 14067
		private ItemData m_InspectedItem;

		// Token: 0x040036F4 RID: 14068
		private int m_CurrentDesiredAmount;
	}
}
