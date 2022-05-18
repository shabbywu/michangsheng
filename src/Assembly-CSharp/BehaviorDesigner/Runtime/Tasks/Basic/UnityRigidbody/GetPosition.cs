using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001553 RID: 5459
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the position of the Rigidbody. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x0600814A RID: 33098 RVA: 0x002CC0AC File Offset: 0x002CA2AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x00058478 File Offset: 0x00056678
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.position;
			return 2;
		}

		// Token: 0x0600814C RID: 33100 RVA: 0x000584AB File Offset: 0x000566AB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006E06 RID: 28166
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E07 RID: 28167
		[Tooltip("The position of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006E08 RID: 28168
		private Rigidbody rigidbody;

		// Token: 0x04006E09 RID: 28169
		private GameObject prevGameObject;
	}
}
