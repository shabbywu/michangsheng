using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001050 RID: 4176
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedColor : Conditional
	{
		// Token: 0x0600724D RID: 29261 RVA: 0x002ADA5C File Offset: 0x002ABC5C
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600724E RID: 29262 RVA: 0x002ADA8C File Offset: 0x002ABC8C
		public override void OnReset()
		{
			this.variable = Color.black;
			this.compareTo = Color.black;
		}

		// Token: 0x04005E20 RID: 24096
		[Tooltip("The first variable to compare")]
		public SharedColor variable;

		// Token: 0x04005E21 RID: 24097
		[Tooltip("The variable to compare to")]
		public SharedColor compareTo;
	}
}
