using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x0200103F RID: 4159
	[TaskCategory("Basic/Time")]
	[TaskDescription("Returns the time in second since the start of the game.")]
	public class GetTime : Action
	{
		// Token: 0x06007215 RID: 29205 RVA: 0x002AD3EA File Offset: 0x002AB5EA
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.time;
			return 2;
		}

		// Token: 0x06007216 RID: 29206 RVA: 0x002AD3FD File Offset: 0x002AB5FD
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x04005DF3 RID: 24051
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
