using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200149E RID: 5278
	[TaskDescription("Returns success when a collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasEnteredCollision : Conditional
	{
		// Token: 0x06007EBC RID: 32444 RVA: 0x00055CD4 File Offset: 0x00053ED4
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x00055CE1 File Offset: 0x00053EE1
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x002C94EC File Offset: 0x002C76EC
		public override void OnCollisionEnter(Collision collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x00055CEA File Offset: 0x00053EEA
		public override void OnReset()
		{
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x04006BC9 RID: 27593
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04006BCA RID: 27594
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04006BCB RID: 27595
		private bool enteredCollision;
	}
}
