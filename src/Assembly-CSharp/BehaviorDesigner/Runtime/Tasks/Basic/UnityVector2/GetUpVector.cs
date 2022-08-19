using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001014 RID: 4116
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the up vector value.")]
	public class GetUpVector : Action
	{
		// Token: 0x06007175 RID: 29045 RVA: 0x002ABCEA File Offset: 0x002A9EEA
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.up;
			return 2;
		}

		// Token: 0x06007176 RID: 29046 RVA: 0x002ABCFD File Offset: 0x002A9EFD
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04005D4E RID: 23886
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
