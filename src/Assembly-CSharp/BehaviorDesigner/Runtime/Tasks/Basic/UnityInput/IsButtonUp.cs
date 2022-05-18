using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015EA RID: 5610
	[TaskCategory("Basic/Input")]
	[TaskDescription("Returns success when the specified button is released.")]
	public class IsButtonUp : Conditional
	{
		// Token: 0x06008352 RID: 33618 RVA: 0x0005A4BC File Offset: 0x000586BC
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetButtonUp(this.buttonName.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008353 RID: 33619 RVA: 0x0005A4D3 File Offset: 0x000586D3
		public override void OnReset()
		{
			this.buttonName = "Fire1";
		}

		// Token: 0x0400701A RID: 28698
		[Tooltip("The name of the button")]
		public SharedString buttonName;
	}
}
