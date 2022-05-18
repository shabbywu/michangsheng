using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x0200149F RID: 5279
	[TaskDescription("Returns success when a 2D collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasEnteredCollision2D : Conditional
	{
		// Token: 0x06007EC1 RID: 32449 RVA: 0x00055D1B File Offset: 0x00053F1B
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007EC2 RID: 32450 RVA: 0x00055D28 File Offset: 0x00053F28
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x002C9540 File Offset: 0x002C7740
		public override void OnCollisionEnter2D(Collision2D collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x06007EC4 RID: 32452 RVA: 0x00055D31 File Offset: 0x00053F31
		public override void OnReset()
		{
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x04006BCC RID: 27596
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04006BCD RID: 27597
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04006BCE RID: 27598
		private bool enteredCollision;
	}
}
