using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014C0 RID: 5312
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Multiply the Vector3 by a float.")]
	public class Multiply : Action
	{
		// Token: 0x06007F4B RID: 32587 RVA: 0x00056493 File Offset: 0x00054693
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value * this.multiplyBy.Value;
			return 2;
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x002C9CEC File Offset: 0x002C7EEC
		public override void OnReset()
		{
			this.vector3Variable = (this.storeResult = Vector3.zero);
			this.multiplyBy = 0f;
		}

		// Token: 0x04006C24 RID: 27684
		[Tooltip("The Vector3 to multiply of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04006C25 RID: 27685
		[Tooltip("The value to multiply the Vector3 of")]
		public SharedFloat multiplyBy;

		// Token: 0x04006C26 RID: 27686
		[Tooltip("The multiplication resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
