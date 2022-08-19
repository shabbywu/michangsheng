using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001101 RID: 4353
	[TaskCategory("Basic/Math")]
	[TaskDescription("Stores the absolute value of the int.")]
	public class IntAbs : Action
	{
		// Token: 0x060074C8 RID: 29896 RVA: 0x002B3274 File Offset: 0x002B1474
		public override TaskStatus OnUpdate()
		{
			this.intVariable.Value = Mathf.Abs(this.intVariable.Value);
			return 2;
		}

		// Token: 0x060074C9 RID: 29897 RVA: 0x002B3292 File Offset: 0x002B1492
		public override void OnReset()
		{
			this.intVariable = 0;
		}

		// Token: 0x04006078 RID: 24696
		[Tooltip("The int to return the absolute value of")]
		public SharedInt intVariable;
	}
}
