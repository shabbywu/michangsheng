using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014D2 RID: 5330
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Multiply the Vector2 by a float.")]
	public class Multiply : Action
	{
		// Token: 0x06007F7E RID: 32638 RVA: 0x000566E3 File Offset: 0x000548E3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value * this.multiplyBy.Value;
			return 2;
		}

		// Token: 0x06007F7F RID: 32639 RVA: 0x002CA168 File Offset: 0x002C8368
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
			this.multiplyBy = 0f;
		}

		// Token: 0x04006C58 RID: 27736
		[Tooltip("The Vector2 to multiply of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04006C59 RID: 27737
		[Tooltip("The value to multiply the Vector2 of")]
		public SharedFloat multiplyBy;

		// Token: 0x04006C5A RID: 27738
		[Tooltip("The multiplication resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
