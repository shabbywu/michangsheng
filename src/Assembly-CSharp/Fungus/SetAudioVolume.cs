using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001285 RID: 4741
	[CommandInfo("Audio", "Set Audio Volume", "Sets the global volume level for audio played with Play Music and Play Sound commands.", 0)]
	[AddComponentMenu("")]
	public class SetAudioVolume : Command
	{
		// Token: 0x060072F4 RID: 29428 RVA: 0x0004E4D3 File Offset: 0x0004C6D3
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

		// Token: 0x060072F5 RID: 29429 RVA: 0x002A9A80 File Offset: 0x002A7C80
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

		// Token: 0x060072F6 RID: 29430 RVA: 0x0004C749 File Offset: 0x0004A949
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x0400650E RID: 25870
		[Range(0f, 1f)]
		[Tooltip("Global volume level for audio played using Play Music and Play Sound")]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x0400650F RID: 25871
		[Range(0f, 30f)]
		[Tooltip("Time to fade between current volume level and target volume level.")]
		[SerializeField]
		protected float fadeDuration = 1f;

		// Token: 0x04006510 RID: 25872
		[Tooltip("Wait until the volume fade has completed before continuing.")]
		[SerializeField]
		protected bool waitUntilFinished = true;
	}
}
