using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015EE RID: 5614
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified mouse button is pressed.")]
	public class IsMouseUp : Conditional
	{
		// Token: 0x0600835E RID: 33630 RVA: 0x0005A540 File Offset: 0x00058740
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetMouseButtonUp(this.buttonIndex.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600835F RID: 33631 RVA: 0x0005A557 File Offset: 0x00058757
		public override void OnReset()
		{
			this.buttonIndex = 0;
		}

		// Token: 0x0400701E RID: 28702
		[Tooltip("The button index")]
		public SharedInt buttonIndex;
	}
}
