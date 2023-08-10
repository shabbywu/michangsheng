using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class CraftingCategory : Selectable
{
	public Message<CraftingCategory> Selected = new Message<CraftingCategory>();

	[Header("Settings")]
	[SerializeField]
	private string m_DisplayName = "None";

	[SerializeField]
	[Reorderable]
	private ReorderableStringList m_CorrespondingCategories;

	public string DisplayName => m_DisplayName;

	public bool HasCategory(string categoryName)
	{
		for (int i = 0; i < m_CorrespondingCategories.Count; i++)
		{
			if (m_CorrespondingCategories[i] == categoryName)
			{
				return true;
			}
		}
		return false;
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		((Selectable)this).OnPointerDown(eventData);
		Selected.Send(this);
	}
}
