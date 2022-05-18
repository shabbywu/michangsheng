using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A1 RID: 5281
	[TaskDescription("Returns success when an object enters the 2D trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasEnteredTrigger2D : Conditional
	{
		// Token: 0x06007ECB RID: 32459 RVA: 0x00055DA9 File Offset: 0x00053FA9
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredTrigger)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007ECC RID: 32460 RVA: 0x00055DB6 File Offset: 0x00053FB6
		public override void OnEnd()
		{
			this.enteredTrigger = false;
		}

		// Token: 0x06007ECD RID: 32461 RVA: 0x002C95E8 File Offset: 0x002C77E8
		public override void OnTriggerEnter2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.enteredTrigger = true;
			}
		}

		// Token: 0x06007ECE RID: 32462 RVA: 0x00055DBF File Offset: 0x00053FBF
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04006BD2 RID: 27602
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04006BD3 RID: 27603
		[Tooltip("The object that entered the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04006BD4 RID: 27604
		private bool enteredTrigger;
	}
}
