using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class AnvilGUI : GUIBehaviour
{
	[SerializeField]
	private Text m_RequiredItemsText;

	[SerializeField]
	private Color m_HasEnoughColor = Color.yellow;

	[SerializeField]
	private Color m_NotEnoughColor = Color.red;

	[SerializeField]
	private Image m_ProgressBar;

	[SerializeField]
	private Button m_RepairButton;

	private ItemContainer m_InventoryContainer;

	private ItemContainer m_InputContainer;

	private ItemContainer m_ResultContainer;

	private string m_EnoughHex;

	private string m_NotEnoughHex;

	private Anvil m_CurrentAnvil;

	private void Awake()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		m_InputContainer = ((Component)((Component)this).transform.FindDeepChild("Input")).GetComponent<ItemContainer>();
		m_ResultContainer = ((Component)((Component)this).transform.FindDeepChild("Result")).GetComponent<ItemContainer>();
		MonoSingleton<InventoryController>.Instance.State.AddChangeListener(OnChanged_InventoryState);
		MonoSingleton<InventoryController>.Instance.OpenAnvil.SetTryer(Try_OpenAnvil);
		((UnityEvent)m_RepairButton.onClick).AddListener(new UnityAction(On_ButtonClicked));
	}

	private void Start()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		m_EnoughHex = ColorUtils.ColorToHex(Color32.op_Implicit(m_HasEnoughColor));
		m_NotEnoughHex = ColorUtils.ColorToHex(Color32.op_Implicit(m_NotEnoughColor));
		m_InventoryContainer = base.Controller.GetContainer("Inventory");
		m_InventoryContainer.Slot_Refreshed.AddListener(On_InventoryRefreshed);
	}

	private void On_ButtonClicked()
	{
		if (Object.op_Implicit((Object)(object)m_CurrentAnvil))
		{
			m_CurrentAnvil.Repairing.TryStart();
		}
	}

	private void OnChanged_InventoryState()
	{
		if (Object.op_Implicit((Object)(object)m_CurrentAnvil) && MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			m_CurrentAnvil.RepairProgress.RemoveChangeListener(OnChanged_RepairProgress);
			m_CurrentAnvil.InputItemReadyForRepair.RemoveChangeListener(OnChanged_InputItemIsReadyForRepair);
			m_CurrentAnvil = null;
		}
	}

	private bool Try_OpenAnvil(Anvil anvil)
	{
		if (MonoSingleton<InventoryController>.Instance.IsClosed && MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Anvil))
		{
			m_CurrentAnvil = anvil;
			m_InputContainer.Setup(anvil.InputHolder);
			m_ResultContainer.Setup(anvil.ResultHolder);
			m_CurrentAnvil.RepairProgress.AddChangeListener(OnChanged_RepairProgress);
			m_CurrentAnvil.InputItemReadyForRepair.AddChangeListener(OnChanged_InputItemIsReadyForRepair);
			m_ProgressBar.fillAmount = m_CurrentAnvil.RepairProgress.Get();
			return true;
		}
		return false;
	}

	private void OnChanged_RepairProgress()
	{
		m_ProgressBar.fillAmount = m_CurrentAnvil.RepairProgress.Get();
	}

	private void On_InventoryRefreshed(Slot displayer)
	{
		UpdateRequiredItemsList();
	}

	private void OnChanged_InputItemIsReadyForRepair()
	{
		UpdateRequiredItemsList();
	}

	private void UpdateRequiredItemsList()
	{
		if (Object.op_Implicit((Object)(object)m_CurrentAnvil) && m_InputContainer.Slots[0].HasItem)
		{
			if (m_CurrentAnvil.InputItemReadyForRepair.Is(value: false))
			{
				m_RequiredItemsText.text = "<size=10><i>This item doesn't require repairing...</i></size>";
				return;
			}
			m_RequiredItemsText.text = string.Empty;
			Anvil.ItemToRepair inputItem = m_CurrentAnvil.InputItem;
			StringBuilder stringBuilder = new StringBuilder("Requires: \n");
			for (int i = 0; i < m_CurrentAnvil.RequiredItems.Length; i++)
			{
				Anvil.RequiredItem requiredItem = m_CurrentAnvil.RequiredItems[i];
				string arg = (requiredItem.HasEnough() ? m_EnoughHex : m_NotEnoughHex);
				stringBuilder.AppendFormat("<color={0}>{1} x {2}</color> \n", arg, inputItem.Recipe.RequiredItems[i].Name, requiredItem.Needs);
			}
			m_RequiredItemsText.text = stringBuilder.ToString();
		}
		else
		{
			m_RequiredItemsText.text = "<size=10><i>Place an item here...</i></size>";
		}
	}
}
