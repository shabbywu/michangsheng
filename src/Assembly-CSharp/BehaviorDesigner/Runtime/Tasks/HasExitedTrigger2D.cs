using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FED RID: 4077
	[TaskDescription("Returns success when an object exits the 2D trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasExitedTrigger2D : Conditional
	{
		// Token: 0x060070E5 RID: 28901 RVA: 0x002AAC55 File Offset: 0x002A8E55
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedTrigger)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070E6 RID: 28902 RVA: 0x002AAC62 File Offset: 0x002A8E62
		public override void OnEnd()
		{
			this.exitedTrigger = false;
		}

		// Token: 0x060070E7 RID: 28903 RVA: 0x002AAC6C File Offset: 0x002A8E6C
		public override void OnTriggerExit2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.exitedTrigger = true;
			}
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x002AACC0 File Offset: 0x002A8EC0
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04005CE6 RID: 23782
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04005CE7 RID: 23783
		[Tooltip("The object that exited the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04005CE8 RID: 23784
		private bool exitedTrigger;
	}
}
