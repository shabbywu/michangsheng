using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001015 RID: 4117
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the Vector3 value of the Vector2.")]
	public class GetVector3 : Action
	{
		// Token: 0x06007178 RID: 29048 RVA: 0x002ABD0F File Offset: 0x002A9F0F
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value;
			return 2;
		}

		// Token: 0x06007179 RID: 29049 RVA: 0x002ABD2D File Offset: 0x002A9F2D
		public override void OnReset()
		{
			this.vector3Variable = Vector2.zero;
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04005D4F RID: 23887
		[Tooltip("The Vector2 to get the Vector3 value of")]
		public SharedVector2 vector3Variable;

		// Token: 0x04005D50 RID: 23888
		[Tooltip("The Vector3 value")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
