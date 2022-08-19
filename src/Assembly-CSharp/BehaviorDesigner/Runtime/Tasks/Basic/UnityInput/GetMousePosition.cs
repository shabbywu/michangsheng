using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x02001129 RID: 4393
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the mouse position.")]
	public class GetMousePosition : Action
	{
		// Token: 0x06007552 RID: 30034 RVA: 0x002B449E File Offset: 0x002B269E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.mousePosition;
			return 2;
		}

		// Token: 0x06007553 RID: 30035 RVA: 0x002B44B6 File Offset: 0x002B26B6
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x040060F5 RID: 24821
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedVector2 storeResult;
	}
}
