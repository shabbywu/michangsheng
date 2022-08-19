using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x0200112F RID: 4399
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified mouse button is pressed.")]
	public class IsMouseUp : Conditional
	{
		// Token: 0x06007564 RID: 30052 RVA: 0x002B4575 File Offset: 0x002B2775
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetMouseButtonUp(this.buttonIndex.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007565 RID: 30053 RVA: 0x002B458C File Offset: 0x002B278C
		public override void OnReset()
		{
			this.buttonIndex = 0;
		}

		// Token: 0x040060FB RID: 24827
		[Tooltip("The button index")]
		public SharedInt buttonIndex;
	}
}
