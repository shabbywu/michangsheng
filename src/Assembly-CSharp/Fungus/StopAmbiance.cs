using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E4E RID: 3662
	[CommandInfo("Audio", "Stop Ambiance", "Stops the currently playing game ambiance.", 0)]
	[AddComponentMenu("")]
	public class StopAmbiance : Command
	{
		// Token: 0x060066F8 RID: 26360 RVA: 0x00288647 File Offset: 0x00286847
		public override void OnEnter()
		{
			FungusManager.Instance.MusicManager.StopAmbiance();
			this.Continue();
		}

		// Token: 0x060066F9 RID: 26361 RVA: 0x0027DDC5 File Offset: 0x0027BFC5
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}
	}
}
