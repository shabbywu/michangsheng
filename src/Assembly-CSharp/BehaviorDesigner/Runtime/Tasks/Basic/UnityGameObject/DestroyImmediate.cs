using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001135 RID: 4405
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Destorys the specified GameObject immediately. Returns Success.")]
	public class DestroyImmediate : Action
	{
		// Token: 0x06007576 RID: 30070 RVA: 0x002B46B1 File Offset: 0x002B28B1
		public override TaskStatus OnUpdate()
		{
			Object.DestroyImmediate(base.GetDefaultGameObject(this.targetGameObject.Value));
			return 2;
		}

		// Token: 0x06007577 RID: 30071 RVA: 0x002B46CA File Offset: 0x002B28CA
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006104 RID: 24836
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
