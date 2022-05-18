using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001516 RID: 5398
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector2 : Conditional
	{
		// Token: 0x0600806B RID: 32875 RVA: 0x002CB448 File Offset: 0x002C9648
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600806C RID: 32876 RVA: 0x0005757F File Offset: 0x0005577F
		public override void OnReset()
		{
			this.variable = Vector2.zero;
			this.compareTo = Vector2.zero;
		}

		// Token: 0x04006D38 RID: 27960
		[Tooltip("The first variable to compare")]
		public SharedVector2 variable;

		// Token: 0x04006D39 RID: 27961
		[Tooltip("The variable to compare to")]
		public SharedVector2 compareTo;
	}
}
