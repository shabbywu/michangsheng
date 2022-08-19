using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020010FD RID: 4349
	[TaskCategory("Basic/Math")]
	[TaskDescription("Stores the absolute value of the float.")]
	public class FloatAbs : Action
	{
		// Token: 0x060074BC RID: 29884 RVA: 0x002B2F52 File Offset: 0x002B1152
		public override TaskStatus OnUpdate()
		{
			this.floatVariable.Value = Mathf.Abs(this.floatVariable.Value);
			return 2;
		}

		// Token: 0x060074BD RID: 29885 RVA: 0x002B2F70 File Offset: 0x002B1170
		public override void OnReset()
		{
			this.floatVariable = 0f;
		}

		// Token: 0x0400606D RID: 24685
		[Tooltip("The float to return the absolute value of")]
		public SharedFloat floatVariable;
	}
}
