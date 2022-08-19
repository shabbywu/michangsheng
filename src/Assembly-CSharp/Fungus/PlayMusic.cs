using System;
using UnityEngine;
using YSGame;

namespace Fungus
{
	// Token: 0x02000E0D RID: 3597
	[CommandInfo("Audio", "Play Music", "Plays looping game music. If any game music is already playing, it is stopped. Game music will continue playing across scene loads.", 0)]
	[AddComponentMenu("")]
	public class PlayMusic : Command
	{
		// Token: 0x0600658D RID: 25997 RVA: 0x00283760 File Offset: 0x00281960
		public override void OnEnter()
		{
			MusicManager musicManager = FungusManager.Instance.MusicManager;
			if (MusicMag.instance != null)
			{
				MusicMag.instance.setFunguseMusice();
			}
			float num = Mathf.Max(0f, this.atTime);
			musicManager.PlayMusic(this.musicClip, this.loop, this.fadeDuration, num);
			this.Continue();
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x002837BD File Offset: 0x002819BD
		public override string GetSummary()
		{
			if (this.musicClip == null)
			{
				return "Error: No music clip selected";
			}
			return this.musicClip.name;
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x0027DDC5 File Offset: 0x0027BFC5
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x04005738 RID: 22328
		[Tooltip("Music sound clip to play")]
		[SerializeField]
		protected AudioClip musicClip;

		// Token: 0x04005739 RID: 22329
		[Tooltip("Time to begin playing in seconds. If the audio file is compressed, the time index may be inaccurate.")]
		[SerializeField]
		protected float atTime;

		// Token: 0x0400573A RID: 22330
		[Tooltip("The music will start playing again at end.")]
		[SerializeField]
		protected bool loop = true;

		// Token: 0x0400573B RID: 22331
		[Tooltip("Length of time to fade out previous playing music.")]
		[SerializeField]
		protected float fadeDuration = 1f;
	}
}
