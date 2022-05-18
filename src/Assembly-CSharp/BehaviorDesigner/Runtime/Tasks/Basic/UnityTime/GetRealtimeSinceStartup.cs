using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x020014F8 RID: 5368
	[TaskCategory("Basic/Time")]
	[TaskDescription("Returns the real time in seconds since the game started.")]
	public class GetRealtimeSinceStartup : Action
	{
		// Token: 0x0600800C RID: 32780 RVA: 0x00057082 File Offset: 0x00055282
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.realtimeSinceStartup;
			return 2;
		}

		// Token: 0x0600800D RID: 32781 RVA: 0x00057095 File Offset: 0x00055295
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x04006CF2 RID: 27890
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
