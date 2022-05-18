using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200160A RID: 5642
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Returns Success if the collider hit another object, otherwise Failure.")]
	public class HasColliderHit : Conditional
	{
		// Token: 0x060083BC RID: 33724 RVA: 0x0005AC15 File Offset: 0x00058E15
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060083BD RID: 33725 RVA: 0x0005AC22 File Offset: 0x00058E22
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x060083BE RID: 33726 RVA: 0x002CEE0C File Offset: 0x002CD00C
		public override void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(hit.gameObject.tag))
			{
				this.collidedGameObject.Value = hit.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x060083BF RID: 33727 RVA: 0x0005AC2B File Offset: 0x00058E2B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x0400706D RID: 28781
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400706E RID: 28782
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x0400706F RID: 28783
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04007070 RID: 28784
		private bool enteredCollision;
	}
}
