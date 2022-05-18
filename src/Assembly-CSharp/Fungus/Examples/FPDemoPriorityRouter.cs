using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02001466 RID: 5222
	public class FPDemoPriorityRouter : MonoBehaviour
	{
		// Token: 0x06007DDD RID: 32221 RVA: 0x00055156 File Offset: 0x00053356
		private void OnEnable()
		{
			FungusPrioritySignals.OnFungusPriorityStart += this.FungusPrioritySignals_OnFungusPriorityStart;
			FungusPrioritySignals.OnFungusPriorityEnd += this.FungusPrioritySignals_OnFungusPriorityEnd;
		}

		// Token: 0x06007DDE RID: 32222 RVA: 0x0005517A File Offset: 0x0005337A
		private void OnDisable()
		{
			FungusPrioritySignals.OnFungusPriorityStart -= this.FungusPrioritySignals_OnFungusPriorityStart;
			FungusPrioritySignals.OnFungusPriorityEnd -= this.FungusPrioritySignals_OnFungusPriorityEnd;
		}

		// Token: 0x06007DDF RID: 32223 RVA: 0x002C7F64 File Offset: 0x002C6164
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

		// Token: 0x06007DE0 RID: 32224 RVA: 0x002C7FB0 File Offset: 0x002C61B0
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

		// Token: 0x06007DE1 RID: 32225 RVA: 0x000042DD File Offset: 0x000024DD
		private void Update()
		{
		}

		// Token: 0x04006B4F RID: 27471
		public Behaviour[] componentEnabledOutsideFungusPriority;

		// Token: 0x04006B50 RID: 27472
		public Behaviour[] componentEnabledInsideFungusPriority;
	}
}
