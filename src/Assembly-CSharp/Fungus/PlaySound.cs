using System;
using UnityEngine;
using YSGame;

namespace Fungus
{
	// Token: 0x0200125C RID: 4700
	[CommandInfo("Audio", "Play Sound", "Plays a once-off sound effect. Multiple sound effects can be played at the same time.", 0)]
	[AddComponentMenu("")]
	public class PlaySound : Command
	{
		// Token: 0x06007224 RID: 29220 RVA: 0x00011424 File Offset: 0x0000F624
		protected virtual void DoWait()
		{
			this.Continue();
		}

		// Token: 0x06007225 RID: 29221 RVA: 0x002A796C File Offset: 0x002A5B6C
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

		// Token: 0x06007226 RID: 29222 RVA: 0x0004DAAC File Offset: 0x0004BCAC
		public override string GetSummary()
		{
			if (this.soundClip == null)
			{
				return "Error: No sound clip selected";
			}
			return this.soundClip.name;
		}

		// Token: 0x06007227 RID: 29223 RVA: 0x0004C749 File Offset: 0x0004A949
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x04006474 RID: 25716
		[Tooltip("Sound effect clip to play")]
		[SerializeField]
		protected AudioClip soundClip;

		// Token: 0x04006475 RID: 25717
		[Range(0f, 1f)]
		[Tooltip("Volume level of the sound effect")]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x04006476 RID: 25718
		[Tooltip("Wait until the sound has finished playing before continuing execution.")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
