using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x0200157F RID: 5503
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetInt : Action
	{
		// Token: 0x060081E5 RID: 33253 RVA: 0x00058F07 File Offset: 0x00057107
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetInt(this.key.Value, this.value.Value);
			return 2;
		}

		// Token: 0x060081E6 RID: 33254 RVA: 0x00058F25 File Offset: 0x00057125
		public override void OnReset()
		{
			this.key = "";
			this.value = 0;
		}

		// Token: 0x04006E91 RID: 28305
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04006E92 RID: 28306
		[Tooltip("The value to set")]
		public SharedInt value;
	}
}
