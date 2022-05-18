using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x0200157E RID: 5502
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetFloat : Action
	{
		// Token: 0x060081E2 RID: 33250 RVA: 0x00058EC7 File Offset: 0x000570C7
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetFloat(this.key.Value, this.value.Value);
			return 2;
		}

		// Token: 0x060081E3 RID: 33251 RVA: 0x00058EE5 File Offset: 0x000570E5
		public override void OnReset()
		{
			this.key = "";
			this.value = 0f;
		}

		// Token: 0x04006E8F RID: 28303
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04006E90 RID: 28304
		[Tooltip("The value to set")]
		public SharedFloat value;
	}
}
