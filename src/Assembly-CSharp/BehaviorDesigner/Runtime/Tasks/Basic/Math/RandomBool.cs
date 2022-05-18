using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015C8 RID: 5576
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a random bool value")]
	public class RandomBool : Action
	{
		// Token: 0x060082DA RID: 33498 RVA: 0x00059C80 File Offset: 0x00057E80
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = (Random.value < 0.5f);
			return 2;
		}

		// Token: 0x060082DB RID: 33499 RVA: 0x00059C9A File Offset: 0x00057E9A
		public override void OnReset()
		{
			this.storeResult.Value = false;
		}

		// Token: 0x04006FB0 RID: 28592
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;
	}
}
