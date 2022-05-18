using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014C1 RID: 5313
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Normalize the Vector3.")]
	public class Normalize : Action
	{
		// Token: 0x06007F4E RID: 32590 RVA: 0x000564BC File Offset: 0x000546BC
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Normalize(this.vector3Variable.Value);
			return 2;
		}

		// Token: 0x06007F4F RID: 32591 RVA: 0x002C9D24 File Offset: 0x002C7F24
		public override void OnReset()
		{
			this.vector3Variable = (this.storeResult = Vector3.zero);
		}

		// Token: 0x04006C27 RID: 27687
		[Tooltip("The Vector3 to normalize")]
		public SharedVector3 vector3Variable;

		// Token: 0x04006C28 RID: 27688
		[Tooltip("The normalized resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
