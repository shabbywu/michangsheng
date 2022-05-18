using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x0200157D RID: 5501
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Saves the PlayerPrefs.")]
	public class Save : Action
	{
		// Token: 0x060081E0 RID: 33248 RVA: 0x00058EBF File Offset: 0x000570BF
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.Save();
			return 2;
		}
	}
}
