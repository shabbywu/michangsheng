using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPlayerPrefs
{
	// Token: 0x02001577 RID: 5495
	[TaskCategory("Basic/PlayerPrefs")]
	[TaskDescription("Deletes all entries from the PlayerPrefs.")]
	public class DeleteAll : Action
	{
		// Token: 0x060081CF RID: 33231 RVA: 0x00058D60 File Offset: 0x00056F60
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.DeleteAll();
			return 2;
		}
	}
}
