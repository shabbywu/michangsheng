using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014C5 RID: 5317
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Sets the value of the Vector3.")]
	public class SetValue : Action
	{
		// Token: 0x06007F57 RID: 32599 RVA: 0x000564DA File Offset: 0x000546DA
		public override TaskStatus OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Value.Value;
			return 2;
		}

		// Token: 0x06007F58 RID: 32600 RVA: 0x002C9EC4 File Offset: 0x002C80C4
		public override void OnReset()
		{
			this.vector3Value = (this.vector3Variable = Vector3.zero);
		}

		// Token: 0x04006C36 RID: 27702
		[Tooltip("The Vector3 to get the values of")]
		public SharedVector3 vector3Value;

		// Token: 0x04006C37 RID: 27703
		[Tooltip("The Vector3 to set the values of")]
		public SharedVector3 vector3Variable;
	}
}
