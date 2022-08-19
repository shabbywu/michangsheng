using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010C4 RID: 4292
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetFloat : Action
	{
		// Token: 0x060073E8 RID: 29672 RVA: 0x002B0DE6 File Offset: 0x002AEFE6
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetFloat(this.key.Value, this.value.Value);
			return 2;
		}

		// Token: 0x060073E9 RID: 29673 RVA: 0x002B0E04 File Offset: 0x002AF004
		public override void OnReset()
		{
			this.key = "";
			this.value = 0f;
		}

		// Token: 0x04005F8F RID: 24463
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04005F90 RID: 24464
		[Tooltip("The value to set")]
		public SharedFloat value;
	}
}
