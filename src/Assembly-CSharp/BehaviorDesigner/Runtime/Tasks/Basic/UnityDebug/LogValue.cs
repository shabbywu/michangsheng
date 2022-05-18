using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityDebug
{
	// Token: 0x02001601 RID: 5633
	[TaskCategory("Basic/Debug")]
	[TaskDescription("Log a variable value.")]
	public class LogValue : Action
	{
		// Token: 0x06008399 RID: 33689 RVA: 0x0005A98F File Offset: 0x00058B8F
		public override TaskStatus OnUpdate()
		{
			Debug.Log(this.variable.Value.value.GetValue());
			return 2;
		}

		// Token: 0x0600839A RID: 33690 RVA: 0x0005A9AC File Offset: 0x00058BAC
		public override void OnReset()
		{
			this.variable = null;
		}

		// Token: 0x0400704C RID: 28748
		[Tooltip("The variable to output")]
		public SharedGenericVariable variable;
	}
}
