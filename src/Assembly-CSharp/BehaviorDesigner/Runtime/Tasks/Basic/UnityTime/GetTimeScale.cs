using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x02001040 RID: 4160
	[TaskCategory("Basic/Time")]
	[TaskDescription("Returns the scale at which time is passing.")]
	public class GetTimeScale : Action
	{
		// Token: 0x06007218 RID: 29208 RVA: 0x002AD40F File Offset: 0x002AB60F
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.timeScale;
			return 2;
		}

		// Token: 0x06007219 RID: 29209 RVA: 0x002AD422 File Offset: 0x002AB622
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x04005DF4 RID: 24052
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
