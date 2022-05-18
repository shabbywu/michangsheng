using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A3 RID: 5283
	[TaskDescription("Returns success when a 2D collision ends. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasExitedCollision2D : Conditional
	{
		// Token: 0x06007ED5 RID: 32469 RVA: 0x00055E27 File Offset: 0x00054027
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x00055E34 File Offset: 0x00054034
		public override void OnEnd()
		{
			this.exitedCollision = false;
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x002C9690 File Offset: 0x002C7890
		public override void OnCollisionExit2D(Collision2D collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.exitedCollision = true;
			}
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x00055E3D File Offset: 0x0005403D
		public override void OnReset()
		{
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x04006BD8 RID: 27608
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04006BD9 RID: 27609
		[Tooltip("The object that exited the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04006BDA RID: 27610
		private bool exitedCollision;
	}
}
