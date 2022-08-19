using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200105E RID: 4190
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector4 : Conditional
	{
		// Token: 0x06007277 RID: 29303 RVA: 0x002AE0E8 File Offset: 0x002AC2E8
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007278 RID: 29304 RVA: 0x002AE118 File Offset: 0x002AC318
		public override void OnReset()
		{
			this.variable = Vector4.zero;
			this.compareTo = Vector4.zero;
		}

		// Token: 0x04005E3C RID: 24124
		[Tooltip("The first variable to compare")]
		public SharedVector4 variable;

		// Token: 0x04005E3D RID: 24125
		[Tooltip("The variable to compare to")]
		public SharedVector4 compareTo;
	}
}
