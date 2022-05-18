using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200160C RID: 5644
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("A more complex move function taking absolute movement deltas. Returns Success.")]
	public class Move : Action
	{
		// Token: 0x060083C5 RID: 33733 RVA: 0x002CEEA0 File Offset: 0x002CD0A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083C6 RID: 33734 RVA: 0x0005AC98 File Offset: 0x00058E98
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

		// Token: 0x060083C7 RID: 33735 RVA: 0x0005ACCC File Offset: 0x00058ECC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.motion = Vector3.zero;
		}

		// Token: 0x04007074 RID: 28788
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007075 RID: 28789
		[Tooltip("The amount to move")]
		public SharedVector3 motion;

		// Token: 0x04007076 RID: 28790
		private CharacterController characterController;

		// Token: 0x04007077 RID: 28791
		private GameObject prevGameObject;
	}
}
