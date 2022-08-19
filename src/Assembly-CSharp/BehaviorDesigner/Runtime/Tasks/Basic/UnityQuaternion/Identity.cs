using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010B7 RID: 4279
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the quaternion identity.")]
	public class Identity : Action
	{
		// Token: 0x060073C3 RID: 29635 RVA: 0x002B0A76 File Offset: 0x002AEC76
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.identity;
			return 2;
		}

		// Token: 0x060073C4 RID: 29636 RVA: 0x002B0A89 File Offset: 0x002AEC89
		public override void OnReset()
		{
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04005F72 RID: 24434
		[Tooltip("The identity")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
