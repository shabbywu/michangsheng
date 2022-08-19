using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E0E RID: 3598
	[CommandInfo("Audio", "Play Random Sound", "Plays a once-off sound effect from a list of available sound effects. Multiple sound effects can be played at the same time.", 0)]
	[AddComponentMenu("")]
	public class PlayRandomSound : Command
	{
		// Token: 0x06006591 RID: 26001 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		protected virtual void DoWait()
		{
			this.Continue();
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x002837F8 File Offset: 0x002819F8
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

		// Token: 0x06006593 RID: 26003 RVA: 0x00283868 File Offset: 0x00281A68
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

		// Token: 0x06006594 RID: 26004 RVA: 0x0027DDC5 File Offset: 0x0027BFC5
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x0400573C RID: 22332
		[Tooltip("Sound effect clip to play")]
		[SerializeField]
		protected AudioClip[] soundClip;

		// Token: 0x0400573D RID: 22333
		[Range(0f, 1f)]
		[Tooltip("Volume level of the sound effect")]
		[SerializeField]
		protected float volume = 1f;

		// Token: 0x0400573E RID: 22334
		[Tooltip("Wait until the sound has finished playing before continuing execution.")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
