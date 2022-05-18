using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x02001580 RID: 5504
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetString : Action
	{
		// Token: 0x060081E8 RID: 33256 RVA: 0x00058F43 File Offset: 0x00057143
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetString(this.key.Value, this.value.Value);
			return 2;
		}

		// Token: 0x060081E9 RID: 33257 RVA: 0x00058F61 File Offset: 0x00057161
		public override void OnReset()
		{
			this.key = "";
			this.value = "";
		}

		// Token: 0x04006E93 RID: 28307
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04006E94 RID: 28308
		[Tooltip("The value to set")]
		public SharedString value;
	}
}
