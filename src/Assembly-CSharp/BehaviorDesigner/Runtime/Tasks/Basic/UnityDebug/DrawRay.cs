using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityDebug
{
	// Token: 0x02001140 RID: 4416
	[TaskCategory("Basic/Debug")]
	[TaskDescription("Draws a debug ray")]
	public class DrawRay : Action
	{
		// Token: 0x06007597 RID: 30103 RVA: 0x002B4A5D File Offset: 0x002B2C5D
		public override TaskStatus OnUpdate()
		{
			Debug.DrawRay(this.start.Value, this.direction.Value, this.color.Value);
			return 2;
		}

		// Token: 0x06007598 RID: 30104 RVA: 0x002B4A86 File Offset: 0x002B2C86
		public override void OnReset()
		{
			this.start = Vector3.zero;
			this.direction = Vector3.zero;
			this.color = Color.white;
		}

		// Token: 0x04006120 RID: 24864
		[Tooltip("The position")]
		public SharedVector3 start;

		// Token: 0x04006121 RID: 24865
		[Tooltip("The direction")]
		public SharedVector3 direction;

		// Token: 0x04006122 RID: 24866
		[Tooltip("The color")]
		public SharedColor color = Color.white;
	}
}
