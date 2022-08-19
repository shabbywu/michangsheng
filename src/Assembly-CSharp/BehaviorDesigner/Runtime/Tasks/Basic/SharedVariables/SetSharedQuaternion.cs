using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001067 RID: 4199
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedQuaternion variable to the specified object. Returns Success.")]
	public class SetSharedQuaternion : Action
	{
		// Token: 0x06007292 RID: 29330 RVA: 0x002AE2FA File Offset: 0x002AC4FA
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06007293 RID: 29331 RVA: 0x002AE313 File Offset: 0x002AC513
		public override void OnReset()
		{
			this.targetValue = Quaternion.identity;
			this.targetVariable = Quaternion.identity;
		}

		// Token: 0x04005E4F RID: 24143
		[Tooltip("The value to set the SharedQuaternion to")]
		public SharedQuaternion targetValue;

		// Token: 0x04005E50 RID: 24144
		[RequiredField]
		[Tooltip("The SharedQuaternion to set")]
		public SharedQuaternion targetVariable;
	}
}
