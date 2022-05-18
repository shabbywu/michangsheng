using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F4 RID: 5620
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Destorys the specified GameObject immediately. Returns Success.")]
	public class DestroyImmediate : Action
	{
		// Token: 0x06008370 RID: 33648 RVA: 0x0005A638 File Offset: 0x00058838
		public override TaskStatus OnUpdate()
		{
			Object.DestroyImmediate(base.GetDefaultGameObject(this.targetGameObject.Value));
			return 2;
		}

		// Token: 0x06008371 RID: 33649 RVA: 0x0005A651 File Offset: 0x00058851
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04007027 RID: 28711
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
