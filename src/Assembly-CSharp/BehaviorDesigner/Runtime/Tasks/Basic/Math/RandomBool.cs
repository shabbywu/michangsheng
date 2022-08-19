using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001109 RID: 4361
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a random bool value")]
	public class RandomBool : Action
	{
		// Token: 0x060074E0 RID: 29920 RVA: 0x002B369C File Offset: 0x002B189C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = (Random.value < 0.5f);
			return 2;
		}

		// Token: 0x060074E1 RID: 29921 RVA: 0x002B36B6 File Offset: 0x002B18B6
		public override void OnReset()
		{
			this.storeResult.Value = false;
		}

		// Token: 0x0400608D RID: 24717
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;
	}
}
