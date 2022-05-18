using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014B3 RID: 5299
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Returns the angle between two Vector3s.")]
	public class Angle : Action
	{
		// Token: 0x06007F24 RID: 32548 RVA: 0x0005628E File Offset: 0x0005448E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Angle(this.firstVector3.Value, this.secondVector3.Value);
			return 2;
		}

		// Token: 0x06007F25 RID: 32549 RVA: 0x002C9A94 File Offset: 0x002C7C94
		public override void OnReset()
		{
			this.firstVector3 = (this.secondVector3 = Vector3.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04006C03 RID: 27651
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04006C04 RID: 27652
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04006C05 RID: 27653
		[Tooltip("The angle")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
