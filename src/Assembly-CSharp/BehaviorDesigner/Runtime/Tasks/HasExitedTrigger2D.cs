using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A5 RID: 5285
	[TaskDescription("Returns success when an object exits the 2D trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasExitedTrigger2D : Conditional
	{
		// Token: 0x06007EDF RID: 32479 RVA: 0x00055EB5 File Offset: 0x000540B5
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedTrigger)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007EE0 RID: 32480 RVA: 0x00055EC2 File Offset: 0x000540C2
		public override void OnEnd()
		{
			this.exitedTrigger = false;
		}

		// Token: 0x06007EE1 RID: 32481 RVA: 0x002C9738 File Offset: 0x002C7938
		public override void OnTriggerExit2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.exitedTrigger = true;
			}
		}

		// Token: 0x06007EE2 RID: 32482 RVA: 0x00055ECB File Offset: 0x000540CB
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04006BDE RID: 27614
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04006BDF RID: 27615
		[Tooltip("The object that exited the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04006BE0 RID: 27616
		private bool exitedTrigger;
	}
}
