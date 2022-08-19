using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010BF RID: 4287
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetFloat : Action
	{
		// Token: 0x060073DA RID: 29658 RVA: 0x002B0CAC File Offset: 0x002AEEAC
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetFloat(this.key.Value, this.defaultValue.Value);
			return 2;
		}

		// Token: 0x060073DB RID: 29659 RVA: 0x002B0CD5 File Offset: 0x002AEED5
		public override void OnReset()
		{
			this.key = "";
			this.defaultValue = 0f;
			this.storeResult = 0f;
		}

		// Token: 0x04005F85 RID: 24453
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04005F86 RID: 24454
		[Tooltip("The default value")]
		public SharedFloat defaultValue;

		// Token: 0x04005F87 RID: 24455
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
