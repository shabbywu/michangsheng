using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010C5 RID: 4293
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetInt : Action
	{
		// Token: 0x060073EB RID: 29675 RVA: 0x002B0E26 File Offset: 0x002AF026
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetInt(this.key.Value, this.value.Value);
			return 2;
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x002B0E44 File Offset: 0x002AF044
		public override void OnReset()
		{
			this.key = "";
			this.value = 0;
		}

		// Token: 0x04005F91 RID: 24465
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04005F92 RID: 24466
		[Tooltip("The value to set")]
		public SharedInt value;
	}
}
