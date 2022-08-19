using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010C2 RID: 4290
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Retruns success if the specified key exists.")]
	public class HasKey : Conditional
	{
		// Token: 0x060073E3 RID: 29667 RVA: 0x002B0DB5 File Offset: 0x002AEFB5
		public override TaskStatus OnUpdate()
		{
			if (!PlayerPrefs.HasKey(this.key.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060073E4 RID: 29668 RVA: 0x002B0DCC File Offset: 0x002AEFCC
		public override void OnReset()
		{
			this.key = "";
		}

		// Token: 0x04005F8E RID: 24462
		[Tooltip("The key to check")]
		public SharedString key;
	}
}
