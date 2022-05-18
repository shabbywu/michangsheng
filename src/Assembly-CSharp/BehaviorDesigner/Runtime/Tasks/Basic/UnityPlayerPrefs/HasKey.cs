using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x0200157C RID: 5500
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Retruns success if the specified key exists.")]
	public class HasKey : Conditional
	{
		// Token: 0x060081DD RID: 33245 RVA: 0x00058E96 File Offset: 0x00057096
		public override TaskStatus OnUpdate()
		{
			if (!PlayerPrefs.HasKey(this.key.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060081DE RID: 33246 RVA: 0x00058EAD File Offset: 0x000570AD
		public override void OnReset()
		{
			this.key = "";
		}

		// Token: 0x04006E8E RID: 28302
		[Tooltip("The key to check")]
		public SharedString key;
	}
}
