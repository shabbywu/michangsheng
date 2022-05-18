using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001284 RID: 4740
	[CommandInfo("Audio", "Set Audio Pitch", "Sets the global pitch level for audio played with Play Music and Play Sound commands.", 0)]
	[AddComponentMenu("")]
	public class SetAudioPitch : Command
	{
		// Token: 0x060072EF RID: 29423 RVA: 0x002A99F0 File Offset: 0x002A7BF0
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

		// Token: 0x060072F0 RID: 29424 RVA: 0x002A9A34 File Offset: 0x002A7C34
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

		// Token: 0x060072F1 RID: 29425 RVA: 0x0004C749 File Offset: 0x0004A949
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x0400650B RID: 25867
		[Range(0f, 1f)]
		[Tooltip("Global pitch level for audio played using the Play Music and Play Sound commands")]
		[SerializeField]
		protected float pitch = 1f;

		// Token: 0x0400650C RID: 25868
		[Range(0f, 30f)]
		[Tooltip("Time to fade between current pitch level and target pitch level.")]
		[SerializeField]
		protected float fadeDuration;

		// Token: 0x0400650D RID: 25869
		[Tooltip("Wait until the pitch change has finished before executing next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;
	}
}
