using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014C6 RID: 5318
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Sets the X, Y, and Z values of the Vector3.")]
	public class SetXYZ : Action
	{
		// Token: 0x06007F5A RID: 32602 RVA: 0x002C9EEC File Offset: 0x002C80EC
		public override TaskStatus OnUpdate()
		{
			Vector3 value = this.vector3Variable.Value;
			if (!this.xValue.IsNone)
			{
				value.x = this.xValue.Value;
			}
			if (!this.yValue.IsNone)
			{
				value.y = this.yValue.Value;
			}
			if (!this.zValue.IsNone)
			{
				value.z = this.zValue.Value;
			}
			this.vector3Variable.Value = value;
			return 2;
		}

		// Token: 0x06007F5B RID: 32603 RVA: 0x002C9F70 File Offset: 0x002C8170
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.xValue = (this.yValue = (this.zValue = 0f));
		}

		// Token: 0x04006C38 RID: 27704
		[Tooltip("The Vector3 to set the values of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04006C39 RID: 27705
		[Tooltip("The X value. Set to None to have the value ignored")]
		public SharedFloat xValue;

		// Token: 0x04006C3A RID: 27706
		[Tooltip("The Y value. Set to None to have the value ignored")]
		public SharedFloat yValue;

		// Token: 0x04006C3B RID: 27707
		[Tooltip("The Z value. Set to None to have the value ignored")]
		public SharedFloat zValue;
	}
}
