using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015EB RID: 5611
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified key is pressed.")]
	public class IsKeyDown : Conditional
	{
		// Token: 0x06008355 RID: 33621 RVA: 0x0005A4E5 File Offset: 0x000586E5
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetKeyDown(this.key))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008356 RID: 33622 RVA: 0x0005A4F7 File Offset: 0x000586F7
		public override void OnReset()
		{
			this.key = 0;
		}

		// Token: 0x0400701B RID: 28699
		[Tooltip("The key to test")]
		public KeyCode key;
	}
}
