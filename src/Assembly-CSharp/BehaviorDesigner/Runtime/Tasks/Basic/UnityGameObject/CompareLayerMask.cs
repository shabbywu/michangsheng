using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001132 RID: 4402
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns Success if the layermasks match, otherwise Failure.")]
	public class CompareLayerMask : Conditional
	{
		// Token: 0x0600756D RID: 30061 RVA: 0x002B45E6 File Offset: 0x002B27E6
		public override TaskStatus OnUpdate()
		{
			if ((base.GetDefaultGameObject(this.targetGameObject.Value).layer & this.layermask.value) == 0)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600756E RID: 30062 RVA: 0x002B460F File Offset: 0x002B280F
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040060FE RID: 24830
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060FF RID: 24831
		[Tooltip("The layermask to compare against")]
		public LayerMask layermask;
	}
}
