using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200106D RID: 4205
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedVector3 variable to the specified object. Returns Success.")]
	public class SetSharedVector3 : Action
	{
		// Token: 0x060072A4 RID: 29348 RVA: 0x002AE468 File Offset: 0x002AC668
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x060072A5 RID: 29349 RVA: 0x002AE481 File Offset: 0x002AC681
		public override void OnReset()
		{
			this.targetValue = Vector3.zero;
			this.targetVariable = Vector3.zero;
		}

		// Token: 0x04005E5B RID: 24155
		[Tooltip("The value to set the SharedVector3 to")]
		public SharedVector3 targetValue;

		// Token: 0x04005E5C RID: 24156
		[RequiredField]
		[Tooltip("The SharedVector3 to set")]
		public SharedVector3 targetVariable;
	}
}
