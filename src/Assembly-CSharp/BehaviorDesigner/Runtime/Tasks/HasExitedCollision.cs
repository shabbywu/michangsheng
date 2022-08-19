using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FEA RID: 4074
	[TaskDescription("Returns success when a collision ends. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasExitedCollision : Conditional
	{
		// Token: 0x060070D6 RID: 28886 RVA: 0x002AAA91 File Offset: 0x002A8C91
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070D7 RID: 28887 RVA: 0x002AAA9E File Offset: 0x002A8C9E
		public override void OnEnd()
		{
			this.exitedCollision = false;
		}

		// Token: 0x060070D8 RID: 28888 RVA: 0x002AAAA8 File Offset: 0x002A8CA8
		public override void OnCollisionExit(Collision collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.exitedCollision = true;
			}
		}

		// Token: 0x060070D9 RID: 28889 RVA: 0x002AAAFC File Offset: 0x002A8CFC
		public override void OnReset()
		{
			this.collidedGameObject = null;
		}

		// Token: 0x04005CDD RID: 23773
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04005CDE RID: 23774
		[Tooltip("The object that exited the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04005CDF RID: 23775
		private bool exitedCollision;
	}
}
