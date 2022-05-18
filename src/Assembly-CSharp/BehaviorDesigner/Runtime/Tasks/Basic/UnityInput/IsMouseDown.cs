using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015ED RID: 5613
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified mouse button is pressed.")]
	public class IsMouseDown : Conditional
	{
		// Token: 0x0600835B RID: 33627 RVA: 0x0005A51B File Offset: 0x0005871B
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetMouseButtonDown(this.buttonIndex.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600835C RID: 33628 RVA: 0x0005A532 File Offset: 0x00058732
		public override void OnReset()
		{
			this.buttonIndex = 0;
		}

		// Token: 0x0400701D RID: 28701
		[Tooltip("The button index")]
		public SharedInt buttonIndex;
	}
}
