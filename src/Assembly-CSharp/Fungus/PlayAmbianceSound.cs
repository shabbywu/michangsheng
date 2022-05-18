using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001258 RID: 4696
	[CommandInfo("Audio", "Play Ambiance Sound", "Plays a background sound to be overlayed on top of the music. Only one Ambiance can be played at a time.", 0)]
	[AddComponentMenu("")]
	public class PlayAmbianceSound : Command
	{
		// Token: 0x06007211 RID: 29201 RVA: 0x00011424 File Offset: 0x0000F624
		protected virtual void DoWait()
		{
			this.Continue();
		}

		// Token: 0x06007212 RID: 29202 RVA: 0x0004D9A8 File Offset: 0x0004BBA8
		public override void OnEnter()
		{
			if (this.soundClip == null)
			{
				this.Continue();
				return;
			}
			FungusManager.Instance.MusicManager.PlayAmbianceSound(this.soundClip, this.loop, this.volume);
			this.Continue();
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x0004D9E6 File Offset: 0x0004BBE6
		public override string GetSummary()
		{
			if (this.soundClip == null)
			{
				return "Error: No sound clip selected";
			}
			return this.soundClip.name;
		}

		// Token: 0x06007214 RID: 29204 RVA: 0x0004C749 File Offset: 0x0004A949
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x04006466 RID: 25702
		[Tooltip("Sound effect clip to play")]
		[SerializeField]
		protected AudioClip soundClip;

		// Token: 0x04006467 RID: 25703
		[Range(0f, 1f)]
		[Tooltip("Volume level of the sound effect")]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x04006468 RID: 25704
		[Tooltip("Sound effect clip to play")]
		[SerializeField]
		protected bool loop;
	}
}
