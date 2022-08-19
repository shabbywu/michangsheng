using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200114A RID: 4426
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the velocity of the CharacterController. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x060075BE RID: 30142 RVA: 0x002B5058 File Offset: 0x002B3258
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075BF RID: 30143 RVA: 0x002B5098 File Offset: 0x002B3298
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

		// Token: 0x060075C0 RID: 30144 RVA: 0x002B50CB File Offset: 0x002B32CB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006146 RID: 24902
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006147 RID: 24903
		[Tooltip("The velocity of the CharacterController")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006148 RID: 24904
		private CharacterController characterController;

		// Token: 0x04006149 RID: 24905
		private GameObject prevGameObject;
	}
}
