using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014C8 RID: 5320
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Returns the distance between two Vector2s.")]
	public class Distance : Action
	{
		// Token: 0x06007F60 RID: 32608 RVA: 0x0005651C File Offset: 0x0005471C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Distance(this.firstVector2.Value, this.secondVector2.Value);
			return 2;
		}

		// Token: 0x06007F61 RID: 32609 RVA: 0x002C9FE8 File Offset: 0x002C81E8
		public override void OnReset()
		{
			this.firstVector2 = (this.secondVector2 = Vector2.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04006C3F RID: 27711
		[Tooltip("The first Vector2")]
		public SharedVector2 firstVector2;

		// Token: 0x04006C40 RID: 27712
		[Tooltip("The second Vector2")]
		public SharedVector2 secondVector2;

		// Token: 0x04006C41 RID: 27713
		[Tooltip("The distance")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
