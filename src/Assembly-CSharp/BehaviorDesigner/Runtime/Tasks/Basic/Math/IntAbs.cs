using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015BE RID: 5566
	[TaskCategory("Basic/Math")]
	[TaskDescription("Stores the absolute value of the int.")]
	public class IntAbs : Action
	{
		// Token: 0x060082C2 RID: 33474 RVA: 0x00059B20 File Offset: 0x00057D20
		public override TaskStatus OnUpdate()
		{
			this.intVariable.Value = Mathf.Abs(this.intVariable.Value);
			return 2;
		}

		// Token: 0x060082C3 RID: 33475 RVA: 0x00059B3E File Offset: 0x00057D3E
		public override void OnReset()
		{
			this.intVariable = 0;
		}

		// Token: 0x04006F8C RID: 28556
		[Tooltip("The int to return the absolute value of")]
		public SharedInt intVariable;
	}
}
