using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001057 RID: 4183
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedQuaternion : Conditional
	{
		// Token: 0x06007262 RID: 29282 RVA: 0x002ADDF0 File Offset: 0x002ABFF0
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007263 RID: 29283 RVA: 0x002ADE20 File Offset: 0x002AC020
		public override void OnReset()
		{
			this.variable = Quaternion.identity;
			this.compareTo = Quaternion.identity;
		}

		// Token: 0x04005E2E RID: 24110
		[Tooltip("The first variable to compare")]
		public SharedQuaternion variable;

		// Token: 0x04005E2F RID: 24111
		[Tooltip("The variable to compare to")]
		public SharedQuaternion compareTo;
	}
}
