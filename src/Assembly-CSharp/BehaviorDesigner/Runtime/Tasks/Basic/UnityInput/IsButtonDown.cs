using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x0200112A RID: 4394
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified button is pressed.")]
	public class IsButtonDown : Conditional
	{
		// Token: 0x06007555 RID: 30037 RVA: 0x002B44C8 File Offset: 0x002B26C8
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetButtonDown(this.buttonName.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007556 RID: 30038 RVA: 0x002B44DF File Offset: 0x002B26DF
		public override void OnReset()
		{
			this.buttonName = "Fire1";
		}

		// Token: 0x040060F6 RID: 24822
		[Tooltip("The name of the button")]
		public SharedString buttonName;
	}
}
