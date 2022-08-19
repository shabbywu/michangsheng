using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x0200112B RID: 4395
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified button is released.")]
	public class IsButtonUp : Conditional
	{
		// Token: 0x06007558 RID: 30040 RVA: 0x002B44F1 File Offset: 0x002B26F1
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetButtonUp(this.buttonName.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007559 RID: 30041 RVA: 0x002B4508 File Offset: 0x002B2708
		public override void OnReset()
		{
			this.buttonName = "Fire1";
		}

		// Token: 0x040060F7 RID: 24823
		[Tooltip("The name of the button")]
		public SharedString buttonName;
	}
}
