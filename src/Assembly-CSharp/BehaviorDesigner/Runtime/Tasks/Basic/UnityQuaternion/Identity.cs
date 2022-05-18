using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x02001571 RID: 5489
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the quaternion identity.")]
	public class Identity : Action
	{
		// Token: 0x060081BD RID: 33213 RVA: 0x00058C41 File Offset: 0x00056E41
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.identity;
			return 2;
		}

		// Token: 0x060081BE RID: 33214 RVA: 0x00058C54 File Offset: 0x00056E54
		public override void OnReset()
		{
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04006E72 RID: 28274
		[Tooltip("The identity")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
