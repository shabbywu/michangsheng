using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014BC RID: 5308
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the Vector2 value of the Vector3.")]
	public class GetVector2 : Action
	{
		// Token: 0x06007F3F RID: 32575 RVA: 0x000563E5 File Offset: 0x000545E5
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value;
			return 2;
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x00056403 File Offset: 0x00054603
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04006C16 RID: 27670
		[Tooltip("The Vector3 to get the Vector2 value of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04006C17 RID: 27671
		[Tooltip("The Vector2 value")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
