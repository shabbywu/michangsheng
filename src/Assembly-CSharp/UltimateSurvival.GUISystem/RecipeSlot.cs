using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class RecipeSlot : Selectable
{
	public delegate void BaseAction(BaseEventData data, RecipeSlot slot);

	public delegate void PointerAction(PointerEventData data, RecipeSlot slot);

	[Header("Setup")]
	[SerializeField]
	private Text m_RecipeName;

	[SerializeField]
	private Image m_Icon;

	public ItemData Result { get; private set; }

	public event BaseAction E_Deselect;

	public event PointerAction PointerUp;

	public void ShowRecipeForItem(ItemData item)
	{
		_ = MonoSingleton<InventoryController>.Instance.Database;
		Result = item;
		m_RecipeName.text = ((item.DisplayName == string.Empty) ? item.Name : item.DisplayName);
		m_Icon.sprite = item.Icon;
	}

	public void ShowRecipeForItem(ItemData item, ItemDatabase database)
	{
		Result = item;
		m_RecipeName.text = ((item.DisplayName == string.Empty) ? item.Name : item.DisplayName);
		m_Icon.sprite = item.Icon;
	}

	public override void OnDeselect(BaseEventData data)
	{
		if (Object.op_Implicit((Object)(object)MonoSingleton<GUIController>.Instance) && MonoSingleton<GUIController>.Instance.MouseOverSelectionKeeper())
		{
			((MonoBehaviour)this).StartCoroutine(C_WaitAndSelect(1));
			return;
		}
		Event.fireOut("ShopOnDeselect", this);
		if (this.E_Deselect != null)
		{
			this.E_Deselect(data, this);
		}
		((Selectable)this).OnDeselect(data);
	}

	public override void OnPointerUp(PointerEventData data)
	{
		((Selectable)this).OnPointerUp(data);
		if (this.PointerUp != null)
		{
			this.PointerUp(data, this);
		}
	}

	protected IEnumerator C_WaitAndSelect(int waitFrameCount)
	{
		for (int i = 0; i < waitFrameCount; i++)
		{
			yield return null;
		}
		((Selectable)this).Select();
	}
}
