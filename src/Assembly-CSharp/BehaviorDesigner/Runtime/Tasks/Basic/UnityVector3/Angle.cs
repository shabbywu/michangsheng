using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02000FFB RID: 4091
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Returns the angle between two Vector3s.")]
	public class Angle : Action
	{
		// Token: 0x0600712A RID: 28970 RVA: 0x002AB387 File Offset: 0x002A9587
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Angle(this.firstVector3.Value, this.secondVector3.Value);
			return 2;
		}

		// Token: 0x0600712B RID: 28971 RVA: 0x002AB3B0 File Offset: 0x002A95B0
		public override void OnReset()
		{
			this.firstVector3 = (this.secondVector3 = Vector3.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04005D0B RID: 23819
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04005D0C RID: 23820
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04005D0D RID: 23821
		[Tooltip("The angle")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
