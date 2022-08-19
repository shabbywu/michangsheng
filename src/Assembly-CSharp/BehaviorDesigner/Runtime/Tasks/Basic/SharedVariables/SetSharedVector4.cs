using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200106E RID: 4206
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedVector4 variable to the specified object. Returns Success.")]
	public class SetSharedVector4 : Action
	{
		// Token: 0x060072A7 RID: 29351 RVA: 0x002AE4A3 File Offset: 0x002AC6A3
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x060072A8 RID: 29352 RVA: 0x002AE4BC File Offset: 0x002AC6BC
		public override void OnReset()
		{
			this.targetValue = Vector4.zero;
			this.targetVariable = Vector4.zero;
		}

		// Token: 0x04005E5D RID: 24157
		[Tooltip("The value to set the SharedVector4 to")]
		public SharedVector4 targetValue;

		// Token: 0x04005E5E RID: 24158
		[RequiredField]
		[Tooltip("The SharedVector4 to set")]
		public SharedVector4 targetVariable;
	}
}
