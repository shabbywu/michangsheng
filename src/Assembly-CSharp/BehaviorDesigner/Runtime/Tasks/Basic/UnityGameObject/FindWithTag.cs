using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F7 RID: 5623
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Finds a GameObject by tag. Returns Success.")]
	public class FindWithTag : Action
	{
		// Token: 0x06008379 RID: 33657 RVA: 0x0005A6A2 File Offset: 0x000588A2
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = GameObject.FindWithTag(this.tag.Value);
			return 2;
		}

		// Token: 0x0600837A RID: 33658 RVA: 0x0005A6C0 File Offset: 0x000588C0
		public override void OnReset()
		{
			this.tag.Value = null;
			this.storeValue.Value = null;
		}

		// Token: 0x0400702C RID: 28716
		[Tooltip("The tag of the GameObject to find")]
		public SharedString tag;

		// Token: 0x0400702D RID: 28717
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedGameObject storeValue;
	}
}
