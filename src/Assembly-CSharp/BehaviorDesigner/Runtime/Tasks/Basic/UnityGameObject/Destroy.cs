using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001134 RID: 4404
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Destorys the specified GameObject. Returns Success.")]
	public class Destroy : Action
	{
		// Token: 0x06007573 RID: 30067 RVA: 0x002B465C File Offset: 0x002B285C
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

		// Token: 0x06007574 RID: 30068 RVA: 0x002B469D File Offset: 0x002B289D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x04006102 RID: 24834
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006103 RID: 24835
		[Tooltip("Time to destroy the GameObject in")]
		public float time;
	}
}
