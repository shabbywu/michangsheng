using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x0200103E RID: 4158
	[TaskCategory("Basic/Time")]
	[TaskDescription("Returns the real time in seconds since the game started.")]
	public class GetRealtimeSinceStartup : Action
	{
		// Token: 0x06007212 RID: 29202 RVA: 0x002AD3C5 File Offset: 0x002AB5C5
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.realtimeSinceStartup;
			return 2;
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x002AD3D8 File Offset: 0x002AB5D8
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x04005DF2 RID: 24050
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
