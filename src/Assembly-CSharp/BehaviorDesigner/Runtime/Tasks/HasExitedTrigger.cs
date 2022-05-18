using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A4 RID: 5284
	[TaskDescription("Returns success when an object exits the trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasExitedTrigger : Conditional
	{
		// Token: 0x06007EDA RID: 32474 RVA: 0x00055E6E File Offset: 0x0005406E
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedTrigger)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x00055E7B File Offset: 0x0005407B
		public override void OnEnd()
		{
			this.exitedTrigger = false;
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x002C96E4 File Offset: 0x002C78E4
		public override void OnTriggerExit(Collider other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.exitedTrigger = true;
			}
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x00055E84 File Offset: 0x00054084
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04006BDB RID: 27611
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04006BDC RID: 27612
		[Tooltip("The object that exited the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04006BDD RID: 27613
		private bool exitedTrigger;
	}
}
