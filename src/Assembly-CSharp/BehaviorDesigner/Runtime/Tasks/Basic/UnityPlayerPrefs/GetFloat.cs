using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x02001579 RID: 5497
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetFloat : Action
	{
		// Token: 0x060081D4 RID: 33236 RVA: 0x00058D8D File Offset: 0x00056F8D
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetFloat(this.key.Value, this.defaultValue.Value);
			return 2;
		}

		// Token: 0x060081D5 RID: 33237 RVA: 0x00058DB6 File Offset: 0x00056FB6
		public override void OnReset()
		{
			this.key = "";
			this.defaultValue = 0f;
			this.storeResult = 0f;
		}

		// Token: 0x04006E85 RID: 28293
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04006E86 RID: 28294
		[Tooltip("The default value")]
		public SharedFloat defaultValue;

		// Token: 0x04006E87 RID: 28295
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
