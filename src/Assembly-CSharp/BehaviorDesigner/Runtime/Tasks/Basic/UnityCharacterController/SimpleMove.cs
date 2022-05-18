using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001612 RID: 5650
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Moves the character with speed. Returns Success.")]
	public class SimpleMove : Action
	{
		// Token: 0x060083DD RID: 33757 RVA: 0x002CF020 File Offset: 0x002CD220
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083DE RID: 33758 RVA: 0x0005AE61 File Offset: 0x00059061
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.characterController.SimpleMove(this.speed.Value);
			return 2;
		}

		// Token: 0x060083DF RID: 33759 RVA: 0x0005AE95 File Offset: 0x00059095
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = Vector3.zero;
		}

		// Token: 0x0400708C RID: 28812
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400708D RID: 28813
		[Tooltip("The speed of the movement")]
		public SharedVector3 speed;

		// Token: 0x0400708E RID: 28814
		private CharacterController characterController;

		// Token: 0x0400708F RID: 28815
		private GameObject prevGameObject;
	}
}
