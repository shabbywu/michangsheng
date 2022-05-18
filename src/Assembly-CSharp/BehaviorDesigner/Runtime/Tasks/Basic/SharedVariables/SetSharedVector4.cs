using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001528 RID: 5416
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedVector4 variable to the specified object. Returns Success.")]
	public class SetSharedVector4 : Action
	{
		// Token: 0x060080A1 RID: 32929 RVA: 0x000578CA File Offset: 0x00055ACA
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x060080A2 RID: 32930 RVA: 0x000578E3 File Offset: 0x00055AE3
		public override void OnReset()
		{
			this.targetValue = Vector4.zero;
			this.targetVariable = Vector4.zero;
		}

		// Token: 0x04006D5D RID: 27997
		[Tooltip("The value to set the SharedVector4 to")]
		public SharedVector4 targetValue;

		// Token: 0x04006D5E RID: 27998
		[RequiredField]
		[Tooltip("The SharedVector4 to set")]
		public SharedVector4 targetVariable;
	}
}
