using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E51 RID: 3665
	[CommandInfo("Audio", "Stop Music", "Stops the currently playing game music.", 0)]
	[AddComponentMenu("")]
	public class StopMusic : Command
	{
		// Token: 0x06006704 RID: 26372 RVA: 0x00288798 File Offset: 0x00286998
		public override void OnEnter()
		{
			FungusManager.Instance.MusicManager.StopMusic();
			this.Continue();
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x0027DDC5 File Offset: 0x0027BFC5
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}
	}
}
