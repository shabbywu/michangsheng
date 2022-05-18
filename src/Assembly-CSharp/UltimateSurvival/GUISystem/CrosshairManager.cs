using System;
using UltimateSurvival.Building;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200094F RID: 2383
	public class CrosshairManager : GUIBehaviour
	{
		// Token: 0x06003CE3 RID: 15587 RVA: 0x001B233C File Offset: 0x001B053C
		private void Start()
		{
			base.Player.EquippedItem.AddChangeListener(new Action(this.OnChanged_EquippedItem));
			base.Player.Aim.AddStartListener(new Action(this.OnStart_Aim));
			base.Player.Aim.AddStopListener(new Action(this.OnStop_Aim));
			base.Player.RaycastData.AddChangeListener(new Action(this.OnChanged_RaycastObject));
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
			if (base.Player.EquippedItem.Get())
			{
				this.OnChanged_EquippedItem();
			}
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x0002BE06 File Offset: 0x0002A006
		private void Update()
		{
			if (this.m_CurrentCrosshair)
			{
				this.m_CurrentCrosshair.Update(base.Player);
			}
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x001B23F4 File Offset: 0x001B05F4
		private void OnChanged_InventoryState()
		{
			bool isClosed = MonoSingleton<InventoryController>.Instance.IsClosed;
			if (this.m_CurrentCrosshair)
			{
				this.m_CurrentCrosshair.SetActive(isClosed);
			}
			if (!isClosed)
			{
				this.m_OpenMessage.SetActive(false);
				this.m_GrabMessage.Toggle(false);
			}
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x001B2440 File Offset: 0x001B0640
		private void OnChanged_EquippedItem()
		{
			if (!MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				return;
			}
			if (this.m_CurrentCrosshair)
			{
				this.m_CurrentCrosshair.SetActive(false);
				this.m_CurrentCrosshair = null;
			}
			SavableItem savableItem = base.Player.EquippedItem.Get();
			if (savableItem != null)
			{
				for (int i = 0; i < this.m_CustomCrosshairs.Length; i++)
				{
					if (this.m_CustomCrosshairs[i].ItemName == savableItem.ItemData.Name)
					{
						this.m_CurrentCrosshair = this.m_CustomCrosshairs[i];
						this.m_CurrentCrosshair.SetActive(true);
						return;
					}
				}
			}
			this.m_CurrentCrosshair = this.m_DefaultCrosshair;
			this.m_CurrentCrosshair.SetActive(true);
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x0002BE26 File Offset: 0x0002A026
		private void OnStart_Aim()
		{
			if (this.m_CurrentCrosshair != null && this.m_CurrentCrosshair.HideWhenAiming)
			{
				this.m_CurrentCrosshair.SetActive(false);
			}
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x0002BE49 File Offset: 0x0002A049
		private void OnStop_Aim()
		{
			if (this.m_CurrentCrosshair != null && MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_CurrentCrosshair.SetActive(true);
			}
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x001B24F4 File Offset: 0x001B06F4
		private void OnChanged_RaycastObject()
		{
			if (!MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_OpenMessage.SetActive(false);
				this.m_GrabMessage.Toggle(false);
				if (this.m_CurrentCrosshair)
				{
					this.m_CurrentCrosshair.SetActive(false);
				}
				return;
			}
			IInventoryTrigger inventoryTrigger = null;
			ItemPickup itemPickup = null;
			Door door = null;
			SleepingBag sleepingBag = null;
			Lamp lamp = null;
			RaycastData raycastData = base.Player.RaycastData.Get();
			if (raycastData && raycastData.ObjectIsInteractable)
			{
				inventoryTrigger = raycastData.GameObject.GetComponent<IInventoryTrigger>();
				itemPickup = raycastData.GameObject.GetComponent<ItemPickup>();
				door = raycastData.GameObject.GetComponent<Door>();
				sleepingBag = raycastData.GameObject.GetComponent<SleepingBag>();
				lamp = raycastData.GameObject.GetComponent<Lamp>();
			}
			this.m_OpenMessage.SetActive(inventoryTrigger != null);
			if (itemPickup != null && itemPickup.ItemToAdd != null)
			{
				this.m_GrabMessage.Toggle(true);
				this.m_GrabMessage.SetText(itemPickup.ItemToAdd.Name + ((itemPickup.ItemToAdd.CurrentInStack > 1) ? (" x " + itemPickup.ItemToAdd.CurrentInStack) : ""));
			}
			else if (door != null)
			{
				this.m_GrabMessage.Toggle(true);
				this.m_GrabMessage.SetText(door.Open ? "Close the door" : "Open the door");
			}
			else if (sleepingBag != null)
			{
				this.m_GrabMessage.Toggle(true);
				this.m_GrabMessage.SetText((MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Night) ? "Sleep..." : "You can only sleep at night time!");
			}
			else if (lamp != null)
			{
				this.m_GrabMessage.Toggle(true);
				this.m_GrabMessage.SetText(string.Format("Turn {0}", (!lamp.State) ? "<color=yellow>ON</color>" : "<color=red>OFF</color>"));
			}
			else
			{
				this.m_GrabMessage.Toggle(false);
			}
			if (this.m_CurrentCrosshair)
			{
				bool active = !base.Player.Aim.Active && inventoryTrigger == null && itemPickup == null && door == null && sleepingBag == null;
				this.m_CurrentCrosshair.SetActive(active);
			}
		}

		// Token: 0x04003726 RID: 14118
		[Header("Messages")]
		[SerializeField]
		private GameObject m_OpenMessage;

		// Token: 0x04003727 RID: 14119
		[SerializeField]
		private MessageForPlayer m_GrabMessage;

		// Token: 0x04003728 RID: 14120
		[Header("Crosshairs")]
		[SerializeField]
		private CrosshairData m_DefaultCrosshair;

		// Token: 0x04003729 RID: 14121
		[SerializeField]
		private CrosshairData[] m_CustomCrosshairs;

		// Token: 0x0400372A RID: 14122
		private CrosshairData m_CurrentCrosshair;

		// Token: 0x0400372B RID: 14123
		private float m_OpenGraphicHideTime;
	}
}
