using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001013 RID: 4115
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the square magnitude of the Vector2.")]
	public class GetSqrMagnitude : Action
	{
		// Token: 0x06007172 RID: 29042 RVA: 0x002ABC9C File Offset: 0x002A9E9C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.sqrMagnitude;
			return 2;
		}

		// Token: 0x06007173 RID: 29043 RVA: 0x002ABCC8 File Offset: 0x002A9EC8
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04005D4C RID: 23884
		[Tooltip("The Vector2 to get the square magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04005D4D RID: 23885
		[Tooltip("The square magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
