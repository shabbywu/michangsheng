using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014CE RID: 5326
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the Vector3 value of the Vector2.")]
	public class GetVector3 : Action
	{
		// Token: 0x06007F72 RID: 32626 RVA: 0x000565FC File Offset: 0x000547FC
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value;
			return 2;
		}

		// Token: 0x06007F73 RID: 32627 RVA: 0x0005661A File Offset: 0x0005481A
		public override void OnReset()
		{
			this.vector3Variable = Vector2.zero;
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04006C4B RID: 27723
		[Tooltip("The Vector2 to get the Vector3 value of")]
		public SharedVector2 vector3Variable;

		// Token: 0x04006C4C RID: 27724
		[Tooltip("The Vector3 value")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
