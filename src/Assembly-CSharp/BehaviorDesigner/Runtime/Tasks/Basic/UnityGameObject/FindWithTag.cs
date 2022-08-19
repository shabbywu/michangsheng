using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001138 RID: 4408
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Finds a GameObject by tag. Returns Success.")]
	public class FindWithTag : Action
	{
		// Token: 0x0600757F RID: 30079 RVA: 0x002B475E File Offset: 0x002B295E
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = GameObject.FindWithTag(this.tag.Value);
			return 2;
		}

		// Token: 0x06007580 RID: 30080 RVA: 0x002B477C File Offset: 0x002B297C
		public override void OnReset()
		{
			this.tag.Value = null;
			this.storeValue.Value = null;
		}

		// Token: 0x04006109 RID: 24841
		[Tooltip("The tag of the GameObject to find")]
		public SharedString tag;

		// Token: 0x0400610A RID: 24842
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedGameObject storeValue;
	}
}
