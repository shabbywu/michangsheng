using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x0200110A RID: 4362
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a random float value")]
	public class RandomFloat : Action
	{
		// Token: 0x060074E3 RID: 29923 RVA: 0x002B36C4 File Offset: 0x002B18C4
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

		// Token: 0x060074E4 RID: 29924 RVA: 0x002B372E File Offset: 0x002B192E
		public override void OnReset()
		{
			this.min.Value = 0f;
			this.max.Value = 0f;
			this.inclusive = false;
			this.storeResult.Value = 0f;
		}

		// Token: 0x0400608E RID: 24718
		[Tooltip("The minimum amount")]
		public SharedFloat min;

		// Token: 0x0400608F RID: 24719
		[Tooltip("The maximum amount")]
		public SharedFloat max;

		// Token: 0x04006090 RID: 24720
		[Tooltip("Is the maximum value inclusive?")]
		public bool inclusive;

		// Token: 0x04006091 RID: 24721
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
