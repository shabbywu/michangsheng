using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014B5 RID: 5301
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Returns the distance between two Vector3s.")]
	public class Distance : Action
	{
		// Token: 0x06007F2A RID: 32554 RVA: 0x000562E0 File Offset: 0x000544E0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Distance(this.firstVector3.Value, this.secondVector3.Value);
			return 2;
		}

		// Token: 0x06007F2B RID: 32555 RVA: 0x002C9B04 File Offset: 0x002C7D04
		public override void OnReset()
		{
			this.firstVector3 = (this.secondVector3 = Vector3.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04006C09 RID: 27657
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04006C0A RID: 27658
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04006C0B RID: 27659
		[Tooltip("The distance")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
