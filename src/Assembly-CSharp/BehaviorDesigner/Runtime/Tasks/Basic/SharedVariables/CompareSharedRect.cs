using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001512 RID: 5394
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedRect : Conditional
	{
		// Token: 0x0600805F RID: 32863 RVA: 0x002CB2B0 File Offset: 0x002C94B0
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008060 RID: 32864 RVA: 0x002CB2E0 File Offset: 0x002C94E0
		public override void OnReset()
		{
			this.variable = default(Rect);
			this.compareTo = default(Rect);
		}

		// Token: 0x04006D30 RID: 27952
		[Tooltip("The first variable to compare")]
		public SharedRect variable;

		// Token: 0x04006D31 RID: 27953
		[Tooltip("The variable to compare to")]
		public SharedRect compareTo;
	}
}
