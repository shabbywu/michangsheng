using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02000FFE RID: 4094
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the dot product of two Vector3 values.")]
	public class Dot : Action
	{
		// Token: 0x06007133 RID: 28979 RVA: 0x002AB4A6 File Offset: 0x002A96A6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Dot(this.leftHandSide.Value, this.rightHandSide.Value);
			return 2;
		}

		// Token: 0x06007134 RID: 28980 RVA: 0x002AB4D0 File Offset: 0x002A96D0
		public override void OnReset()
		{
			this.leftHandSide = (this.rightHandSide = Vector3.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04005D14 RID: 23828
		[Tooltip("The left hand side of the dot product")]
		public SharedVector3 leftHandSide;

		// Token: 0x04005D15 RID: 23829
		[Tooltip("The right hand side of the dot product")]
		public SharedVector3 rightHandSide;

		// Token: 0x04005D16 RID: 23830
		[Tooltip("The dot product result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
