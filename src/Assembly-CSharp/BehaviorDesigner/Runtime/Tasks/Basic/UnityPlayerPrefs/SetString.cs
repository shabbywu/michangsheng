using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010C6 RID: 4294
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetString : Action
	{
		// Token: 0x060073EE RID: 29678 RVA: 0x002B0E62 File Offset: 0x002AF062
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetString(this.key.Value, this.value.Value);
			return 2;
		}

		// Token: 0x060073EF RID: 29679 RVA: 0x002B0E80 File Offset: 0x002AF080
		public override void OnReset()
		{
			this.key = "";
			this.value = "";
		}

		// Token: 0x04005F93 RID: 24467
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04005F94 RID: 24468
		[Tooltip("The value to set")]
		public SharedString value;
	}
}
