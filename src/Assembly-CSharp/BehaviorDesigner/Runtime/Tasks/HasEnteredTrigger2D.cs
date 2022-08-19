using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FE9 RID: 4073
	[TaskDescription("Returns success when an object enters the 2D trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=110")]
	public class HasEnteredTrigger2D : Conditional
	{
		// Token: 0x060070D1 RID: 28881 RVA: 0x002AA9F5 File Offset: 0x002A8BF5
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredTrigger)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060070D2 RID: 28882 RVA: 0x002AAA02 File Offset: 0x002A8C02
		public override void OnEnd()
		{
			this.enteredTrigger = false;
		}

		// Token: 0x060070D3 RID: 28883 RVA: 0x002AAA0C File Offset: 0x002A8C0C
		public override void OnTriggerEnter2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.enteredTrigger = true;
			}
		}

		// Token: 0x060070D4 RID: 28884 RVA: 0x002AAA60 File Offset: 0x002A8C60
		public override void OnReset()
		{
			this.tag = "";
			this.otherGameObject = null;
		}

		// Token: 0x04005CDA RID: 23770
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = "";

		// Token: 0x04005CDB RID: 23771
		[Tooltip("The object that entered the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04005CDC RID: 23772
		private bool enteredTrigger;
	}
}
