using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010BD RID: 4285
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Deletes all entries from the PlayerPrefs.")]
	public class DeleteAll : Action
	{
		// Token: 0x060073D5 RID: 29653 RVA: 0x002B0C7F File Offset: 0x002AEE7F
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.DeleteAll();
			return 2;
		}
	}
}
