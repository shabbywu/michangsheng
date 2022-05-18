using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012A2 RID: 4770
	[CommandInfo("Audio", "Stop Music", "Stops the currently playing game music.", 0)]
	[AddComponentMenu("")]
	public class StopMusic : Command
	{
		// Token: 0x06007392 RID: 29586 RVA: 0x0004ED8E File Offset: 0x0004CF8E
		public override void OnEnter()
		{
			FungusManager.Instance.MusicManager.StopMusic();
			this.Continue();
		}

		// Token: 0x06007393 RID: 29587 RVA: 0x0004C749 File Offset: 0x0004A949
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}
	}
}
