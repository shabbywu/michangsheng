using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x02001128 RID: 4392
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the state of the specified mouse button.")]
	public class GetMouseButton : Action
	{
		// Token: 0x0600754F RID: 30031 RVA: 0x002B4466 File Offset: 0x002B2666
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetMouseButton(this.buttonIndex.Value);
			return 2;
		}

		// Token: 0x06007550 RID: 30032 RVA: 0x002B4484 File Offset: 0x002B2684
		public override void OnReset()
		{
			this.buttonIndex = 0;
			this.storeResult = false;
		}

		// Token: 0x040060F3 RID: 24819
		[Tooltip("The index of the button")]
		public SharedInt buttonIndex;

		// Token: 0x040060F4 RID: 24820
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}
