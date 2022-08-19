using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001000 RID: 4096
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the magnitude of the Vector3.")]
	public class GetMagnitude : Action
	{
		// Token: 0x06007139 RID: 28985 RVA: 0x002AB52C File Offset: 0x002A972C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value.magnitude;
			return 2;
		}

		// Token: 0x0600713A RID: 28986 RVA: 0x002AB558 File Offset: 0x002A9758
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04005D18 RID: 23832
		[Tooltip("The Vector3 to get the magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04005D19 RID: 23833
		[Tooltip("The magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
