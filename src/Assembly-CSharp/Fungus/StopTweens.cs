using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E54 RID: 3668
	[CommandInfo("iTween", "Stop Tweens", "Stop all active iTweens in the current scene.", 0)]
	[AddComponentMenu("")]
	public class StopTweens : Command
	{
		// Token: 0x0600670E RID: 26382 RVA: 0x0028883F File Offset: 0x00286A3F
		public override void OnEnter()
		{
			iTween.Stop();
			this.Continue();
		}
	}
}
