using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x0200101D RID: 4125
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Sets the X and Y values of the Vector2.")]
	public class SetXY : Action
	{
		// Token: 0x06007190 RID: 29072 RVA: 0x002AC07C File Offset: 0x002AA27C
		public override TaskStatus OnUpdate()
		{
			Vector2 value = this.vector2Variable.Value;
			if (!this.xValue.IsNone)
			{
				value.x = this.xValue.Value;
			}
			if (!this.yValue.IsNone)
			{
				value.y = this.yValue.Value;
			}
			this.vector2Variable.Value = value;
			return 2;
		}

		// Token: 0x06007191 RID: 29073 RVA: 0x002AC0E0 File Offset: 0x002AA2E0
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.xValue = (this.yValue = 0f);
		}

		// Token: 0x04005D67 RID: 23911
		[Tooltip("The Vector2 to set the values of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04005D68 RID: 23912
		[Tooltip("The X value. Set to None to have the value ignored")]
		public SharedFloat xValue;

		// Token: 0x04005D69 RID: 23913
		[Tooltip("The Y value. Set to None to have the value ignored")]
		public SharedFloat yValue;
	}
}
