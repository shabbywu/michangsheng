using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001010 RID: 4112
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the dot product of two Vector2 values.")]
	public class Dot : Action
	{
		// Token: 0x06007169 RID: 29033 RVA: 0x002ABBC6 File Offset: 0x002A9DC6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Dot(this.leftHandSide.Value, this.rightHandSide.Value);
			return 2;
		}

		// Token: 0x0600716A RID: 29034 RVA: 0x002ABBF0 File Offset: 0x002A9DF0
		public override void OnReset()
		{
			this.leftHandSide = (this.rightHandSide = Vector2.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04005D46 RID: 23878
		[Tooltip("The left hand side of the dot product")]
		public SharedVector2 leftHandSide;

		// Token: 0x04005D47 RID: 23879
		[Tooltip("The right hand side of the dot product")]
		public SharedVector2 rightHandSide;

		// Token: 0x04005D48 RID: 23880
		[Tooltip("The dot product result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
