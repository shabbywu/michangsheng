using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x0200103D RID: 4157
	[TaskCategory("Basic/Time")]
	[TaskDescription("Returns the time in seconds it took to complete the last frame.")]
	public class GetDeltaTime : Action
	{
		// Token: 0x0600720F RID: 29199 RVA: 0x002AD3A0 File Offset: 0x002AB5A0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.deltaTime;
			return 2;
		}

		// Token: 0x06007210 RID: 29200 RVA: 0x002AD3B3 File Offset: 0x002AB5B3
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x04005DF1 RID: 24049
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
