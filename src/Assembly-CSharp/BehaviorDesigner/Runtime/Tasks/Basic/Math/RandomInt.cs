using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015CA RID: 5578
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a random int value")]
	public class RandomInt : Action
	{
		// Token: 0x060082E0 RID: 33504 RVA: 0x002CE3C4 File Offset: 0x002CC5C4
		public override TaskStatus OnUpdate()
		{
			if (this.inclusive)
			{
				this.storeResult.Value = Random.Range(this.min.Value, this.max.Value + 1);
			}
			else
			{
				this.storeResult.Value = Random.Range(this.min.Value, this.max.Value);
			}
			return 2;
		}

		// Token: 0x060082E1 RID: 33505 RVA: 0x00059CE1 File Offset: 0x00057EE1
		public override void OnReset()
		{
			this.min.Value = 0;
			this.max.Value = 0;
			this.inclusive = false;
			this.storeResult.Value = 0;
		}

		// Token: 0x04006FB5 RID: 28597
		[Tooltip("The minimum amount")]
		public SharedInt min;

		// Token: 0x04006FB6 RID: 28598
		[Tooltip("The maximum amount")]
		public SharedInt max;

		// Token: 0x04006FB7 RID: 28599
		[Tooltip("Is the maximum value inclusive?")]
		public bool inclusive;

		// Token: 0x04006FB8 RID: 28600
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;
	}
}
