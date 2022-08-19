using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x0200100D RID: 4109
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Sets the X, Y, and Z values of the Vector3.")]
	public class SetXYZ : Action
	{
		// Token: 0x06007160 RID: 29024 RVA: 0x002ABA44 File Offset: 0x002A9C44
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

		// Token: 0x06007161 RID: 29025 RVA: 0x002ABAC8 File Offset: 0x002A9CC8
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.xValue = (this.yValue = (this.zValue = 0f));
		}

		// Token: 0x04005D3C RID: 23868
		[Tooltip("The Vector3 to set the values of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04005D3D RID: 23869
		[Tooltip("The X value. Set to None to have the value ignored")]
		public SharedFloat xValue;

		// Token: 0x04005D3E RID: 23870
		[Tooltip("The Y value. Set to None to have the value ignored")]
		public SharedFloat yValue;

		// Token: 0x04005D3F RID: 23871
		[Tooltip("The Z value. Set to None to have the value ignored")]
		public SharedFloat zValue;
	}
}
