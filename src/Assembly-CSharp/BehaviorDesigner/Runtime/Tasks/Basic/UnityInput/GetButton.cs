using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x02001126 RID: 4390
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the state of the specified button.")]
	public class GetButton : Action
	{
		// Token: 0x06007549 RID: 30025 RVA: 0x002B43FC File Offset: 0x002B25FC
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetButton(this.buttonName.Value);
			return 2;
		}

		// Token: 0x0600754A RID: 30026 RVA: 0x002B441A File Offset: 0x002B261A
		public override void OnReset()
		{
			this.buttonName = "Fire1";
			this.storeResult = false;
		}

		// Token: 0x040060EF RID: 24815
		[Tooltip("The name of the button")]
		public SharedString buttonName;

		// Token: 0x040060F0 RID: 24816
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}
