using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x0200113B RID: 4411
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Instantiates a new GameObject. Returns Success.")]
	public class Instantiate : Action
	{
		// Token: 0x06007588 RID: 30088 RVA: 0x002B4827 File Offset: 0x002B2A27
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Object.Instantiate<GameObject>(this.targetGameObject.Value, this.position.Value, this.rotation.Value);
			return 2;
		}

		// Token: 0x06007589 RID: 30089 RVA: 0x002B485B File Offset: 0x002B2A5B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04006110 RID: 24848
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006111 RID: 24849
		[Tooltip("The position of the new GameObject")]
		public SharedVector3 position;

		// Token: 0x04006112 RID: 24850
		[Tooltip("The rotation of the new GameObject")]
		public SharedQuaternion rotation = Quaternion.identity;

		// Token: 0x04006113 RID: 24851
		[SharedRequired]
		[Tooltip("The instantiated GameObject")]
		public SharedGameObject storeResult;
	}
}
