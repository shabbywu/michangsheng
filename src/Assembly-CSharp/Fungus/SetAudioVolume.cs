using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E34 RID: 3636
	[CommandInfo("Audio", "Set Audio Volume", "Sets the global volume level for audio played with Play Music and Play Sound commands.", 0)]
	[AddComponentMenu("")]
	public class SetAudioVolume : Command
	{
		// Token: 0x06006666 RID: 26214 RVA: 0x0028643E File Offset: 0x0028463E
		public override void OnEnter()
		{
			FungusManager.Instance.MusicManager.SetAudioVolume(this.volume, this.fadeDuration, delegate
			{
				if (this.waitUntilFinished)
				{
					this.Continue();
				}
			});
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x00286478 File Offset: 0x00284678
		public override string GetSummary()
		{
			return string.Concat(new object[]
			{
				"Set to ",
				this.volume,
				" over ",
				this.fadeDuration,
				" seconds."
			});
		}

		// Token: 0x06006668 RID: 26216 RVA: 0x0027DDC5 File Offset: 0x0027BFC5
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x040057CA RID: 22474
		[Range(0f, 1f)]
		[Tooltip("Global volume level for audio played using Play Music and Play Sound")]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x040057CB RID: 22475
		[Range(0f, 30f)]
		[Tooltip("Time to fade between current volume level and target volume level.")]
		[SerializeField]
		protected float fadeDuration = 1f;

		// Token: 0x040057CC RID: 22476
		[Tooltip("Wait until the volume fade has completed before continuing.")]
		[SerializeField]
		protected bool waitUntilFinished = true;
	}
}
