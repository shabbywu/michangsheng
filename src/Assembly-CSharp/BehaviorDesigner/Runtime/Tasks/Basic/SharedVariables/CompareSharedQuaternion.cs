using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001511 RID: 5393
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedQuaternion : Conditional
	{
		// Token: 0x0600805C RID: 32860 RVA: 0x002CB280 File Offset: 0x002C9480
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x000574F9 File Offset: 0x000556F9
		public override void OnReset()
		{
			this.variable = Quaternion.identity;
			this.compareTo = Quaternion.identity;
		}

		// Token: 0x04006D2E RID: 27950
		[Tooltip("The first variable to compare")]
		public SharedQuaternion variable;

		// Token: 0x04006D2F RID: 27951
		[Tooltip("The variable to compare to")]
		public SharedQuaternion compareTo;
	}
}
