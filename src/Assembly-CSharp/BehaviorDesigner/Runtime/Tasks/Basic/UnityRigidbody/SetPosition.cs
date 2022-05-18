using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001563 RID: 5475
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the position of the Rigidbody. Returns Success.")]
	public class SetPosition : Action
	{
		// Token: 0x0600818A RID: 33162 RVA: 0x002CC4AC File Offset: 0x002CA6AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600818B RID: 33163 RVA: 0x000588F0 File Offset: 0x00056AF0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.position = this.position.Value;
			return 2;
		}

		// Token: 0x0600818C RID: 33164 RVA: 0x00058923 File Offset: 0x00056B23
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04006E44 RID: 28228
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E45 RID: 28229
		[Tooltip("The position of the Rigidbody")]
		public SharedVector3 position;

		// Token: 0x04006E46 RID: 28230
		private Rigidbody rigidbody;

		// Token: 0x04006E47 RID: 28231
		private GameObject prevGameObject;
	}
}
