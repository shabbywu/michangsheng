using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001651 RID: 5713
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Converts the state name to its corresponding hash code. Returns Success.")]
	public class GetStringToHash : Action
	{
		// Token: 0x060084D6 RID: 34006 RVA: 0x0005C080 File Offset: 0x0005A280
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = Animator.StringToHash(this.stateName.Value);
			return 2;
		}

		// Token: 0x060084D7 RID: 34007 RVA: 0x0005C09E File Offset: 0x0005A29E
		public override void OnReset()
		{
			this.stateName = "";
			this.storeValue = 0;
		}

		// Token: 0x04007185 RID: 29061
		[Tooltip("The name of the state to convert to a hash code")]
		public SharedString stateName;

		// Token: 0x04007186 RID: 29062
		[Tooltip("The hash value")]
		[RequiredField]
		public SharedInt storeValue;
	}
}
