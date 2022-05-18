using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200160B RID: 5643
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Returns Success if the character is grounded, otherwise Failure.")]
	public class IsGrounded : Conditional
	{
		// Token: 0x060083C1 RID: 33729 RVA: 0x002CEE60 File Offset: 0x002CD060
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083C2 RID: 33730 RVA: 0x0005AC63 File Offset: 0x00058E63
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

		// Token: 0x060083C3 RID: 33731 RVA: 0x0005AC8F File Offset: 0x00058E8F
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04007071 RID: 28785
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007072 RID: 28786
		private CharacterController characterController;

		// Token: 0x04007073 RID: 28787
		private GameObject prevGameObject;
	}
}
