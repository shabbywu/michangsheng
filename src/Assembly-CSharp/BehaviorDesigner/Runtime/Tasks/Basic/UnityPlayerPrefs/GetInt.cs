using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010C0 RID: 4288
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetInt : Action
	{
		// Token: 0x060073DD RID: 29661 RVA: 0x002B0D07 File Offset: 0x002AEF07
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetInt(this.key.Value, this.defaultValue.Value);
			return 2;
		}

		// Token: 0x060073DE RID: 29662 RVA: 0x002B0D30 File Offset: 0x002AEF30
		public override void OnReset()
		{
			this.key = "";
			this.defaultValue = 0;
			this.storeResult = 0;
		}

		// Token: 0x04005F88 RID: 24456
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04005F89 RID: 24457
		[Tooltip("The default value")]
		public SharedInt defaultValue;

		// Token: 0x04005F8A RID: 24458
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedInt storeResult;
	}
}
