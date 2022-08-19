using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200064C RID: 1612
	public class AnvilGUI : GUIBehaviour
	{
		// Token: 0x0600335A RID: 13146 RVA: 0x00168DBC File Offset: 0x00166FBC
		private void Awake()
		{
			this.m_InputContainer = base.transform.FindDeepChild("Input").GetComponent<ItemContainer>();
			this.m_ResultContainer = base.transform.FindDeepChild("Result").GetComponent<ItemContainer>();
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
			MonoSingleton<InventoryController>.Instance.OpenAnvil.SetTryer(new Attempt<Anvil>.GenericTryerDelegate(this.Try_OpenAnvil));
			this.m_RepairButton.onClick.AddListener(new UnityAction(this.On_ButtonClicked));
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x00168E54 File Offset: 0x00167054
		private void Start()
		{
			this.m_EnoughHex = ColorUtils.ColorToHex(this.m_HasEnoughColor);
			this.m_NotEnoughHex = ColorUtils.ColorToHex(this.m_NotEnoughColor);
			this.m_InventoryContainer = base.Controller.GetContainer("Inventory");
			this.m_InventoryContainer.Slot_Refreshed.AddListener(new Action<Slot>(this.On_InventoryRefreshed));
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x00168EBF File Offset: 0x001670BF
		private void On_ButtonClicked()
		{
			if (this.m_CurrentAnvil)
			{
				this.m_CurrentAnvil.Repairing.TryStart();
			}
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x00168EE0 File Offset: 0x001670E0
		private void OnChanged_InventoryState()
		{
			if (this.m_CurrentAnvil && MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_CurrentAnvil.RepairProgress.RemoveChangeListener(new Action(this.OnChanged_RepairProgress));
				this.m_CurrentAnvil.InputItemReadyForRepair.RemoveChangeListener(new Action(this.OnChanged_InputItemIsReadyForRepair));
				this.m_CurrentAnvil = null;
			}
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x00168F48 File Offset: 0x00167148
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

		// Token: 0x0600335F RID: 13151 RVA: 0x00168FF5 File Offset: 0x001671F5
		private void OnChanged_RepairProgress()
		{
			this.m_ProgressBar.fillAmount = this.m_CurrentAnvil.RepairProgress.Get();
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x00169012 File Offset: 0x00167212
		private void On_InventoryRefreshed(Slot displayer)
		{
			this.UpdateRequiredItemsList();
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x00169012 File Offset: 0x00167212
		private void OnChanged_InputItemIsReadyForRepair()
		{
			this.UpdateRequiredItemsList();
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x0016901C File Offset: 0x0016721C
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

		// Token: 0x04002D91 RID: 11665
		[SerializeField]
		private Text m_RequiredItemsText;

		// Token: 0x04002D92 RID: 11666
		[SerializeField]
		private Color m_HasEnoughColor = Color.yellow;

		// Token: 0x04002D93 RID: 11667
		[SerializeField]
		private Color m_NotEnoughColor = Color.red;

		// Token: 0x04002D94 RID: 11668
		[SerializeField]
		private Image m_ProgressBar;

		// Token: 0x04002D95 RID: 11669
		[SerializeField]
		private Button m_RepairButton;

		// Token: 0x04002D96 RID: 11670
		private ItemContainer m_InventoryContainer;

		// Token: 0x04002D97 RID: 11671
		private ItemContainer m_InputContainer;

		// Token: 0x04002D98 RID: 11672
		private ItemContainer m_ResultContainer;

		// Token: 0x04002D99 RID: 11673
		private string m_EnoughHex;

		// Token: 0x04002D9A RID: 11674
		private string m_NotEnoughHex;

		// Token: 0x04002D9B RID: 11675
		private Anvil m_CurrentAnvil;
	}
}
