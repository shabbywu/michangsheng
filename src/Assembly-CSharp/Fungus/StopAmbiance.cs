using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200129F RID: 4767
	[CommandInfo("Audio", "Stop Ambiance", "Stops the currently playing game ambiance.", 0)]
	[AddComponentMenu("")]
	public class StopAmbiance : Command
	{
		// Token: 0x06007386 RID: 29574 RVA: 0x0004ED0F File Offset: 0x0004CF0F
		public override void OnEnter()
		{
			FungusManager.Instance.MusicManager.StopAmbiance();
			this.Continue();
		}

		// Token: 0x06007387 RID: 29575 RVA: 0x0004C749 File Offset: 0x0004A949
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}
	}
}
