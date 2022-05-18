using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015E8 RID: 5608
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the mouse position.")]
	public class GetMousePosition : Action
	{
		// Token: 0x0600834C RID: 33612 RVA: 0x0005A469 File Offset: 0x00058669
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.mousePosition;
			return 2;
		}

		// Token: 0x0600834D RID: 33613 RVA: 0x0005A481 File Offset: 0x00058681
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04007018 RID: 28696
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedVector2 storeResult;
	}
}
