using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE6 RID: 4070
	[TaskDescription("Returns success when a collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasEnteredCollision : Conditional
	{
		// Token: 0x060070C2 RID: 28866 RVA: 0x002AA821 File Offset: 0x002A8A21
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredCollision)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070C3 RID: 28867 RVA: 0x002AA82E File Offset: 0x002A8A2E
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x060070C4 RID: 28868 RVA: 0x002AA838 File Offset: 0x002A8A38
		public override void OnCollisionEnter(Collision collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(collision.gameObject.tag))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x060070C5 RID: 28869 RVA: 0x002AA88C File Offset: 0x002A8A8C
		public override void OnReset()
		{
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x04005CD1 RID: 23761
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04005CD2 RID: 23762
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04005CD3 RID: 23763
		private bool enteredCollision;
	}
}
