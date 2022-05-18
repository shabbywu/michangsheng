using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001526 RID: 5414
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedVector2 variable to the specified object. Returns Success.")]
	public class SetSharedVector2 : Action
	{
		// Token: 0x0600809B RID: 32923 RVA: 0x00057854 File Offset: 0x00055A54
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600809C RID: 32924 RVA: 0x0005786D File Offset: 0x00055A6D
		public override void OnReset()
		{
			this.targetValue = Vector2.zero;
			this.targetVariable = Vector2.zero;
		}

		// Token: 0x04006D59 RID: 27993
		[Tooltip("The value to set the SharedVector2 to")]
		public SharedVector2 targetValue;

		// Token: 0x04006D5A RID: 27994
		[RequiredField]
		[Tooltip("The SharedVector2 to set")]
		public SharedVector2 targetVariable;
	}
}
