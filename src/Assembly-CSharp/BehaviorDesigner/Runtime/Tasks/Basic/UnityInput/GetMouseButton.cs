using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015E7 RID: 5607
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the state of the specified mouse button.")]
	public class GetMouseButton : Action
	{
		// Token: 0x06008349 RID: 33609 RVA: 0x0005A431 File Offset: 0x00058631
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetMouseButton(this.buttonIndex.Value);
			return 2;
		}

		// Token: 0x0600834A RID: 33610 RVA: 0x0005A44F File Offset: 0x0005864F
		public override void OnReset()
		{
			this.buttonIndex = 0;
			this.storeResult = false;
		}

		// Token: 0x04007016 RID: 28694
		[Tooltip("The index of the button")]
		public SharedInt buttonIndex;

		// Token: 0x04007017 RID: 28695
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}
