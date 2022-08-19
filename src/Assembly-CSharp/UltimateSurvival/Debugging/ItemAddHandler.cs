using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateSurvival.Debugging
{
	// Token: 0x0200065C RID: 1628
	public class ItemAddHandler : MonoBehaviour
	{
		// Token: 0x060033BB RID: 13243 RVA: 0x0016AB7E File Offset: 0x00168D7E
		private void Start()
		{
			this.m_AddButton.onClick.AddListener(new UnityAction(this.TryAddItem));
			this.CreateItemOptions();
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x0016ABA4 File Offset: 0x00168DA4
		private void CreateItemOptions()
		{
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
			foreach (string text in MonoSingleton<InventoryController>.Instance.Database.GetAllItemNames())
			{
				list.Add(new Dropdown.OptionData(text));
			}
			this.m_ItemDropdown.options = list;
			this.m_ItemDropdown.RefreshShownValue();
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x0016AC24 File Offset: 0x00168E24
		private void TryAddItem()
		{
			int amount;
			if (int.TryParse(this.m_AmountInput.text, out amount))
			{
				int num;
				MonoSingleton<InventoryController>.Instance.AddItemToCollection(this.m_ItemDropdown.value, amount, "Inventory", out num);
			}
		}

		// Token: 0x04002DF7 RID: 11767
		[SerializeField]
		private Dropdown m_ItemDropdown;

		// Token: 0x04002DF8 RID: 11768
		[SerializeField]
		private InputField m_AmountInput;

		// Token: 0x04002DF9 RID: 11769
		[SerializeField]
		private Button m_AddButton;
	}
}
