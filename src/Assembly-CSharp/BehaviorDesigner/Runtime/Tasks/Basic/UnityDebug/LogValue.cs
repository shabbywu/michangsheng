using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityDebug
{
	// Token: 0x02001142 RID: 4418
	[TaskCategory("Basic/Debug")]
	[TaskDescription("Log a variable value.")]
	public class LogValue : Action
	{
		// Token: 0x0600759F RID: 30111 RVA: 0x002B4C5D File Offset: 0x002B2E5D
		public override TaskStatus OnUpdate()
		{
			Debug.Log(this.variable.Value.value.GetValue());
			return 2;
		}

		// Token: 0x060075A0 RID: 30112 RVA: 0x002B4C7A File Offset: 0x002B2E7A
		public override void OnReset()
		{
			this.variable = null;
		}

		// Token: 0x04006129 RID: 24873
		[Tooltip("The variable to output")]
		public SharedGenericVariable variable;
	}
}
