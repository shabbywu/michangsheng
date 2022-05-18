using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014CB RID: 5323
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the right vector value.")]
	public class GetRightVector : Action
	{
		// Token: 0x06007F69 RID: 32617 RVA: 0x00056590 File Offset: 0x00054790
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.right;
			return 2;
		}

		// Token: 0x06007F6A RID: 32618 RVA: 0x000565A3 File Offset: 0x000547A3
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04006C47 RID: 27719
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
