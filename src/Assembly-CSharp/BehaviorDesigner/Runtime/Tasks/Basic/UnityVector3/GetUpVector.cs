using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001003 RID: 4099
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the up vector value.")]
	public class GetUpVector : Action
	{
		// Token: 0x06007142 RID: 28994 RVA: 0x002AB5EE File Offset: 0x002A97EE
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.up;
			return 2;
		}

		// Token: 0x06007143 RID: 28995 RVA: 0x002AB601 File Offset: 0x002A9801
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04005D1D RID: 23837
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
