using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityDebug
{
	// Token: 0x020015FF RID: 5631
	[TaskCategory("Basic/Debug")]
	[TaskDescription("Draws a debug ray")]
	public class DrawRay : Action
	{
		// Token: 0x06008391 RID: 33681 RVA: 0x0005A8D2 File Offset: 0x00058AD2
		public override TaskStatus OnUpdate()
		{
			Debug.DrawRay(this.start.Value, this.direction.Value, this.color.Value);
			return 2;
		}

		// Token: 0x06008392 RID: 33682 RVA: 0x0005A8FB File Offset: 0x00058AFB
		public override void OnReset()
		{
			this.start = Vector3.zero;
			this.direction = Vector3.zero;
			this.color = Color.white;
		}

		// Token: 0x04007043 RID: 28739
		[Tooltip("The position")]
		public SharedVector3 start;

		// Token: 0x04007044 RID: 28740
		[Tooltip("The direction")]
		public SharedVector3 direction;

		// Token: 0x04007045 RID: 28741
		[Tooltip("The color")]
		public SharedColor color = Color.white;
	}
}
