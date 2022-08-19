using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE8 RID: 4072
	[TaskDescription("Returns success when an object enters the trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasEnteredTrigger : Conditional
	{
		// Token: 0x060070CC RID: 28876 RVA: 0x002AA959 File Offset: 0x002A8B59
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredTrigger)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070CD RID: 28877 RVA: 0x002AA966 File Offset: 0x002A8B66
		public override void OnEnd()
		{
			this.enteredTrigger = false;
		}

		// Token: 0x060070CE RID: 28878 RVA: 0x002AA970 File Offset: 0x002A8B70
		public override void OnTriggerEnter(Collider other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.enteredTrigger = true;
			}
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x002AA9C4 File Offset: 0x002A8BC4
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04005CD7 RID: 23767
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04005CD8 RID: 23768
		[Tooltip("The object that entered the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04005CD9 RID: 23769
		private bool enteredTrigger;
	}
}
