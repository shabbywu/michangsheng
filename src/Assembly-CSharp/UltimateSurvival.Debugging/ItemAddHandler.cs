using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateSurvival.Debugging;

public class ItemAddHandler : MonoBehaviour
{
	[SerializeField]
	private Dropdown m_ItemDropdown;

	[SerializeField]
	private InputField m_AmountInput;

	[SerializeField]
	private Button m_AddButton;

	private void Start()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		((UnityEvent)m_AddButton.onClick).AddListener(new UnityAction(TryAddItem));
		CreateItemOptions();
	}

	private void CreateItemOptions()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		List<OptionData> list = new List<OptionData>();
		foreach (string allItemName in MonoSingleton<InventoryController>.Instance.Database.GetAllItemNames())
		{
			list.Add(new OptionData(allItemName));
		}
		m_ItemDropdown.options = list;
		m_ItemDropdown.RefreshShownValue();
	}

	private void TryAddItem()
	{
		if (int.TryParse(m_AmountInput.text, out var result))
		{
			MonoSingleton<InventoryController>.Instance.AddItemToCollection(m_ItemDropdown.value, result, "Inventory", out var _);
		}
	}
}
