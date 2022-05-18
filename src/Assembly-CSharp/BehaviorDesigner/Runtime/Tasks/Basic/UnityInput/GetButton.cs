using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015E5 RID: 5605
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the state of the specified button.")]
	public class GetButton : Action
	{
		// Token: 0x06008343 RID: 33603 RVA: 0x0005A3C7 File Offset: 0x000585C7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetButton(this.buttonName.Value);
			return 2;
		}

		// Token: 0x06008344 RID: 33604 RVA: 0x0005A3E5 File Offset: 0x000585E5
		public override void OnReset()
		{
			this.buttonName = "Fire1";
			this.storeResult = false;
		}

		// Token: 0x04007012 RID: 28690
		[Tooltip("The name of the button")]
		public SharedString buttonName;

		// Token: 0x04007013 RID: 28691
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}
