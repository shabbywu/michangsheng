using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001517 RID: 5399
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector3 : Conditional
	{
		// Token: 0x0600806E RID: 32878 RVA: 0x002CB478 File Offset: 0x002C9678
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600806F RID: 32879 RVA: 0x000575A1 File Offset: 0x000557A1
		public override void OnReset()
		{
			this.variable = Vector3.zero;
			this.compareTo = Vector3.zero;
		}

		// Token: 0x04006D3A RID: 27962
		[Tooltip("The first variable to compare")]
		public SharedVector3 variable;

		// Token: 0x04006D3B RID: 27963
		[Tooltip("The variable to compare to")]
		public SharedVector3 compareTo;
	}
}
