using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200065B RID: 1627
	public class SmeltingStationGUI : MonoBehaviour
	{
		// Token: 0x060033B4 RID: 13236 RVA: 0x0016A924 File Offset: 0x00168B24
		private void Awake()
		{
			this.m_Input = base.transform.FindDeepChild("Input").GetComponent<ItemContainer>();
			this.m_Fuel = base.transform.FindDeepChild("Fuel").GetComponent<ItemContainer>();
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x0016A95C File Offset: 0x00168B5C
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

		// Token: 0x060033B6 RID: 13238 RVA: 0x0016A9D0 File Offset: 0x00168BD0
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

		// Token: 0x060033B7 RID: 13239 RVA: 0x0016AACA File Offset: 0x00168CCA
		private void OnChanged_Progress()
		{
			this.m_ProgressBar.fillAmount = this.m_CurrentStation.Progress.Get();
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x0016AAE8 File Offset: 0x00168CE8
		private void OnChanged_InventoryState()
		{
			if (this.m_CurrentStation && MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_CurrentStation.IsBurning.RemoveChangeListener(new Action(this.OnChanged_IsBurning));
				this.m_CurrentStation.Progress.RemoveChangeListener(new Action(this.OnChanged_Progress));
				this.m_CurrentStation = null;
			}
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x0016AB4D File Offset: 0x00168D4D
		private void OnChanged_IsBurning()
		{
			this.IsBurning.Set(this.m_CurrentStation.IsBurning.Get());
		}

		// Token: 0x04002DF0 RID: 11760
		public Value<bool> IsBurning = new Value<bool>(false);

		// Token: 0x04002DF1 RID: 11761
		[SerializeField]
		private SmeltingStationType m_Type;

		// Token: 0x04002DF2 RID: 11762
		[SerializeField]
		private ItemContainer m_LootContainer;

		// Token: 0x04002DF3 RID: 11763
		[SerializeField]
		private Image m_ProgressBar;

		// Token: 0x04002DF4 RID: 11764
		private SmeltingStation m_CurrentStation;

		// Token: 0x04002DF5 RID: 11765
		private ItemContainer m_Input;

		// Token: 0x04002DF6 RID: 11766
		private ItemContainer m_Fuel;
	}
}
