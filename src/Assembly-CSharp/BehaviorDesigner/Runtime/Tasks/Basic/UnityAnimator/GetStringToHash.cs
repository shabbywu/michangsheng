using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001192 RID: 4498
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Converts the state name to its corresponding hash code. Returns Success.")]
	public class GetStringToHash : Action
	{
		// Token: 0x060076DC RID: 30428 RVA: 0x002B7834 File Offset: 0x002B5A34
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = Animator.StringToHash(this.stateName.Value);
			return 2;
		}

		// Token: 0x060076DD RID: 30429 RVA: 0x002B7852 File Offset: 0x002B5A52
		public override void OnReset()
		{
			this.stateName = "";
			this.storeValue = 0;
		}

		// Token: 0x04006262 RID: 25186
		[Tooltip("The name of the state to convert to a hash code")]
		public SharedString stateName;

		// Token: 0x04006263 RID: 25187
		[Tooltip("The hash value")]
		[RequiredField]
		public SharedInt storeValue;
	}
}
