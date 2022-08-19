using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001002 RID: 4098
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the square magnitude of the Vector3.")]
	public class GetSqrMagnitude : Action
	{
		// Token: 0x0600713F RID: 28991 RVA: 0x002AB5A0 File Offset: 0x002A97A0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value.sqrMagnitude;
			return 2;
		}

		// Token: 0x06007140 RID: 28992 RVA: 0x002AB5CC File Offset: 0x002A97CC
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04005D1B RID: 23835
		[Tooltip("The Vector3 to get the square magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04005D1C RID: 23836
		[Tooltip("The square magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
