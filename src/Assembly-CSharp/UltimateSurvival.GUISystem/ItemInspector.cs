using System.Collections;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class ItemInspector : GUIBehaviour
{
	[Header("Setup")]
	[SerializeField]
	private Camera m_GUICamera;

	[SerializeField]
	private Window m_Window;

	[Header("Item Info")]
	[SerializeField]
	private Text m_ItemName;

	[SerializeField]
	private Text m_MainDescription;

	[SerializeField]
	private Text m_SecondaryDescription;

	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private DurabilityBar m_DurabilityBar;

	[SerializeField]
	private Text m_Magazine;

	[Header("Actions")]
	[SerializeField]
	private Button m_DropButton;

	[SerializeField]
	private Button m_ConsumeButton;

	[SerializeField]
	private Button m_DismantleButton;

	[Header("Audio")]
	[SerializeField]
	private SoundPlayer m_ItemDropAudio;

	[SerializeField]
	private SoundPlayer m_DismantleAudio;

	[SerializeField]
	private float m_ConsumeVolume = 0.6f;

	private ItemContainer[] m_InspectableContainers;

	private ItemContainer m_InventoryContainer;

	private float m_LastConsumeTime;

	public Slot InspectedSlot { get; private set; }

	private void Awake()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		((UnityEvent)m_DismantleButton.onClick).AddListener(new UnityAction(On_DismantleClicked));
		((UnityEvent)m_DropButton.onClick).AddListener(new UnityAction(On_DropClicked));
		((UnityEvent)m_ConsumeButton.onClick).AddListener(new UnityAction(On_ConsumeClicked));
	}

	private void Start()
	{
		m_InspectableContainers = base.Controller.Containers;
		ItemContainer[] inspectableContainers = m_InspectableContainers;
		for (int i = 0; i < inspectableContainers.Length; i++)
		{
			inspectableContainers[i].Slot_PointerUp += On_Slot_PointerUp;
		}
		m_InventoryContainer = base.Controller.GetContainer("Inventory");
	}

	private void On_Slot_PointerUp(BaseEventData data, Slot slot)
	{
		if (!MonoSingleton<InventoryController>.Instance.IsClosed && slot.HasItem && (Object)(object)EventSystem.current.currentSelectedGameObject == (Object)(object)((Component)slot).gameObject)
		{
			if (Object.op_Implicit((Object)(object)m_Window))
			{
				m_Window.Open();
			}
			InspectedSlot = slot;
			ShowInfo(slot.CurrentItem);
			InspectedSlot.E_Deselect += delegate
			{
				((MonoBehaviour)this).StartCoroutine(C_WaitAndSelect());
			};
			InspectedSlot.ItemHolder.Updated.AddListener(On_InspectedHolderUpdated);
		}
		else
		{
			((MonoBehaviour)this).StopAllCoroutines();
			((MonoBehaviour)this).StartCoroutine(C_WaitAndSelect());
		}
	}

	private void ShowInfo(SavableItem item)
	{
		m_ItemName.text = ((item.ItemData.DisplayName == string.Empty) ? item.ItemData.Name : item.ItemData.DisplayName);
		if (item.ItemData.Descriptions.Length != 0)
		{
			m_MainDescription.text = item.GetDescription(0);
		}
		else
		{
			m_MainDescription.text = "";
		}
		if (item.ItemData.Descriptions.Length > 1)
		{
			m_SecondaryDescription.text = item.GetDescription(1);
		}
		else
		{
			m_SecondaryDescription.text = "";
		}
		m_Icon.sprite = item.ItemData.Icon;
		if (item.HasProperty("Durability"))
		{
			if (!m_DurabilityBar.Active)
			{
				m_DurabilityBar.SetActive(active: true);
			}
			m_DurabilityBar.SetFillAmount(item.GetPropertyValue("Durability").Float.Ratio);
		}
		else if (m_DurabilityBar.Active)
		{
			m_DurabilityBar.SetActive(active: false);
		}
		if (item.FindPropertyValue("Magazine", out var propertyValue))
		{
			ItemProperty.IntRange intRange = propertyValue.IntRange;
			m_Magazine.text = "库存: " + intRange.ToString();
		}
		else
		{
			m_Magazine.text = "";
		}
		((Component)m_ConsumeButton).gameObject.SetActive(item.HasProperty("Can Consume"));
		((Component)m_DismantleButton).gameObject.SetActive(item.HasProperty("Can Dismantle"));
	}

	private void On_InspectedHolderUpdated(ItemHolder holder)
	{
		if (!holder.HasItem)
		{
			m_Window.Close();
			try
			{
				InspectedSlot.ItemHolder.Updated.RemoveListener(On_InspectedHolderUpdated);
				InspectedSlot = null;
			}
			catch
			{
			}
		}
	}

	private void On_DismantleClicked()
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		SavableItem currentItem = InspectedSlot.CurrentItem;
		InspectedSlot.ItemHolder.SetItem(null);
		RequiredItem[] requiredItems = currentItem.ItemData.Recipe.RequiredItems;
		for (int i = 0; i < requiredItems.Length; i++)
		{
			int amount = Mathf.RoundToInt((float)requiredItems[i].Amount * currentItem.GetPropertyValue("Durability").Float.Ratio * 0.6f) + 1;
			m_InventoryContainer.TryAddItem(requiredItems[i].Name, amount);
		}
		MonoSingleton<MessageDisplayer>.Instance.PushMessage($"<color=yellow>{currentItem.Name}</color> has been dismantled");
		m_DismantleAudio.Play2D();
	}

	private void On_DropClicked()
	{
		SavableItem savableItem = base.Controller.Player.EquippedItem.Get();
		SavableItem currentItem = InspectedSlot.CurrentItem;
		if (savableItem == currentItem)
		{
			base.Controller.Player.ChangeEquippedItem.Try(null, arg2: true);
		}
		Tooltip component = ((Component)this).gameObject.GetComponent<Tooltip>();
		((Avatar)KBEngineApp.app.player())?.dropRequest(component.item.itemUUID);
	}

	private void On_ConsumeClicked()
	{
		SavableItem currentItem = InspectedSlot.CurrentItem;
		if (currentItem.HasProperty("Can Consume"))
		{
			if (currentItem.HasProperty("Consume Sound") && Time.time - m_LastConsumeTime > 2f)
			{
				GameController.Audio.Play2D(currentItem.GetPropertyValue("Consume Sound").Sound, m_ConsumeVolume);
			}
			Tooltip component = ((Component)this).gameObject.GetComponent<Tooltip>();
			((Avatar)KBEngineApp.app.player())?.useItemRequest(component.item.itemUUID);
			m_LastConsumeTime = Time.time;
		}
	}

	private IEnumerator C_WaitAndSelect()
	{
		yield return null;
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		Slot slot = null;
		if (Object.op_Implicit((Object)(object)currentSelectedGameObject))
		{
			slot = currentSelectedGameObject.GetComponent<Slot>();
		}
		if (!Object.op_Implicit((Object)(object)currentSelectedGameObject) || !Object.op_Implicit((Object)(object)slot) || !slot.HasItem)
		{
			if (Object.op_Implicit((Object)(object)m_Window))
			{
				m_Window.Close();
			}
			InspectedSlot = null;
		}
	}
}
