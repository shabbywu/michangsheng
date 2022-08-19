using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001011 RID: 4113
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the magnitude of the Vector2.")]
	public class GetMagnitude : Action
	{
		// Token: 0x0600716C RID: 29036 RVA: 0x002ABC28 File Offset: 0x002A9E28
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.magnitude;
			return 2;
		}

		// Token: 0x0600716D RID: 29037 RVA: 0x002ABC54 File Offset: 0x002A9E54
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04005D49 RID: 23881
		[Tooltip("The Vector2 to get the magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04005D4A RID: 23882
		[Tooltip("The magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
