using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015FA RID: 5626
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Instantiates a new GameObject. Returns Success.")]
	public class Instantiate : Action
	{
		// Token: 0x06008382 RID: 33666 RVA: 0x0005A76B File Offset: 0x0005896B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Object.Instantiate<GameObject>(this.targetGameObject.Value, this.position.Value, this.rotation.Value);
			return 2;
		}

		// Token: 0x06008383 RID: 33667 RVA: 0x0005A79F File Offset: 0x0005899F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04007033 RID: 28723
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007034 RID: 28724
		[Tooltip("The position of the new GameObject")]
		public SharedVector3 position;

		// Token: 0x04007035 RID: 28725
		[Tooltip("The rotation of the new GameObject")]
		public SharedQuaternion rotation = Quaternion.identity;

		// Token: 0x04007036 RID: 28726
		[SharedRequired]
		[Tooltip("The instantiated GameObject")]
		public SharedGameObject storeResult;
	}
}
