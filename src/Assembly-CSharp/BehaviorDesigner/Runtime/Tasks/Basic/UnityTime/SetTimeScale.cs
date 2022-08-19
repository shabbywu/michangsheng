using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x02001041 RID: 4161
	[TaskCategory("Basic/Time")]
	[TaskDescription("Sets the scale at which time is passing.")]
	public class SetTimeScale : Action
	{
		// Token: 0x0600721B RID: 29211 RVA: 0x002AD434 File Offset: 0x002AB634
		public override TaskStatus OnUpdate()
		{
			Time.timeScale = this.timeScale.Value;
			return 2;
		}

		// Token: 0x0600721C RID: 29212 RVA: 0x002AD447 File Offset: 0x002AB647
		public override void OnReset()
		{
			this.timeScale.Value = 0f;
		}

		// Token: 0x04005DF5 RID: 24053
		[Tooltip("The timescale")]
		public SharedFloat timeScale;
	}
}
