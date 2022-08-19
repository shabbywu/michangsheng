using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001016 RID: 4118
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the X and Y values of the Vector2.")]
	public class GetXY : Action
	{
		// Token: 0x0600717B RID: 29051 RVA: 0x002ABD4F File Offset: 0x002A9F4F
		public override TaskStatus OnUpdate()
		{
			this.storeX.Value = this.vector2Variable.Value.x;
			this.storeY.Value = this.vector2Variable.Value.y;
			return 2;
		}

		// Token: 0x0600717C RID: 29052 RVA: 0x002ABD88 File Offset: 0x002A9F88
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeX = (this.storeY = 0f);
		}

		// Token: 0x04005D51 RID: 23889
		[Tooltip("The Vector2 to get the values of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04005D52 RID: 23890
		[Tooltip("The X value")]
		[RequiredField]
		public SharedFloat storeX;

		// Token: 0x04005D53 RID: 23891
		[Tooltip("The Y value")]
		[RequiredField]
		public SharedFloat storeY;
	}
}
