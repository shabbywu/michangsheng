using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014D3 RID: 5331
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Normalize the Vector2.")]
	public class Normalize : Action
	{
		// Token: 0x06007F81 RID: 32641 RVA: 0x002CA1A0 File Offset: 0x002C83A0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.normalized;
			return 2;
		}

		// Token: 0x06007F82 RID: 32642 RVA: 0x002CA1CC File Offset: 0x002C83CC
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
		}

		// Token: 0x04006C5B RID: 27739
		[Tooltip("The Vector2 to normalize")]
		public SharedVector2 vector2Variable;

		// Token: 0x04006C5C RID: 27740
		[Tooltip("The normalized resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
