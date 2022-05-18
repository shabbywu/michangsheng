using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityDebug
{
	// Token: 0x020015FE RID: 5630
	[TaskCategory("Basic/Debug")]
	[TaskDescription("Draws a debug line")]
	public class DrawLine : Action
	{
		// Token: 0x0600838E RID: 33678 RVA: 0x0005A86F File Offset: 0x00058A6F
		public override TaskStatus OnUpdate()
		{
			Debug.DrawLine(this.start.Value, this.end.Value, this.color.Value, this.duration.Value, this.depthTest.Value);
			return 2;
		}

		// Token: 0x0600838F RID: 33679 RVA: 0x002CEA6C File Offset: 0x002CCC6C
		public override void OnReset()
		{
			this.start = Vector3.zero;
			this.end = Vector3.zero;
			this.color = Color.white;
			this.duration = 0f;
			this.depthTest = true;
		}

		// Token: 0x0400703E RID: 28734
		[Tooltip("The start position")]
		public SharedVector3 start;

		// Token: 0x0400703F RID: 28735
		[Tooltip("The end position")]
		public SharedVector3 end;

		// Token: 0x04007040 RID: 28736
		[Tooltip("The color")]
		public SharedColor color = Color.white;

		// Token: 0x04007041 RID: 28737
		[Tooltip("Duration the line will be visible for in seconds.\nDefault: 0 means 1 frame.")]
		public SharedFloat duration;

		// Token: 0x04007042 RID: 28738
		[Tooltip("Whether the line should show through world geometry.")]
		public SharedBool depthTest = true;
	}
}
