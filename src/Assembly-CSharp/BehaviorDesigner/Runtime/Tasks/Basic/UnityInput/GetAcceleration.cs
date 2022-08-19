using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x02001123 RID: 4387
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the acceleration value.")]
	public class GetAcceleration : Action
	{
		// Token: 0x06007540 RID: 30016 RVA: 0x002B42E5 File Offset: 0x002B24E5
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.acceleration;
			return 2;
		}

		// Token: 0x06007541 RID: 30017 RVA: 0x002B42F8 File Offset: 0x002B24F8
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x040060E8 RID: 24808
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedVector3 storeResult;
	}
}
