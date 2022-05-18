using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A0 RID: 5280
	[TaskDescription("Returns success when an object enters the trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasEnteredTrigger : Conditional
	{
		// Token: 0x06007EC6 RID: 32454 RVA: 0x00055D62 File Offset: 0x00053F62
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredTrigger)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007EC7 RID: 32455 RVA: 0x00055D6F File Offset: 0x00053F6F
		public override void OnEnd()
		{
			this.enteredTrigger = false;
		}

		// Token: 0x06007EC8 RID: 32456 RVA: 0x002C9594 File Offset: 0x002C7794
		public override void OnTriggerEnter(Collider other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.enteredTrigger = true;
			}
		}

		// Token: 0x06007EC9 RID: 32457 RVA: 0x00055D78 File Offset: 0x00053F78
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04006BCF RID: 27599
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04006BD0 RID: 27600
		[Tooltip("The object that entered the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04006BD1 RID: 27601
		private bool enteredTrigger;
	}
}
