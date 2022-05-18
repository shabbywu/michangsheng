using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A2 RID: 5282
	[TaskDescription("Returns success when a collision ends. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasExitedCollision : Conditional
	{
		// Token: 0x06007ED0 RID: 32464 RVA: 0x00055DF0 File Offset: 0x00053FF0
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007ED1 RID: 32465 RVA: 0x00055DFD File Offset: 0x00053FFD
		public override void OnEnd()
		{
			this.exitedCollision = false;
		}

		// Token: 0x06007ED2 RID: 32466 RVA: 0x002C963C File Offset: 0x002C783C
		public override void OnCollisionExit(Collision collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.exitedCollision = true;
			}
		}

		// Token: 0x06007ED3 RID: 32467 RVA: 0x00055E06 File Offset: 0x00054006
		public override void OnReset()
		{
			this.collidedGameObject = null;
		}

		// Token: 0x04006BD5 RID: 27605
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04006BD6 RID: 27606
		[Tooltip("The object that exited the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04006BD7 RID: 27607
		private bool exitedCollision;
	}
}
