using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200105D RID: 4189
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector3 : Conditional
	{
		// Token: 0x06007274 RID: 29300 RVA: 0x002AE094 File Offset: 0x002AC294
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007275 RID: 29301 RVA: 0x002AE0C4 File Offset: 0x002AC2C4
		public override void OnReset()
		{
			this.variable = Vector3.zero;
			this.compareTo = Vector3.zero;
		}

		// Token: 0x04005E3A RID: 24122
		[Tooltip("The first variable to compare")]
		public SharedVector3 variable;

		// Token: 0x04005E3B RID: 24123
		[Tooltip("The variable to compare to")]
		public SharedVector3 compareTo;
	}
}
