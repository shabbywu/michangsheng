using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200095D RID: 2397
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(Animator))]
	public class FlamesIcon : MonoBehaviour
	{
		// Token: 0x06003D3E RID: 15678 RVA: 0x001B3824 File Offset: 0x001B1A24
		private void Start()
		{
			this.m_Animator = base.GetComponent<Animator>();
			this.m_SmeltingStationGUI.IsBurning.AddChangeListener(new Action(this.OnChanged_SmeltingStation_IsBurning));
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryController_State));
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x001B3874 File Offset: 0x001B1A74
		private void OnChanged_InventoryController_State()
		{
			if (MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				this.m_Animator.SetBool("Loop", false);
				return;
			}
			if (this.m_SmeltingStationGUI.IsBurning.Get())
			{
				this.m_Animator.SetBool("Loop", true);
			}
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x0002C2A2 File Offset: 0x0002A4A2
		private void OnChanged_SmeltingStation_IsBurning()
		{
			this.m_Animator.SetBool("Loop", this.m_SmeltingStationGUI.IsBurning.Get());
		}

		// Token: 0x04003773 RID: 14195
		[SerializeField]
		private SmeltingStationGUI m_SmeltingStationGUI;

		// Token: 0x04003774 RID: 14196
		private Animator m_Animator;
	}
}
