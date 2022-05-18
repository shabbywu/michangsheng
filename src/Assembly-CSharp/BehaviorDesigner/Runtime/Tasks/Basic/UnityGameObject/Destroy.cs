using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F3 RID: 5619
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Destorys the specified GameObject. Returns Success.")]
	public class Destroy : Action
	{
		// Token: 0x0600836D RID: 33645 RVA: 0x002CE974 File Offset: 0x002CCB74
		public override TaskStatus OnUpdate()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (this.time == 0f)
			{
				Object.Destroy(defaultGameObject);
			}
			else
			{
				Object.Destroy(defaultGameObject, this.time);
			}
			return 2;
		}

		// Token: 0x0600836E RID: 33646 RVA: 0x0005A624 File Offset: 0x00058824
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x04007025 RID: 28709
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007026 RID: 28710
		[Tooltip("Time to destroy the GameObject in")]
		public float time;
	}
}
