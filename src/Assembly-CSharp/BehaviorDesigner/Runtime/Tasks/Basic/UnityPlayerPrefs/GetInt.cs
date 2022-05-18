using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x0200157A RID: 5498
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetInt : Action
	{
		// Token: 0x060081D7 RID: 33239 RVA: 0x00058DE8 File Offset: 0x00056FE8
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetInt(this.key.Value, this.defaultValue.Value);
			return 2;
		}

		// Token: 0x060081D8 RID: 33240 RVA: 0x00058E11 File Offset: 0x00057011
		public override void OnReset()
		{
			this.key = "";
			this.defaultValue = 0;
			this.storeResult = 0;
		}

		// Token: 0x04006E88 RID: 28296
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04006E89 RID: 28297
		[Tooltip("The default value")]
		public SharedInt defaultValue;

		// Token: 0x04006E8A RID: 28298
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedInt storeResult;
	}
}
