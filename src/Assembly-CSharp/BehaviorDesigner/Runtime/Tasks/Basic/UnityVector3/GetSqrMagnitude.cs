using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014BA RID: 5306
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the square magnitude of the Vector3.")]
	public class GetSqrMagnitude : Action
	{
		// Token: 0x06007F39 RID: 32569 RVA: 0x002C9BA0 File Offset: 0x002C7DA0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value.sqrMagnitude;
			return 2;
		}

		// Token: 0x06007F3A RID: 32570 RVA: 0x0005639E File Offset: 0x0005459E
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04006C13 RID: 27667
		[Tooltip("The Vector3 to get the square magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04006C14 RID: 27668
		[Tooltip("The square magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
