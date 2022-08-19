using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200114D RID: 4429
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("A more complex move function taking absolute movement deltas. Returns Success.")]
	public class Move : Action
	{
		// Token: 0x060075CB RID: 30155 RVA: 0x002B5200 File Offset: 0x002B3400
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075CC RID: 30156 RVA: 0x002B5240 File Offset: 0x002B3440
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.characterController.Move(this.motion.Value);
			return 2;
		}

		// Token: 0x060075CD RID: 30157 RVA: 0x002B5274 File Offset: 0x002B3474
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.motion = Vector3.zero;
		}

		// Token: 0x04006151 RID: 24913
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006152 RID: 24914
		[Tooltip("The amount to move")]
		public SharedVector3 motion;

		// Token: 0x04006153 RID: 24915
		private CharacterController characterController;

		// Token: 0x04006154 RID: 24916
		private GameObject prevGameObject;
	}
}
