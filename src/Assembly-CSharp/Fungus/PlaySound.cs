using System;
using UnityEngine;
using YSGame;

namespace Fungus
{
	// Token: 0x02000E0F RID: 3599
	[CommandInfo("Audio", "Play Sound", "Plays a once-off sound effect. Multiple sound effects can be played at the same time.", 0)]
	[AddComponentMenu("")]
	public class PlaySound : Command
	{
		// Token: 0x06006596 RID: 26006 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		protected virtual void DoWait()
		{
			this.Continue();
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x00283900 File Offset: 0x00281B00
		public override void OnEnter()
		{
			if (this.soundClip == null)
			{
				this.Continue();
				return;
			}
			FungusManager.Instance.MusicManager.PlaySound(this.soundClip, this.volume);
			if (MusicMag.instance != null)
			{
				MusicMag.instance.setFunguseMusice();
			}
			if (this.waitUntilFinished)
			{
				base.Invoke("DoWait", this.soundClip.length);
				return;
			}
			this.Continue();
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x00283979 File Offset: 0x00281B79
		public override string GetSummary()
		{
			if (this.soundClip == null)
			{
				return "Error: No sound clip selected";
			}
			return this.soundClip.name;
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x0027DDC5 File Offset: 0x0027BFC5
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x0400573F RID: 22335
		[Tooltip("Sound effect clip to play")]
		[SerializeField]
		protected AudioClip soundClip;

		// Token: 0x04005740 RID: 22336
		[Range(0f, 1f)]
		[Tooltip("Volume level of the sound effect")]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x04005741 RID: 22337
		[Tooltip("Wait until the sound has finished playing before continuing execution.")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
