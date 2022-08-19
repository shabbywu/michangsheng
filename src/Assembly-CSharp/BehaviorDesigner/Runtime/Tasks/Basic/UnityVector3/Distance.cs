using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02000FFD RID: 4093
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Returns the distance between two Vector3s.")]
	public class Distance : Action
	{
		// Token: 0x06007130 RID: 28976 RVA: 0x002AB446 File Offset: 0x002A9646
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Distance(this.firstVector3.Value, this.secondVector3.Value);
			return 2;
		}

		// Token: 0x06007131 RID: 28977 RVA: 0x002AB470 File Offset: 0x002A9670
		public override void OnReset()
		{
			this.firstVector3 = (this.secondVector3 = Vector3.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04005D11 RID: 23825
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04005D12 RID: 23826
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04005D13 RID: 23827
		[Tooltip("The distance")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
