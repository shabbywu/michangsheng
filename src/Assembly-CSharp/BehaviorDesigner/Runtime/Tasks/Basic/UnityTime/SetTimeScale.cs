using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTime
{
	// Token: 0x020014FB RID: 5371
	[TaskCategory("Basic/Time")]
	[TaskDescription("Sets the scale at which time is passing.")]
	public class SetTimeScale : Action
	{
		// Token: 0x06008015 RID: 32789 RVA: 0x000570F1 File Offset: 0x000552F1
		public override TaskStatus OnUpdate()
		{
			Time.timeScale = this.timeScale.Value;
			return 2;
		}

		// Token: 0x06008016 RID: 32790 RVA: 0x00057104 File Offset: 0x00055304
		public override void OnReset()
		{
			this.timeScale.Value = 0f;
		}

		// Token: 0x04006CF5 RID: 27893
		[Tooltip("The timescale")]
		public SharedFloat timeScale;
	}
}
