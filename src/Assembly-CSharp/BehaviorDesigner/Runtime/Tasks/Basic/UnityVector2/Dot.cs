using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014C9 RID: 5321
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the dot product of two Vector2 values.")]
	public class Dot : Action
	{
		// Token: 0x06007F63 RID: 32611 RVA: 0x00056545 File Offset: 0x00054745
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Dot(this.leftHandSide.Value, this.rightHandSide.Value);
			return 2;
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x002CA020 File Offset: 0x002C8220
		public override void OnReset()
		{
			this.leftHandSide = (this.rightHandSide = Vector2.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04006C42 RID: 27714
		[Tooltip("The left hand side of the dot product")]
		public SharedVector2 leftHandSide;

		// Token: 0x04006C43 RID: 27715
		[Tooltip("The right hand side of the dot product")]
		public SharedVector2 rightHandSide;

		// Token: 0x04006C44 RID: 27716
		[Tooltip("The dot product result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
