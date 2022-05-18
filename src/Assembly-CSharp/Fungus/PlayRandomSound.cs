using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200125B RID: 4699
	[CommandInfo("Audio", "Play Random Sound", "Plays a once-off sound effect from a list of available sound effects. Multiple sound effects can be played at the same time.", 0)]
	[AddComponentMenu("")]
	public class PlayRandomSound : Command
	{
		// Token: 0x0600721F RID: 29215 RVA: 0x00011424 File Offset: 0x0000F624
		protected virtual void DoWait()
		{
			this.Continue();
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x002A7878 File Offset: 0x002A5A78
		public override void OnEnter()
		{
			int num = Random.Range(0, this.soundClip.Length);
			if (this.soundClip == null)
			{
				this.Continue();
				return;
			}
			FungusManager.Instance.MusicManager.PlaySound(this.soundClip[num], this.volume);
			if (this.waitUntilFinished)
			{
				base.Invoke("DoWait", this.soundClip[num].length);
				return;
			}
			this.Continue();
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x002A78E8 File Offset: 0x002A5AE8
		public override string GetSummary()
		{
			if (this.soundClip == null)
			{
				return "Error: No sound clip selected";
			}
			string text = "[";
			foreach (AudioClip audioClip in this.soundClip)
			{
				if (audioClip != null)
				{
					text = text + audioClip.name + " ,";
				}
			}
			text = text.TrimEnd(new char[]
			{
				' ',
				','
			});
			text += "]";
			return "Random sounds " + text;
		}

		// Token: 0x06007222 RID: 29218 RVA: 0x0004C749 File Offset: 0x0004A949
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x04006471 RID: 25713
		[Tooltip("Sound effect clip to play")]
		[SerializeField]
		protected AudioClip[] soundClip;

		// Token: 0x04006472 RID: 25714
		[Range(0f, 1f)]
		[Tooltip("Volume level of the sound effect")]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x04006473 RID: 25715
		[Tooltip("Wait until the sound has finished playing before continuing execution.")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
