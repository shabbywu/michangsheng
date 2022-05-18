using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001521 RID: 5409
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedQuaternion variable to the specified object. Returns Success.")]
	public class SetSharedQuaternion : Action
	{
		// Token: 0x0600808C RID: 32908 RVA: 0x00057758 File Offset: 0x00055958
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600808D RID: 32909 RVA: 0x00057771 File Offset: 0x00055971
		public override void OnReset()
		{
			this.targetValue = Quaternion.identity;
			this.targetVariable = Quaternion.identity;
		}

		// Token: 0x04006D4F RID: 27983
		[Tooltip("The value to set the SharedQuaternion to")]
		public SharedQuaternion targetValue;

		// Token: 0x04006D50 RID: 27984
		[RequiredField]
		[Tooltip("The SharedQuaternion to set")]
		public SharedQuaternion targetVariable;
	}
}
