using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x0200100C RID: 4108
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Sets the value of the Vector3.")]
	public class SetValue : Action
	{
		// Token: 0x0600715D RID: 29021 RVA: 0x002ABA00 File Offset: 0x002A9C00
		public override TaskStatus OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Value.Value;
			return 2;
		}

		// Token: 0x0600715E RID: 29022 RVA: 0x002ABA1C File Offset: 0x002A9C1C
		public override void OnReset()
		{
			this.vector3Value = (this.vector3Variable = Vector3.zero);
		}

		// Token: 0x04005D3A RID: 23866
		[Tooltip("The Vector3 to get the values of")]
		public SharedVector3 vector3Value;

		// Token: 0x04005D3B RID: 23867
		[Tooltip("The Vector3 to set the values of")]
		public SharedVector3 vector3Variable;
	}
}
