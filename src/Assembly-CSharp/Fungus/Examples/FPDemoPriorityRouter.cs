using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02000FAE RID: 4014
	public class FPDemoPriorityRouter : MonoBehaviour
	{
		// Token: 0x06006FE3 RID: 28643 RVA: 0x002A871C File Offset: 0x002A691C
		private void OnEnable()
		{
			FungusPrioritySignals.OnFungusPriorityStart += this.FungusPrioritySignals_OnFungusPriorityStart;
			FungusPrioritySignals.OnFungusPriorityEnd += this.FungusPrioritySignals_OnFungusPriorityEnd;
		}

		// Token: 0x06006FE4 RID: 28644 RVA: 0x002A8740 File Offset: 0x002A6940
		private void OnDisable()
		{
			FungusPrioritySignals.OnFungusPriorityStart -= this.FungusPrioritySignals_OnFungusPriorityStart;
			FungusPrioritySignals.OnFungusPriorityEnd -= this.FungusPrioritySignals_OnFungusPriorityEnd;
		}

		// Token: 0x06006FE5 RID: 28645 RVA: 0x002A8764 File Offset: 0x002A6964
		private void FungusPrioritySignals_OnFungusPriorityEnd()
		{
			Behaviour[] array = this.componentEnabledOutsideFungusPriority;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
			array = this.componentEnabledInsideFungusPriority;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}

		// Token: 0x06006FE6 RID: 28646 RVA: 0x002A87B0 File Offset: 0x002A69B0
		private void FungusPrioritySignals_OnFungusPriorityStart()
		{
			Behaviour[] array = this.componentEnabledOutsideFungusPriority;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			array = this.componentEnabledInsideFungusPriority;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}

		// Token: 0x06006FE7 RID: 28647 RVA: 0x00004095 File Offset: 0x00002295
		private void Update()
		{
		}

		// Token: 0x04005C57 RID: 23639
		public Behaviour[] componentEnabledOutsideFungusPriority;

		// Token: 0x04005C58 RID: 23640
		public Behaviour[] componentEnabledInsideFungusPriority;
	}
}
