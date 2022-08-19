using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FEC RID: 4076
	[TaskDescription("Returns success when an object exits the trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasExitedTrigger : Conditional
	{
		// Token: 0x060070E0 RID: 28896 RVA: 0x002AABB9 File Offset: 0x002A8DB9
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedTrigger)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x002AABC6 File Offset: 0x002A8DC6
		public override void OnEnd()
		{
			this.exitedTrigger = false;
		}

		// Token: 0x060070E2 RID: 28898 RVA: 0x002AABD0 File Offset: 0x002A8DD0
		public override void OnTriggerExit(Collider other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.exitedTrigger = true;
			}
		}

		// Token: 0x060070E3 RID: 28899 RVA: 0x002AAC24 File Offset: 0x002A8E24
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04005CE3 RID: 23779
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04005CE4 RID: 23780
		[Tooltip("The object that exited the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04005CE5 RID: 23781
		private bool exitedTrigger;
	}
}
