using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x0200101A RID: 4122
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Normalize the Vector2.")]
	public class Normalize : Action
	{
		// Token: 0x06007187 RID: 29063 RVA: 0x002ABF10 File Offset: 0x002AA110
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.normalized;
			return 2;
		}

		// Token: 0x06007188 RID: 29064 RVA: 0x002ABF3C File Offset: 0x002AA13C
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
		}

		// Token: 0x04005D5F RID: 23903
		[Tooltip("The Vector2 to normalize")]
		public SharedVector2 vector2Variable;

		// Token: 0x04005D60 RID: 23904
		[Tooltip("The normalized resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
