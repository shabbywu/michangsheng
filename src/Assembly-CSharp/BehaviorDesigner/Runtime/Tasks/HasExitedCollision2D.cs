using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FEB RID: 4075
	[TaskDescription("Returns success when a 2D collision ends. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasExitedCollision2D : Conditional
	{
		// Token: 0x060070DB RID: 28891 RVA: 0x002AAB1D File Offset: 0x002A8D1D
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070DC RID: 28892 RVA: 0x002AAB2A File Offset: 0x002A8D2A
		public override void OnEnd()
		{
			this.exitedCollision = false;
		}

		// Token: 0x060070DD RID: 28893 RVA: 0x002AAB34 File Offset: 0x002A8D34
		public override void OnCollisionExit2D(Collision2D collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.exitedCollision = true;
			}
		}

		// Token: 0x060070DE RID: 28894 RVA: 0x002AAB88 File Offset: 0x002A8D88
		public override void OnReset()
		{
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x04005CE0 RID: 23776
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04005CE1 RID: 23777
		[Tooltip("The object that exited the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04005CE2 RID: 23778
		private bool exitedCollision;
	}
}
