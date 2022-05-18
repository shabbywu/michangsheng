using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200094B RID: 2379
	public class AnvilGUI : GUIBehaviour
	{
		// Token: 0x06003CD0 RID: 15568 RVA: 0x001B1DD4 File Offset: 0x001AFFD4
		private void Awake()
		{
			this.m_InputContainer = base.transform.FindDeepChild("Input").GetComponent<ItemContainer>();
			this.m_ResultContainer = base.transform.FindDeepChild("Result").GetComponent<ItemContainer>();
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
			MonoSingleton<InventoryController>.Instance.OpenAnvil.SetTryer(new Attempt<Anvil>.GenericTryerDelegate(this.Try_OpenAnvil));
			this.m_RepairButton.onClick.AddListener(new UnityAction(this.On_ButtonClicked));
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x001B1E6C File Offset: 0x001B006C
		private void Start()
		{
			this.m_EnoughHex = ColorUtils.ColorToHex(this.m_HasEnoughColor);
			this.m_NotEnoughHex = ColorUtils.ColorToHex(this.m_NotEnoughColor);
			this.m_InventoryContainer = base.Controller.GetContainer("Inventory");
			this.m_InventoryContainer.Slot_Refreshed.AddListener(new Action<Slot>(this.On_InventoryRefreshed));
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x0002BD77 File Offset: 0x00029F77
		private void On_ButtonClicked()
		{
			if (this.m_CurrentAnvil)
			{
				this.m_CurrentAnvil.Repairing.TryStart();
			}
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x001B1ED8 File Offset: 0x001B00D8
		private void OnChanged_InventoryState()
		{
			if (this.m_CurrentAnvil && MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_CurrentAnvil.RepairProgress.RemoveChangeListener(new Action(this.OnChanged_RepairProgress));
				this.m_CurrentAnvil.InputItemReadyForRepair.RemoveChangeListener(new Action(this.OnChanged_InputItemIsReadyForRepair));
				this.m_CurrentAnvil = null;
			}
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x001B1F40 File Offset: 0x001B0140
		private bool Try_OpenAnvil(Anvil anvil)
		{
			if (MonoSingleton<InventoryController>.Instance.IsClosed && MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Anvil))
			{
				this.m_CurrentAnvil = anvil;
				this.m_InputContainer.Setup(anvil.InputHolder);
				this.m_ResultContainer.Setup(anvil.ResultHolder);
				this.m_CurrentAnvil.RepairProgress.AddChangeListener(new Action(this.OnChanged_RepairProgress));
				this.m_CurrentAnvil.InputItemReadyForRepair.AddChangeListener(new Action(this.OnChanged_InputItemIsReadyForRepair));
				this.m_ProgressBar.fillAmount = this.m_CurrentAnvil.RepairProgress.Get();
				return true;
			}
			return false;
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x0002BD97 File Offset: 0x00029F97
		private void OnChanged_RepairProgress()
		{
			this.m_ProgressBar.fillAmount = this.m_CurrentAnvil.RepairProgress.Get();
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x0002BDB4 File Offset: 0x00029FB4
		private void On_InventoryRefreshed(Slot displayer)
		{
			this.UpdateRequiredItemsList();
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x0002BDB4 File Offset: 0x00029FB4
		private void OnChanged_InputItemIsReadyForRepair()
		{
			this.UpdateRequiredItemsList();
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x001B1FF0 File Offset: 0x001B01F0
		private void UpdateRequiredItemsList()
		{
			if (!this.m_CurrentAnvil || !this.m_InputContainer.Slots[0].HasItem)
			{
				this.m_RequiredItemsText.text = "<size=10><i>Place an item here...</i></size>";
				return;
			}
			if (this.m_CurrentAnvil.InputItemReadyForRepair.Is(false))
			{
				this.m_RequiredItemsText.text = "<size=10><i>This item doesn't require repairing...</i></size>";
				return;
			}
			this.m_RequiredItemsText.text = string.Empty;
			Anvil.ItemToRepair inputItem = this.m_CurrentAnvil.InputItem;
			StringBuilder stringBuilder = new StringBuilder("Requires: \n");
			for (int i = 0; i < this.m_CurrentAnvil.RequiredItems.Length; i++)
			{
				Anvil.RequiredItem requiredItem = this.m_CurrentAnvil.RequiredItems[i];
				string arg = requiredItem.HasEnough() ? this.m_EnoughHex : this.m_NotEnoughHex;
				stringBuilder.AppendFormat("<color={0}>{1} x {2}</color> \n", arg, inputItem.Recipe.RequiredItems[i].Name, requiredItem.Needs);
			}
			this.m_RequiredItemsText.text = stringBuilder.ToString();
		}

		// Token: 0x04003708 RID: 14088
		[SerializeField]
		private Text m_RequiredItemsText;

		// Token: 0x04003709 RID: 14089
		[SerializeField]
		private Color m_HasEnoughColor = Color.yellow;

		// Token: 0x0400370A RID: 14090
		[SerializeField]
		private Color m_NotEnoughColor = Color.red;

		// Token: 0x0400370B RID: 14091
		[SerializeField]
		private Image m_ProgressBar;

		// Token: 0x0400370C RID: 14092
		[SerializeField]
		private Button m_RepairButton;

		// Token: 0x0400370D RID: 14093
		private ItemContainer m_InventoryContainer;

		// Token: 0x0400370E RID: 14094
		private ItemContainer m_InputContainer;

		// Token: 0x0400370F RID: 14095
		private ItemContainer m_ResultContainer;

		// Token: 0x04003710 RID: 14096
		private string m_EnoughHex;

		// Token: 0x04003711 RID: 14097
		private string m_NotEnoughHex;

		// Token: 0x04003712 RID: 14098
		private Anvil m_CurrentAnvil;
	}
}
