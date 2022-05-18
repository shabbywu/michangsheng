using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012A5 RID: 4773
	[CommandInfo("iTween", "Stop Tweens", "Stop all active iTweens in the current scene.", 0)]
	[AddComponentMenu("")]
	public class StopTweens : Command
	{
		// Token: 0x0600739C RID: 29596 RVA: 0x0004EE35 File Offset: 0x0004D035
		public override void OnEnter()
		{
			iTween.Stop();
			this.Continue();
		}
	}
}
