using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200105C RID: 4188
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector2 : Conditional
	{
		// Token: 0x06007271 RID: 29297 RVA: 0x002AE040 File Offset: 0x002AC240
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007272 RID: 29298 RVA: 0x002AE070 File Offset: 0x002AC270
		public override void OnReset()
		{
			this.variable = Vector2.zero;
			this.compareTo = Vector2.zero;
		}

		// Token: 0x04005E38 RID: 24120
		[Tooltip("The first variable to compare")]
		public SharedVector2 variable;

		// Token: 0x04005E39 RID: 24121
		[Tooltip("The variable to compare to")]
		public SharedVector2 compareTo;
	}
}
