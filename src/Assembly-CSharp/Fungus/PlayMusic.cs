using System;
using UnityEngine;
using YSGame;

namespace Fungus
{
	// Token: 0x0200125A RID: 4698
	[CommandInfo("Audio", "Play Music", "Plays looping game music. If any game music is already playing, it is stopped. Game music will continue playing across scene loads.", 0)]
	[AddComponentMenu("")]
	public class PlayMusic : Command
	{
		// Token: 0x0600721B RID: 29211 RVA: 0x002A7818 File Offset: 0x002A5A18
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

		// Token: 0x0600721C RID: 29212 RVA: 0x0004DA5E File Offset: 0x0004BC5E
		public override string GetSummary()
		{
			if (this.musicClip == null)
			{
				return "Error: No music clip selected";
			}
			return this.musicClip.name;
		}

		// Token: 0x0600721D RID: 29213 RVA: 0x0004C749 File Offset: 0x0004A949
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x0400646D RID: 25709
		[Tooltip("Music sound clip to play")]
		[SerializeField]
		protected AudioClip musicClip;

		// Token: 0x0400646E RID: 25710
		[Tooltip("Time to begin playing in seconds. If the audio file is compressed, the time index may be inaccurate.")]
		[SerializeField]
		protected float atTime;

		// Token: 0x0400646F RID: 25711
		[Tooltip("The music will start playing again at end.")]
		[SerializeField]
		protected bool loop = true;

		// Token: 0x04006470 RID: 25712
		[Tooltip("Length of time to fade out previous playing music.")]
		[SerializeField]
		protected float fadeDuration = 1f;
	}
}
