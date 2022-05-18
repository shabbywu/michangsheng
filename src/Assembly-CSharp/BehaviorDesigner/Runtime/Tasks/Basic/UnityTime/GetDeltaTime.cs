using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x020014F7 RID: 5367
	[TaskCategory("Basic/Time")]
	[TaskDescription("Returns the time in seconds it took to complete the last frame.")]
	public class GetDeltaTime : Action
	{
		// Token: 0x06008009 RID: 32777 RVA: 0x0005705D File Offset: 0x0005525D
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.deltaTime;
			return 2;
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x00057070 File Offset: 0x00055270
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x04006CF1 RID: 27889
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
