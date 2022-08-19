using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001012 RID: 4114
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the right vector value.")]
	public class GetRightVector : Action
	{
		// Token: 0x0600716F RID: 29039 RVA: 0x002ABC76 File Offset: 0x002A9E76
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.right;
			return 2;
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x002ABC89 File Offset: 0x002A9E89
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04005D4B RID: 23883
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
