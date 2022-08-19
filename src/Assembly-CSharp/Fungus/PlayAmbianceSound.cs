using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E0B RID: 3595
	[CommandInfo("Audio", "Play Ambiance Sound", "Plays a background sound to be overlayed on top of the music. Only one Ambiance can be played at a time.", 0)]
	[AddComponentMenu("")]
	public class PlayAmbianceSound : Command
	{
		// Token: 0x06006583 RID: 25987 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		protected virtual void DoWait()
		{
			this.Continue();
		}

		// Token: 0x06006584 RID: 25988 RVA: 0x0028359E File Offset: 0x0028179E
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

		// Token: 0x06006585 RID: 25989 RVA: 0x002835DC File Offset: 0x002817DC
		public override string GetSummary()
		{
			if (this.soundClip == null)
			{
				return "Error: No sound clip selected";
			}
			return this.soundClip.name;
		}

		// Token: 0x06006586 RID: 25990 RVA: 0x0027DDC5 File Offset: 0x0027BFC5
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x04005731 RID: 22321
		[Tooltip("Sound effect clip to play")]
		[SerializeField]
		protected AudioClip soundClip;

		// Token: 0x04005732 RID: 22322
		[Range(0f, 1f)]
		[Tooltip("Volume level of the sound effect")]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x04005733 RID: 22323
		[Tooltip("Sound effect clip to play")]
		[SerializeField]
		protected bool loop;
	}
}
