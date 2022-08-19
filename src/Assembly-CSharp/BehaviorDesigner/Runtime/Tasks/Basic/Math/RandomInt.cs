using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x0200110B RID: 4363
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a random int value")]
	public class RandomInt : Action
	{
		// Token: 0x060074E6 RID: 29926 RVA: 0x002B3768 File Offset: 0x002B1968
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

		// Token: 0x060074E7 RID: 29927 RVA: 0x002B37CE File Offset: 0x002B19CE
		public override void OnReset()
		{
			this.min.Value = 0;
			this.max.Value = 0;
			this.inclusive = false;
			this.storeResult.Value = 0;
		}

		// Token: 0x04006092 RID: 24722
		[Tooltip("The minimum amount")]
		public SharedInt min;

		// Token: 0x04006093 RID: 24723
		[Tooltip("The maximum amount")]
		public SharedInt max;

		// Token: 0x04006094 RID: 24724
		[Tooltip("Is the maximum value inclusive?")]
		public bool inclusive;

		// Token: 0x04006095 RID: 24725
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;
	}
}
