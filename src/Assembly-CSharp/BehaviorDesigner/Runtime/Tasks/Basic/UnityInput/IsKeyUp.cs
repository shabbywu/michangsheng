using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015EC RID: 5612
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified key is released.")]
	public class IsKeyUp : Conditional
	{
		// Token: 0x06008358 RID: 33624 RVA: 0x0005A500 File Offset: 0x00058700
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetKeyUp(this.key))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008359 RID: 33625 RVA: 0x0005A512 File Offset: 0x00058712
		public override void OnReset()
		{
			this.key = 0;
		}

		// Token: 0x0400701C RID: 28700
		[Tooltip("The key to test")]
		public KeyCode key;
	}
}
