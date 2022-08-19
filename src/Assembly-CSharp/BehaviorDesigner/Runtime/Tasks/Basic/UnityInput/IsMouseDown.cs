using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x0200112E RID: 4398
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified mouse button is pressed.")]
	public class IsMouseDown : Conditional
	{
		// Token: 0x06007561 RID: 30049 RVA: 0x002B4550 File Offset: 0x002B2750
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetMouseButtonDown(this.buttonIndex.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007562 RID: 30050 RVA: 0x002B4567 File Offset: 0x002B2767
		public override void OnReset()
		{
			this.buttonIndex = 0;
		}

		// Token: 0x040060FA RID: 24826
		[Tooltip("The button index")]
		public SharedInt buttonIndex;
	}
}
