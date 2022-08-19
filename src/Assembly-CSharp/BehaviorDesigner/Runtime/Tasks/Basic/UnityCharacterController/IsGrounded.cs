using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200114C RID: 4428
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Returns Success if the character is grounded, otherwise Failure.")]
	public class IsGrounded : Conditional
	{
		// Token: 0x060075C7 RID: 30151 RVA: 0x002B5188 File Offset: 0x002B3388
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075C8 RID: 30152 RVA: 0x002B51C8 File Offset: 0x002B33C8
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			if (!this.characterController.isGrounded)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060075C9 RID: 30153 RVA: 0x002B51F4 File Offset: 0x002B33F4
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400614E RID: 24910
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400614F RID: 24911
		private CharacterController characterController;

		// Token: 0x04006150 RID: 24912
		private GameObject prevGameObject;
	}
}
