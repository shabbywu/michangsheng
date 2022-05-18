using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001527 RID: 5415
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedVector3 variable to the specified object. Returns Success.")]
	public class SetSharedVector3 : Action
	{
		// Token: 0x0600809E RID: 32926 RVA: 0x0005788F File Offset: 0x00055A8F
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600809F RID: 32927 RVA: 0x000578A8 File Offset: 0x00055AA8
		public override void OnReset()
		{
			this.targetValue = Vector3.zero;
			this.targetVariable = Vector3.zero;
		}

		// Token: 0x04006D5B RID: 27995
		[Tooltip("The value to set the SharedVector3 to")]
		public SharedVector3 targetValue;

		// Token: 0x04006D5C RID: 27996
		[RequiredField]
		[Tooltip("The SharedVector3 to set")]
		public SharedVector3 targetVariable;
	}
}
