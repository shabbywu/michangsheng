using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015E6 RID: 5606
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the pressed state of the specified key.")]
	public class GetKey : Action
	{
		// Token: 0x06008346 RID: 33606 RVA: 0x0005A403 File Offset: 0x00058603
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetKey(this.key);
			return 2;
		}

		// Token: 0x06008347 RID: 33607 RVA: 0x0005A41C File Offset: 0x0005861C
		public override void OnReset()
		{
			this.key = 0;
			this.storeResult = false;
		}

		// Token: 0x04007014 RID: 28692
		[Tooltip("The key to test.")]
		public KeyCode key;

		// Token: 0x04007015 RID: 28693
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}
