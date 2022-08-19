using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x0200100F RID: 4111
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Returns the distance between two Vector2s.")]
	public class Distance : Action
	{
		// Token: 0x06007166 RID: 29030 RVA: 0x002ABB66 File Offset: 0x002A9D66
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Distance(this.firstVector2.Value, this.secondVector2.Value);
			return 2;
		}

		// Token: 0x06007167 RID: 29031 RVA: 0x002ABB90 File Offset: 0x002A9D90
		public override void OnReset()
		{
			this.firstVector2 = (this.secondVector2 = Vector2.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04005D43 RID: 23875
		[Tooltip("The first Vector2")]
		public SharedVector2 firstVector2;

		// Token: 0x04005D44 RID: 23876
		[Tooltip("The second Vector2")]
		public SharedVector2 secondVector2;

		// Token: 0x04005D45 RID: 23877
		[Tooltip("The distance")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
