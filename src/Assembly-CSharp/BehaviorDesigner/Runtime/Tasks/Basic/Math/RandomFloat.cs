using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015C9 RID: 5577
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a random float value")]
	public class RandomFloat : Action
	{
		// Token: 0x060082DD RID: 33501 RVA: 0x002CE358 File Offset: 0x002CC558
		public override TaskStatus OnUpdate()
		{
			if (this.inclusive)
			{
				this.storeResult.Value = Random.Range(this.min.Value, this.max.Value);
			}
			else
			{
				this.storeResult.Value = Random.Range(this.min.Value, this.max.Value - 1E-05f);
			}
			return 2;
		}

		// Token: 0x060082DE RID: 33502 RVA: 0x00059CA8 File Offset: 0x00057EA8
		public override void OnReset()
		{
			this.min.Value = 0f;
			this.max.Value = 0f;
			this.inclusive = false;
			this.storeResult.Value = 0f;
		}

		// Token: 0x04006FB1 RID: 28593
		[Tooltip("The minimum amount")]
		public SharedFloat min;

		// Token: 0x04006FB2 RID: 28594
		[Tooltip("The maximum amount")]
		public SharedFloat max;

		// Token: 0x04006FB3 RID: 28595
		[Tooltip("Is the maximum value inclusive?")]
		public bool inclusive;

		// Token: 0x04006FB4 RID: 28596
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
