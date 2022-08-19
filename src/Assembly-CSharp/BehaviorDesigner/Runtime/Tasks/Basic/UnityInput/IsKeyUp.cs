using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x0200112D RID: 4397
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified key is released.")]
	public class IsKeyUp : Conditional
	{
		// Token: 0x0600755E RID: 30046 RVA: 0x002B4535 File Offset: 0x002B2735
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetKeyUp(this.key))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600755F RID: 30047 RVA: 0x002B4547 File Offset: 0x002B2747
		public override void OnReset()
		{
			this.key = 0;
		}

		// Token: 0x040060F9 RID: 24825
		[Tooltip("The key to test")]
		public KeyCode key;
	}
}
