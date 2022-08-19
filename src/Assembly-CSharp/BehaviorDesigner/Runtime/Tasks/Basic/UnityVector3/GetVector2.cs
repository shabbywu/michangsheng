using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001004 RID: 4100
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the Vector2 value of the Vector3.")]
	public class GetVector2 : Action
	{
		// Token: 0x06007145 RID: 28997 RVA: 0x002AB613 File Offset: 0x002A9813
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value;
			return 2;
		}

		// Token: 0x06007146 RID: 28998 RVA: 0x002AB631 File Offset: 0x002A9831
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04005D1E RID: 23838
		[Tooltip("The Vector3 to get the Vector2 value of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04005D1F RID: 23839
		[Tooltip("The Vector2 value")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
