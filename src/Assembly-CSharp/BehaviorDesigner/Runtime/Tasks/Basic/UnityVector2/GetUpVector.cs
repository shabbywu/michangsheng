using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014CD RID: 5325
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the up vector value.")]
	public class GetUpVector : Action
	{
		// Token: 0x06007F6F RID: 32623 RVA: 0x000565D7 File Offset: 0x000547D7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.up;
			return 2;
		}

		// Token: 0x06007F70 RID: 32624 RVA: 0x000565EA File Offset: 0x000547EA
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04006C4A RID: 27722
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
