using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x0200112C RID: 4396
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified key is pressed.")]
	public class IsKeyDown : Conditional
	{
		// Token: 0x0600755B RID: 30043 RVA: 0x002B451A File Offset: 0x002B271A
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetKeyDown(this.key))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600755C RID: 30044 RVA: 0x002B452C File Offset: 0x002B272C
		public override void OnReset()
		{
			this.key = 0;
		}

		// Token: 0x040060F8 RID: 24824
		[Tooltip("The key to test")]
		public KeyCode key;
	}
}
