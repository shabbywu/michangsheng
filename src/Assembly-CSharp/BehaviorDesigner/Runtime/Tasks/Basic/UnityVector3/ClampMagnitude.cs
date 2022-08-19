using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02000FFC RID: 4092
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Clamps the magnitude of the Vector3.")]
	public class ClampMagnitude : Action
	{
		// Token: 0x0600712D RID: 28973 RVA: 0x002AB3E6 File Offset: 0x002A95E6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.ClampMagnitude(this.vector3Variable.Value, this.maxLength.Value);
			return 2;
		}

		// Token: 0x0600712E RID: 28974 RVA: 0x002AB410 File Offset: 0x002A9610
		public override void OnReset()
		{
			this.vector3Variable = (this.storeResult = Vector3.zero);
			this.maxLength = 0f;
		}

		// Token: 0x04005D0E RID: 23822
		[Tooltip("The Vector3 to clamp the magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04005D0F RID: 23823
		[Tooltip("The max length of the magnitude")]
		public SharedFloat maxLength;

		// Token: 0x04005D10 RID: 23824
		[Tooltip("The clamp magnitude resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
