using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x0200157B RID: 5499
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetString : Action
	{
		// Token: 0x060081DA RID: 33242 RVA: 0x00058E3B File Offset: 0x0005703B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetString(this.key.Value, this.defaultValue.Value);
			return 2;
		}

		// Token: 0x060081DB RID: 33243 RVA: 0x00058E64 File Offset: 0x00057064
		public override void OnReset()
		{
			this.key = "";
			this.defaultValue = "";
			this.storeResult = "";
		}

		// Token: 0x04006E8B RID: 28299
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04006E8C RID: 28300
		[Tooltip("The default value")]
		public SharedString defaultValue;

		// Token: 0x04006E8D RID: 28301
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedString storeResult;
	}
}
