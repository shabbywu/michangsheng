using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010BE RID: 4286
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Deletes the specified key from the PlayerPrefs.")]
	public class DeleteKey : Action
	{
		// Token: 0x060073D7 RID: 29655 RVA: 0x002B0C87 File Offset: 0x002AEE87
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.DeleteKey(this.key.Value);
			return 2;
		}

		// Token: 0x060073D8 RID: 29656 RVA: 0x002B0C9A File Offset: 0x002AEE9A
		public override void OnReset()
		{
			this.key = "";
		}

		// Token: 0x04005F84 RID: 24452
		[Tooltip("The key to delete")]
		public SharedString key;
	}
}
