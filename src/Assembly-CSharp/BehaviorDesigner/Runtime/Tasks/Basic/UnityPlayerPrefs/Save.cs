using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x020010C3 RID: 4291
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Saves the PlayerPrefs.")]
	public class Save : Action
	{
		// Token: 0x060073E6 RID: 29670 RVA: 0x002B0DDE File Offset: 0x002AEFDE
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.Save();
			return 2;
		}
	}
}
