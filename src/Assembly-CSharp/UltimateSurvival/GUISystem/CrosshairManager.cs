using System;
using UltimateSurvival.Building;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000650 RID: 1616
	public class CrosshairManager : GUIBehaviour
	{
		// Token: 0x0600336D RID: 13165 RVA: 0x001693B4 File Offset: 0x001675B4
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

		// Token: 0x0600336E RID: 13166 RVA: 0x00169469 File Offset: 0x00167669
		private void Update()
		{
			if (this.m_CurrentCrosshair)
			{
				this.m_CurrentCrosshair.Update(base.Player);
			}
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x0016948C File Offset: 0x0016768C
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

		// Token: 0x06003370 RID: 13168 RVA: 0x001694D8 File Offset: 0x001676D8
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

		// Token: 0x06003371 RID: 13169 RVA: 0x0016958B File Offset: 0x0016778B
		private void OnStart_Aim()
		{
			if (this.m_CurrentCrosshair != null && this.m_CurrentCrosshair.HideWhenAiming)
			{
				this.m_CurrentCrosshair.SetActive(false);
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x001695AE File Offset: 0x001677AE
		private void OnStop_Aim()
		{
			if (this.m_CurrentCrosshair != null && MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_CurrentCrosshair.SetActive(true);
			}
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x001695D0 File Offset: 0x001677D0
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

		// Token: 0x04002DAF RID: 11695
		[Header("Messages")]
		[SerializeField]
		private GameObject m_OpenMessage;

		// Token: 0x04002DB0 RID: 11696
		[SerializeField]
		private MessageForPlayer m_GrabMessage;

		// Token: 0x04002DB1 RID: 11697
		[Header("Crosshairs")]
		[SerializeField]
		private CrosshairData m_DefaultCrosshair;

		// Token: 0x04002DB2 RID: 11698
		[SerializeField]
		private CrosshairData[] m_CustomCrosshairs;

		// Token: 0x04002DB3 RID: 11699
		private CrosshairData m_CurrentCrosshair;

		// Token: 0x04002DB4 RID: 11700
		private float m_OpenGraphicHideTime;
	}
}
