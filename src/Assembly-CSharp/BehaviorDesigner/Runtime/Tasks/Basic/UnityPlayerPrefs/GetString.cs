using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010C1 RID: 4289
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetString : Action
	{
		// Token: 0x060073E0 RID: 29664 RVA: 0x002B0D5A File Offset: 0x002AEF5A
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetString(this.key.Value, this.defaultValue.Value);
			return 2;
		}

		// Token: 0x060073E1 RID: 29665 RVA: 0x002B0D83 File Offset: 0x002AEF83
		public override void OnReset()
		{
			this.key = "";
			this.defaultValue = "";
			this.storeResult = "";
		}

		// Token: 0x04005F8B RID: 24459
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04005F8C RID: 24460
		[Tooltip("The default value")]
		public SharedString defaultValue;

		// Token: 0x04005F8D RID: 24461
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedString storeResult;
	}
}
