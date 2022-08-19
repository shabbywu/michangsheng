using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x02001127 RID: 4391
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the pressed state of the specified key.")]
	public class GetKey : Action
	{
		// Token: 0x0600754C RID: 30028 RVA: 0x002B4438 File Offset: 0x002B2638
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetKey(this.key);
			return 2;
		}

		// Token: 0x0600754D RID: 30029 RVA: 0x002B4451 File Offset: 0x002B2651
		public override void OnReset()
		{
			this.key = 0;
			this.storeResult = false;
		}

		// Token: 0x040060F1 RID: 24817
		[Tooltip("The key to test.")]
		public KeyCode key;

		// Token: 0x040060F2 RID: 24818
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}
