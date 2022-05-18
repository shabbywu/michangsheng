using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200095E RID: 2398
	public class SmeltingStationGUI : MonoBehaviour
	{
		// Token: 0x06003D42 RID: 15682 RVA: 0x0002C2C4 File Offset: 0x0002A4C4
		private void Awake()
		{
			this.m_Input = base.transform.FindDeepChild("Input").GetComponent<ItemContainer>();
			this.m_Fuel = base.transform.FindDeepChild("Fuel").GetComponent<ItemContainer>();
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x001B38C4 File Offset: 0x001B1AC4
		private void Start()
		{
			if (this.m_Type == SmeltingStationType.Furnace)
			{
				MonoSingleton<InventoryController>.Instance.OpenFurnace.SetTryer(new Attempt<SmeltingStation>.GenericTryerDelegate(this.Try_OpenSmeltingStation));
			}
			else if (this.m_Type == SmeltingStationType.Campfire)
			{
				MonoSingleton<InventoryController>.Instance.OpenCampfire.SetTryer(new Attempt<SmeltingStation>.GenericTryerDelegate(this.Try_OpenSmeltingStation));
			}
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x001B3938 File Offset: 0x001B1B38
		private bool Try_OpenSmeltingStation(SmeltingStation station)
		{
			if (MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				bool flag = false;
				if (this.m_Type == SmeltingStationType.Furnace)
				{
					flag = MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Furnace);
				}
				else if (this.m_Type == SmeltingStationType.Campfire)
				{
					flag = MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Campfire);
				}
				if (flag)
				{
					this.m_CurrentStation = station;
					station.IsBurning.AddChangeListener(new Action(this.OnChanged_IsBurning));
					station.Progress.AddChangeListener(new Action(this.OnChanged_Progress));
					this.m_ProgressBar.fillAmount = this.m_CurrentStation.Progress.Get();
					this.m_Input.Setup(station.InputSlot);
					this.m_Fuel.Setup(station.FuelSlot);
					this.m_LootContainer.Setup(station.LootSlots);
					this.IsBurning.Set(this.m_CurrentStation.IsBurning.Get());
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x0002C2FC File Offset: 0x0002A4FC
		private void OnChanged_Progress()
		{
			this.m_ProgressBar.fillAmount = this.m_CurrentStation.Progress.Get();
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x001B3A34 File Offset: 0x001B1C34
		private void OnChanged_InventoryState()
		{
			if (this.m_CurrentStation && MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_CurrentStation.IsBurning.RemoveChangeListener(new Action(this.OnChanged_IsBurning));
				this.m_CurrentStation.Progress.RemoveChangeListener(new Action(this.OnChanged_Progress));
				this.m_CurrentStation = null;
			}
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x0002C319 File Offset: 0x0002A519
		private void OnChanged_IsBurning()
		{
			this.IsBurning.Set(this.m_CurrentStation.IsBurning.Get());
		}

		// Token: 0x04003775 RID: 14197
		public Value<bool> IsBurning = new Value<bool>(false);

		// Token: 0x04003776 RID: 14198
		[SerializeField]
		private SmeltingStationType m_Type;

		// Token: 0x04003777 RID: 14199
		[SerializeField]
		private ItemContainer m_LootContainer;

		// Token: 0x04003778 RID: 14200
		[SerializeField]
		private Image m_ProgressBar;

		// Token: 0x04003779 RID: 14201
		private SmeltingStation m_CurrentStation;

		// Token: 0x0400377A RID: 14202
		private ItemContainer m_Input;

		// Token: 0x0400377B RID: 14203
		private ItemContainer m_Fuel;
	}
}
