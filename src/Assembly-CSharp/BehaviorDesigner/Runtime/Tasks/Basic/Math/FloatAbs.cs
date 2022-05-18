using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015B8 RID: 5560
	[TaskCategory("Basic/Math")]
	[TaskDescription("Stores the absolute value of the float.")]
	public class FloatAbs : Action
	{
		// Token: 0x060082B6 RID: 33462 RVA: 0x00059A5A File Offset: 0x00057C5A
		public override TaskStatus OnUpdate()
		{
			this.floatVariable.Value = Mathf.Abs(this.floatVariable.Value);
			return 2;
		}

		// Token: 0x060082B7 RID: 33463 RVA: 0x00059A78 File Offset: 0x00057C78
		public override void OnReset()
		{
			this.floatVariable = 0f;
		}

		// Token: 0x04006F72 RID: 28530
		[Tooltip("The float to return the absolute value of")]
		public SharedFloat floatVariable;
	}
}
