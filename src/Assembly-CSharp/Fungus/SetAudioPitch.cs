using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E33 RID: 3635
	[CommandInfo("Audio", "Set Audio Pitch", "Sets the global pitch level for audio played with Play Music and Play Sound commands.", 0)]
	[AddComponentMenu("")]
	public class SetAudioPitch : Command
	{
		// Token: 0x06006661 RID: 26209 RVA: 0x00286384 File Offset: 0x00284584
		public override void OnEnter()
		{
			Action onComplete = delegate()
			{
				if (this.waitUntilFinished)
				{
					this.Continue();
				}
			};
			FungusManager.Instance.MusicManager.SetAudioPitch(this.pitch, this.fadeDuration, onComplete);
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x002863C8 File Offset: 0x002845C8
		public override string GetSummary()
		{
			return string.Concat(new object[]
			{
				"Set to ",
				this.pitch,
				" over ",
				this.fadeDuration,
				" seconds."
			});
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x0027DDC5 File Offset: 0x0027BFC5
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x040057C7 RID: 22471
		[Range(0f, 1f)]
		[Tooltip("Global pitch level for audio played using the Play Music and Play Sound commands")]
		[SerializeField]
		protected float pitch = 1f;

		// Token: 0x040057C8 RID: 22472
		[Range(0f, 30f)]
		[Tooltip("Time to fade between current pitch level and target pitch level.")]
		[SerializeField]
		protected float fadeDuration;

		// Token: 0x040057C9 RID: 22473
		[Tooltip("Wait until the pitch change has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;
	}
}
