using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02000FFF RID: 4095
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the forward vector value.")]
	public class GetForwardVector : Action
	{
		// Token: 0x06007136 RID: 28982 RVA: 0x002AB506 File Offset: 0x002A9706
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.forward;
			return 2;
		}

		// Token: 0x06007137 RID: 28983 RVA: 0x002AB519 File Offset: 0x002A9719
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04005D17 RID: 23831
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
