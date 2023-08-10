using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class RecipeInspector : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField]
	private ItemContainer m_Inventory;

	[SerializeField]
	private CraftingList m_CraftingList;

	[SerializeField]
	private Window m_Window;

	[SerializeField]
	private int m_MaxCraftAmount = 999;

	[Header("GUI Elements")]
	[SerializeField]
	private Text m_ItemName;

	[SerializeField]
	private Text m_Description;

	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private RequiredItemRow[] m_RequiredItemsTable;

	[SerializeField]
	private Text m_TotalTime;

	[SerializeField]
	private Text m_DesiredAmount;

	private ItemData m_InspectedItem;

	private int m_CurrentDesiredAmount;

	public RecipeSlot InspectedSlot { get; private set; }

	public void Try_StartCrafting()
	{
		bool flag = m_CurrentDesiredAmount > 0;
		RequiredItem[] requiredItems = m_InspectedItem.Recipe.RequiredItems;
		foreach (RequiredItem requiredItem in requiredItems)
		{
			int num = MonoSingleton<InventoryController>.Instance.findItemCount(requiredItem.Name);
			if (requiredItem.Amount * m_CurrentDesiredAmount > num)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			CraftData arg = new CraftData
			{
				Result = m_InspectedItem,
				Amount = m_CurrentDesiredAmount
			};
			if (MonoSingleton<InventoryController>.Instance.CraftItem.Try(arg))
			{
				m_CurrentDesiredAmount = 1;
				ShowRecipeInfo(m_InspectedItem);
			}
		}
	}

	public void TryIncreaseAmount()
	{
		if (m_InspectedItem != null)
		{
			m_CurrentDesiredAmount++;
			ShowRecipeInfo(m_InspectedItem);
		}
	}

	public void TryDecreaseAmount()
	{
		if (m_InspectedItem != null)
		{
			m_CurrentDesiredAmount = Mathf.Clamp(m_CurrentDesiredAmount - 1, 1, m_MaxCraftAmount);
			ShowRecipeInfo(m_InspectedItem);
		}
	}

	private void Awake()
	{
		if (!Object.op_Implicit((Object)(object)m_CraftingList))
		{
			Debug.LogError((object)"Please assign the Crafting List in the inspector!", (Object)(object)this);
		}
		m_CraftingList.RecipesGenerated.AddListener(On_RecipesGenerated);
		m_TotalTime.text = "";
	}

	private void On_RecipesGenerated()
	{
		foreach (ObjectHolder item in m_CraftingList.RecipesByCategory)
		{
			foreach (GameObject @object in item.ObjectList)
			{
				RecipeSlot component = @object.GetComponent<RecipeSlot>();
				if (Object.op_Implicit((Object)(object)component))
				{
					component.PointerUp += On_Slot_PointerUp;
				}
			}
		}
	}

	private void On_Slot_PointerUp(BaseEventData data, RecipeSlot slot)
	{
		if (!MonoSingleton<InventoryController>.Instance.IsClosed && (Object)(object)EventSystem.current.currentSelectedGameObject == (Object)(object)((Component)slot).gameObject)
		{
			if (Object.op_Implicit((Object)(object)m_Window))
			{
				m_Window.Open();
			}
			InspectedSlot = slot;
			m_InspectedItem = slot.Result;
			m_CurrentDesiredAmount = 1;
			ShowRecipeInfo(m_InspectedItem);
			slot.E_Deselect += On_Slot_Deselect;
		}
		else
		{
			On_Slot_Deselect(data, slot);
		}
	}

	private void ShowRecipeInfo(ItemData itemData)
	{
		_ = MonoSingleton<InventoryController>.Instance.Database;
		m_ItemName.text = ((itemData.DisplayName == string.Empty) ? itemData.Name : itemData.DisplayName);
		m_Description.text = itemData.Descriptions[0];
		m_Icon.sprite = itemData.Icon;
		for (int i = 0; i < m_RequiredItemsTable.Length; i++)
		{
			RequiredItemRow requiredItemRow = m_RequiredItemsTable[i];
			if (itemData.Recipe.RequiredItems.Length > i)
			{
				RequiredItem requiredItem = itemData.Recipe.RequiredItems[i];
				int total = requiredItem.Amount * Mathf.Clamp(m_CurrentDesiredAmount, 1, int.MaxValue);
				int have = MonoSingleton<InventoryController>.Instance.findItemCount(requiredItem.Name);
				requiredItemRow.Set(requiredItem.Amount, requiredItem.Name, total, have);
			}
			else
			{
				requiredItemRow.Set(0, "", 0, 0);
			}
		}
		m_TotalTime.text = $"耗时: {(itemData.Recipe.Duration * m_CurrentDesiredAmount).ToString()}s";
		m_DesiredAmount.text = ((m_CurrentDesiredAmount > 0) ? "x" : "") + m_CurrentDesiredAmount;
	}

	private void On_Slot_Deselect(BaseEventData data, RecipeSlot deselectedSlot)
	{
		((MonoBehaviour)this).StartCoroutine(C_CheckNextSelection());
	}

	private IEnumerator C_CheckNextSelection()
	{
		yield return null;
		RecipeSlot recipeSlot = null;
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (Object.op_Implicit((Object)(object)currentSelectedGameObject))
		{
			recipeSlot = currentSelectedGameObject.GetComponent<RecipeSlot>();
		}
		if (!Object.op_Implicit((Object)(object)currentSelectedGameObject) || !Object.op_Implicit((Object)(object)recipeSlot))
		{
			if (Object.op_Implicit((Object)(object)m_Window))
			{
				m_Window.Close();
			}
			InspectedSlot = null;
		}
	}
}
