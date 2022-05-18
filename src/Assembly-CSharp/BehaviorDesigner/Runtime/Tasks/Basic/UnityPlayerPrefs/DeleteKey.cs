using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x02001578 RID: 5496
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Deletes the specified key from the PlayerPrefs.")]
	public class DeleteKey : Action
	{
		// Token: 0x060081D1 RID: 33233 RVA: 0x00058D68 File Offset: 0x00056F68
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.DeleteKey(this.key.Value);
			return 2;
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x00058D7B File Offset: 0x00056F7B
		public override void OnReset()
		{
			this.key = "";
		}

		// Token: 0x04006E84 RID: 28292
		[Tooltip("The key to delete")]
		public SharedString key;
	}
}
