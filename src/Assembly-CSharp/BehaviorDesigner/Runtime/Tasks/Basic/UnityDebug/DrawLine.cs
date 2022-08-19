using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityDebug
{
	// Token: 0x0200113F RID: 4415
	[TaskCategory("Basic/Debug")]
	[TaskDescription("Draws a debug line")]
	public class DrawLine : Action
	{
		// Token: 0x06007594 RID: 30100 RVA: 0x002B499F File Offset: 0x002B2B9F
		public override TaskStatus OnUpdate()
		{
			Debug.DrawLine(this.start.Value, this.end.Value, this.color.Value, this.duration.Value, this.depthTest.Value);
			return 2;
		}

		// Token: 0x06007595 RID: 30101 RVA: 0x002B49E0 File Offset: 0x002B2BE0
		public override void OnReset()
		{
			this.start = Vector3.zero;
			this.end = Vector3.zero;
			this.color = Color.white;
			this.duration = 0f;
			this.depthTest = true;
		}

		// Token: 0x0400611B RID: 24859
		[Tooltip("The start position")]
		public SharedVector3 start;

		// Token: 0x0400611C RID: 24860
		[Tooltip("The end position")]
		public SharedVector3 end;

		// Token: 0x0400611D RID: 24861
		[Tooltip("The color")]
		public SharedColor color = Color.white;

		// Token: 0x0400611E RID: 24862
		[Tooltip("Duration the line will be visible for in seconds.\nDefault: 0 means 1 frame.")]
		public SharedFloat duration;

		// Token: 0x0400611F RID: 24863
		[Tooltip("Whether the line should show through world geometry.")]
		public SharedBool depthTest = true;
	}
}
