using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014D7 RID: 5335
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Sets the X and Y values of the Vector2.")]
	public class SetXY : Action
	{
		// Token: 0x06007F8A RID: 32650 RVA: 0x002CA2F4 File Offset: 0x002C84F4
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

		// Token: 0x06007F8B RID: 32651 RVA: 0x002CA358 File Offset: 0x002C8558
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.xValue = (this.yValue = 0f);
		}

		// Token: 0x04006C67 RID: 27751
		[Tooltip("The Vector2 to set the values of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04006C68 RID: 27752
		[Tooltip("The X value. Set to None to have the value ignored")]
		public SharedFloat xValue;

		// Token: 0x04006C69 RID: 27753
		[Tooltip("The Y value. Set to None to have the value ignored")]
		public SharedFloat yValue;
	}
}
