using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateSurvival.Debugging
{
	// Token: 0x0200095F RID: 2399
	public class ItemAddHandler : MonoBehaviour
	{
		// Token: 0x06003D49 RID: 15689 RVA: 0x0002C34A File Offset: 0x0002A54A
		private void Start()
		{
			this.m_AddButton.onClick.AddListener(new UnityAction(this.TryAddItem));
			this.CreateItemOptions();
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x001B3A9C File Offset: 0x001B1C9C
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

		// Token: 0x06003D4B RID: 15691 RVA: 0x001B3B1C File Offset: 0x001B1D1C
		private void TryAddItem()
		{
			int amount;
			if (int.TryParse(this.m_AmountInput.text, out amount))
			{
				int num;
				MonoSingleton<InventoryController>.Instance.AddItemToCollection(this.m_ItemDropdown.value, amount, "Inventory", out num);
			}
		}

		// Token: 0x0400377C RID: 14204
		[SerializeField]
		private Dropdown m_ItemDropdown;

		// Token: 0x0400377D RID: 14205
		[SerializeField]
		private InputField m_AmountInput;

		// Token: 0x0400377E RID: 14206
		[SerializeField]
		private Button m_AddButton;
	}
}
