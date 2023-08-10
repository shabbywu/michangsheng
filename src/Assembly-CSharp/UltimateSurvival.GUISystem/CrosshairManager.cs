using UltimateSurvival.Building;
using UnityEngine;

namespace UltimateSurvival.GUISystem;

public class CrosshairManager : GUIBehaviour
{
	[Header("Messages")]
	[SerializeField]
	private GameObject m_OpenMessage;

	[SerializeField]
	private MessageForPlayer m_GrabMessage;

	[Header("Crosshairs")]
	[SerializeField]
	private CrosshairData m_DefaultCrosshair;

	[SerializeField]
	private CrosshairData[] m_CustomCrosshairs;

	private CrosshairData m_CurrentCrosshair;

	private float m_OpenGraphicHideTime;

	private void Start()
	{
		base.Player.EquippedItem.AddChangeListener(OnChanged_EquippedItem);
		base.Player.Aim.AddStartListener(OnStart_Aim);
		base.Player.Aim.AddStopListener(OnStop_Aim);
		base.Player.RaycastData.AddChangeListener(OnChanged_RaycastObject);
		MonoSingleton<InventoryController>.Instance.State.AddChangeListener(OnChanged_InventoryState);
		if ((bool)base.Player.EquippedItem.Get())
		{
			OnChanged_EquippedItem();
		}
	}

	private void Update()
	{
		if ((bool)m_CurrentCrosshair)
		{
			m_CurrentCrosshair.Update(base.Player);
		}
	}

	private void OnChanged_InventoryState()
	{
		bool isClosed = MonoSingleton<InventoryController>.Instance.IsClosed;
		if ((bool)m_CurrentCrosshair)
		{
			m_CurrentCrosshair.SetActive(isClosed);
		}
		if (!isClosed)
		{
			m_OpenMessage.SetActive(false);
			m_GrabMessage.Toggle(toggle: false);
		}
	}

	private void OnChanged_EquippedItem()
	{
		if (!MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			return;
		}
		if ((bool)m_CurrentCrosshair)
		{
			m_CurrentCrosshair.SetActive(active: false);
			m_CurrentCrosshair = null;
		}
		SavableItem savableItem = base.Player.EquippedItem.Get();
		if (savableItem != null)
		{
			for (int i = 0; i < m_CustomCrosshairs.Length; i++)
			{
				if (m_CustomCrosshairs[i].ItemName == savableItem.ItemData.Name)
				{
					m_CurrentCrosshair = m_CustomCrosshairs[i];
					m_CurrentCrosshair.SetActive(active: true);
					return;
				}
			}
		}
		m_CurrentCrosshair = m_DefaultCrosshair;
		m_CurrentCrosshair.SetActive(active: true);
	}

	private void OnStart_Aim()
	{
		if (m_CurrentCrosshair != null && m_CurrentCrosshair.HideWhenAiming)
		{
			m_CurrentCrosshair.SetActive(active: false);
		}
	}

	private void OnStop_Aim()
	{
		if (m_CurrentCrosshair != null && MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			m_CurrentCrosshair.SetActive(active: true);
		}
	}

	private void OnChanged_RaycastObject()
	{
		if (!MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			m_OpenMessage.SetActive(false);
			m_GrabMessage.Toggle(toggle: false);
			if ((bool)m_CurrentCrosshair)
			{
				m_CurrentCrosshair.SetActive(active: false);
			}
			return;
		}
		IInventoryTrigger inventoryTrigger = null;
		ItemPickup itemPickup = null;
		Door door = null;
		SleepingBag sleepingBag = null;
		Lamp lamp = null;
		RaycastData raycastData = base.Player.RaycastData.Get();
		if ((bool)raycastData && raycastData.ObjectIsInteractable)
		{
			inventoryTrigger = raycastData.GameObject.GetComponent<IInventoryTrigger>();
			itemPickup = raycastData.GameObject.GetComponent<ItemPickup>();
			door = raycastData.GameObject.GetComponent<Door>();
			sleepingBag = raycastData.GameObject.GetComponent<SleepingBag>();
			lamp = raycastData.GameObject.GetComponent<Lamp>();
		}
		m_OpenMessage.SetActive(inventoryTrigger != null);
		if ((Object)(object)itemPickup != (Object)null && itemPickup.ItemToAdd != null)
		{
			m_GrabMessage.Toggle(toggle: true);
			m_GrabMessage.SetText(itemPickup.ItemToAdd.Name + ((itemPickup.ItemToAdd.CurrentInStack > 1) ? (" x " + itemPickup.ItemToAdd.CurrentInStack) : ""));
		}
		else if ((Object)(object)door != (Object)null)
		{
			m_GrabMessage.Toggle(toggle: true);
			m_GrabMessage.SetText(door.Open ? "Close the door" : "Open the door");
		}
		else if ((Object)(object)sleepingBag != (Object)null)
		{
			m_GrabMessage.Toggle(toggle: true);
			m_GrabMessage.SetText((MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Night) ? "Sleep..." : "You can only sleep at night time!");
		}
		else if ((Object)(object)lamp != (Object)null)
		{
			m_GrabMessage.Toggle(toggle: true);
			m_GrabMessage.SetText(string.Format("Turn {0}", (!lamp.State) ? "<color=yellow>ON</color>" : "<color=red>OFF</color>"));
		}
		else
		{
			m_GrabMessage.Toggle(toggle: false);
		}
		if ((bool)m_CurrentCrosshair)
		{
			bool active = !base.Player.Aim.Active && inventoryTrigger == null && (Object)(object)itemPickup == (Object)null && (Object)(object)door == (Object)null && (Object)(object)sleepingBag == (Object)null;
			m_CurrentCrosshair.SetActive(active);
		}
	}
}
