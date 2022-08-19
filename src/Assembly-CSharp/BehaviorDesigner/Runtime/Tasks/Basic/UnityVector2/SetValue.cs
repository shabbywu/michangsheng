using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x0200101C RID: 4124
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Sets the value of the Vector2.")]
	public class SetValue : Action
	{
		// Token: 0x0600718D RID: 29069 RVA: 0x002AC03A File Offset: 0x002AA23A
		public override TaskStatus OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Value.Value;
			return 2;
		}

		// Token: 0x0600718E RID: 29070 RVA: 0x002AC054 File Offset: 0x002AA254
		public override void OnReset()
		{
			this.vector2Value = (this.vector2Variable = Vector2.zero);
		}

		// Token: 0x04005D65 RID: 23909
		[Tooltip("The Vector2 to get the values of")]
		public SharedVector2 vector2Value;

		// Token: 0x04005D66 RID: 23910
		[Tooltip("The Vector2 to set the values of")]
		public SharedVector2 vector2Variable;
	}
}
