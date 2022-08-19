using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE7 RID: 4071
	[TaskDescription("Returns success when a 2D collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasEnteredCollision2D : Conditional
	{
		// Token: 0x060070C7 RID: 28871 RVA: 0x002AA8BD File Offset: 0x002A8ABD
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070C8 RID: 28872 RVA: 0x002AA8CA File Offset: 0x002A8ACA
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x060070C9 RID: 28873 RVA: 0x002AA8D4 File Offset: 0x002A8AD4
		public override void OnCollisionEnter2D(Collision2D collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x060070CA RID: 28874 RVA: 0x002AA928 File Offset: 0x002A8B28
		public override void OnReset()
		{
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x04005CD4 RID: 23764
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04005CD5 RID: 23765
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04005CD6 RID: 23766
		private bool enteredCollision;
	}
}
