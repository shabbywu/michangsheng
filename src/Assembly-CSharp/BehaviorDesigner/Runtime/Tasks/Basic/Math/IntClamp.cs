using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001102 RID: 4354
	[TaskCategory("Basic/Math")]
	[TaskDescription("Clamps the int between two values.")]
	public class IntClamp : Action
	{
		// Token: 0x060074CB RID: 29899 RVA: 0x002B32A0 File Offset: 0x002B14A0
		public override TaskStatus OnUpdate()
		{
			this.intVariable.Value = Mathf.Clamp(this.intVariable.Value, this.minValue.Value, this.maxValue.Value);
			return 2;
		}

		// Token: 0x060074CC RID: 29900 RVA: 0x002B32D4 File Offset: 0x002B14D4
		public override void OnReset()
		{
			this.intVariable = (this.minValue = (this.maxValue = 0));
		}

		// Token: 0x04006079 RID: 24697
		[Tooltip("The int to clamp")]
		public SharedInt intVariable;

		// Token: 0x0400607A RID: 24698
		[Tooltip("The maximum value of the int")]
		public SharedInt minValue;

		// Token: 0x0400607B RID: 24699
		[Tooltip("The maximum value of the int")]
		public SharedInt maxValue;
	}
}
