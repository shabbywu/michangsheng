using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015E9 RID: 5609
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified button is pressed.")]
	public class IsButtonDown : Conditional
	{
		// Token: 0x0600834F RID: 33615 RVA: 0x0005A493 File Offset: 0x00058693
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetButtonDown(this.buttonName.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008350 RID: 33616 RVA: 0x0005A4AA File Offset: 0x000586AA
		public override void OnReset()
		{
			this.buttonName = "Fire1";
		}

		// Token: 0x04007019 RID: 28697
		[Tooltip("The name of the button")]
		public SharedString buttonName;
	}
}
