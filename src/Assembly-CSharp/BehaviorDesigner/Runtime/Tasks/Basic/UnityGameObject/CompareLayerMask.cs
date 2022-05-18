using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F1 RID: 5617
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Returns Success if the layermasks match, otherwise Failure.")]
	public class CompareLayerMask : Conditional
	{
		// Token: 0x06008367 RID: 33639 RVA: 0x0005A5B1 File Offset: 0x000587B1
		public override TaskStatus OnUpdate()
		{
			if ((base.GetDefaultGameObject(this.targetGameObject.Value).layer & this.layermask.value) == 0)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008368 RID: 33640 RVA: 0x0005A5DA File Offset: 0x000587DA
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04007021 RID: 28705
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007022 RID: 28706
		[Tooltip("The layermask to compare against")]
		public LayerMask layermask;
	}
}
