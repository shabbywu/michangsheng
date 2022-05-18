using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x020014F9 RID: 5369
	[TaskCategory("Basic/Time")]
	[TaskDescription("Returns the time in second since the start of the game.")]
	public class GetTime : Action
	{
		// Token: 0x0600800F RID: 32783 RVA: 0x000570A7 File Offset: 0x000552A7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.time;
			return 2;
		}

		// Token: 0x06008010 RID: 32784 RVA: 0x000570BA File Offset: 0x000552BA
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x04006CF3 RID: 27891
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
