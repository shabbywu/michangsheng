using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200114B RID: 4427
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Returns Success if the collider hit another object, otherwise Failure.")]
	public class HasColliderHit : Conditional
	{
		// Token: 0x060075C2 RID: 30146 RVA: 0x002B50E4 File Offset: 0x002B32E4
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060075C3 RID: 30147 RVA: 0x002B50F1 File Offset: 0x002B32F1
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x060075C4 RID: 30148 RVA: 0x002B50FC File Offset: 0x002B32FC
		public override void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(hit.gameObject.tag))
			{
				this.collidedGameObject.Value = hit.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x060075C5 RID: 30149 RVA: 0x002B5150 File Offset: 0x002B3350
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x0400614A RID: 24906
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400614B RID: 24907
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x0400614C RID: 24908
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x0400614D RID: 24909
		private bool enteredCollision;
	}
}
