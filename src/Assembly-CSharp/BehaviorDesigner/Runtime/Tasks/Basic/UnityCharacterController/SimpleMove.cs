using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001153 RID: 4435
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Moves the character with speed. Returns Success.")]
	public class SimpleMove : Action
	{
		// Token: 0x060075E3 RID: 30179 RVA: 0x002B554C File Offset: 0x002B374C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075E4 RID: 30180 RVA: 0x002B558C File Offset: 0x002B378C
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

		// Token: 0x060075E5 RID: 30181 RVA: 0x002B55C0 File Offset: 0x002B37C0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = Vector3.zero;
		}

		// Token: 0x04006169 RID: 24937
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400616A RID: 24938
		[Tooltip("The speed of the movement")]
		public SharedVector3 speed;

		// Token: 0x0400616B RID: 24939
		private CharacterController characterController;

		// Token: 0x0400616C RID: 24940
		private GameObject prevGameObject;
	}
}
