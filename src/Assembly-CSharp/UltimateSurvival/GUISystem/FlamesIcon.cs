using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200065A RID: 1626
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(Animator))]
	public class FlamesIcon : MonoBehaviour
	{
		// Token: 0x060033B0 RID: 13232 RVA: 0x0016A864 File Offset: 0x00168A64
		private void Start()
		{
			this.m_Animator = base.GetComponent<Animator>();
			this.m_SmeltingStationGUI.IsBurning.AddChangeListener(new Action(this.OnChanged_SmeltingStation_IsBurning));
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryController_State));
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x0016A8B4 File Offset: 0x00168AB4
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

		// Token: 0x060033B2 RID: 13234 RVA: 0x0016A902 File Offset: 0x00168B02
		private void OnChanged_SmeltingStation_IsBurning()
		{
			this.m_Animator.SetBool("Loop", this.m_SmeltingStationGUI.IsBurning.Get());
		}

		// Token: 0x04002DEE RID: 11758
		[SerializeField]
		private SmeltingStationGUI m_SmeltingStationGUI;

		// Token: 0x04002DEF RID: 11759
		private Animator m_Animator;
	}
}
