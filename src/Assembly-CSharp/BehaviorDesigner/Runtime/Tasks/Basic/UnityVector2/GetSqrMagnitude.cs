using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014CC RID: 5324
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the square magnitude of the Vector2.")]
	public class GetSqrMagnitude : Action
	{
		// Token: 0x06007F6C RID: 32620 RVA: 0x002CA084 File Offset: 0x002C8284
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.sqrMagnitude;
			return 2;
		}

		// Token: 0x06007F6D RID: 32621 RVA: 0x000565B5 File Offset: 0x000547B5
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04006C48 RID: 27720
		[Tooltip("The Vector2 to get the square magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04006C49 RID: 27721
		[Tooltip("The square magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
