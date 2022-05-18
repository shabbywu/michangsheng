using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001609 RID: 5641
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the velocity of the CharacterController. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x060083B8 RID: 33720 RVA: 0x002CEDCC File Offset: 0x002CCFCC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083B9 RID: 33721 RVA: 0x0005ABC9 File Offset: 0x00058DC9
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.storeValue.Value = this.characterController.velocity;
			return 2;
		}

		// Token: 0x060083BA RID: 33722 RVA: 0x0005ABFC File Offset: 0x00058DFC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04007069 RID: 28777
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400706A RID: 28778
		[Tooltip("The velocity of the CharacterController")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400706B RID: 28779
		private CharacterController characterController;

		// Token: 0x0400706C RID: 28780
		private GameObject prevGameObject;
	}
}
