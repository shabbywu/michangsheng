using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001019 RID: 4121
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Multiply the Vector2 by a float.")]
	public class Multiply : Action
	{
		// Token: 0x06007184 RID: 29060 RVA: 0x002ABEAF File Offset: 0x002AA0AF
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value * this.multiplyBy.Value;
			return 2;
		}

		// Token: 0x06007185 RID: 29061 RVA: 0x002ABED8 File Offset: 0x002AA0D8
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
			this.multiplyBy = 0f;
		}

		// Token: 0x04005D5C RID: 23900
		[Tooltip("The Vector2 to multiply of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04005D5D RID: 23901
		[Tooltip("The value to multiply the Vector2 of")]
		public SharedFloat multiplyBy;

		// Token: 0x04005D5E RID: 23902
		[Tooltip("The multiplication resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
