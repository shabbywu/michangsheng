using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x020014FA RID: 5370
	[TaskCategory("Basic/Time")]
	[TaskDescription("Returns the scale at which time is passing.")]
	public class GetTimeScale : Action
	{
		// Token: 0x06008012 RID: 32786 RVA: 0x000570CC File Offset: 0x000552CC
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.timeScale;
			return 2;
		}

		// Token: 0x06008013 RID: 32787 RVA: 0x000570DF File Offset: 0x000552DF
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x04006CF4 RID: 27892
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
