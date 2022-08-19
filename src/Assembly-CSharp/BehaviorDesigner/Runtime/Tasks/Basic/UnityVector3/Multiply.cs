using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001008 RID: 4104
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Multiply the Vector3 by a float.")]
	public class Multiply : Action
	{
		// Token: 0x06007151 RID: 29009 RVA: 0x002AB7E3 File Offset: 0x002A99E3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value * this.multiplyBy.Value;
			return 2;
		}

		// Token: 0x06007152 RID: 29010 RVA: 0x002AB80C File Offset: 0x002A9A0C
		public override void OnReset()
		{
			this.vector3Variable = (this.storeResult = Vector3.zero);
			this.multiplyBy = 0f;
		}

		// Token: 0x04005D2C RID: 23852
		[Tooltip("The Vector3 to multiply of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04005D2D RID: 23853
		[Tooltip("The value to multiply the Vector3 of")]
		public SharedFloat multiplyBy;

		// Token: 0x04005D2E RID: 23854
		[Tooltip("The multiplication resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
