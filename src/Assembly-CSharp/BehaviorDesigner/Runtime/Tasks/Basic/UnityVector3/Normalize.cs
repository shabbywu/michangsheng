using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001009 RID: 4105
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Normalize the Vector3.")]
	public class Normalize : Action
	{
		// Token: 0x06007154 RID: 29012 RVA: 0x002AB842 File Offset: 0x002A9A42
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Normalize(this.vector3Variable.Value);
			return 2;
		}

		// Token: 0x06007155 RID: 29013 RVA: 0x002AB860 File Offset: 0x002A9A60
		public override void OnReset()
		{
			this.vector3Variable = (this.storeResult = Vector3.zero);
		}

		// Token: 0x04005D2F RID: 23855
		[Tooltip("The Vector3 to normalize")]
		public SharedVector3 vector3Variable;

		// Token: 0x04005D30 RID: 23856
		[Tooltip("The normalized resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
