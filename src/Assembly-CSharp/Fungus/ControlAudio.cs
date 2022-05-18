using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x020011F6 RID: 4598
	[CommandInfo("Audio", "Control Audio", "Plays, loops, or stops an audiosource. Any AudioSources with the same tag as the target Audio Source will automatically be stoped.", 0)]
	[ExecuteInEditMode]
	public class ControlAudio : Command
	{
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600708A RID: 28810 RVA: 0x0004C722 File Offset: 0x0004A922
		public virtual ControlAudioType Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x0600708B RID: 28811 RVA: 0x002A29A8 File Offset: 0x002A0BA8
		protected virtual void StopAudioWithSameTag()
		{
			if (this._audioSource.Value == null || this._audioSource.Value.tag == "Untagged")
			{
				return;
			}
			foreach (AudioSource audioSource in Object.FindObjectsOfType<AudioSource>())
			{
				if (audioSource != this._audioSource.Value && audioSource.tag == this._audioSource.Value.tag)
				{
					this.StopLoop(audioSource);
				}
			}
		}

		// Token: 0x0600708C RID: 28812 RVA: 0x002A2A34 File Offset: 0x002A0C34
		protected virtual void PlayOnce()
		{
			if (this.fadeDuration > 0f)
			{
				LeanTween.value(this._audioSource.Value.gameObject, this._audioSource.Value.volume, this.endVolume, this.fadeDuration).setOnUpdate(delegate(float updateVolume)
				{
					this._audioSource.Value.volume = updateVolume;
				});
			}
			this._audioSource.Value.PlayOneShot(this._audioSource.Value.clip);
			if (this.waitUntilFinished)
			{
				base.StartCoroutine(this.WaitAndContinue());
			}
		}

		// Token: 0x0600708D RID: 28813 RVA: 0x0004C72A File Offset: 0x0004A92A
		protected virtual IEnumerator WaitAndContinue()
		{
			while (this._audioSource.Value.isPlaying)
			{
				yield return null;
			}
			this.Continue();
			yield break;
		}

		// Token: 0x0600708E RID: 28814 RVA: 0x002A2AC8 File Offset: 0x002A0CC8
		protected virtual void PlayLoop()
		{
			if (this.fadeDuration > 0f)
			{
				this._audioSource.Value.volume = 0f;
				this._audioSource.Value.loop = true;
				this._audioSource.Value.GetComponent<AudioSource>().Play();
				LeanTween.value(this._audioSource.Value.gameObject, 0f, this.endVolume, this.fadeDuration).setOnUpdate(delegate(float updateVolume)
				{
					this._audioSource.Value.volume = updateVolume;
				}).setOnComplete(delegate()
				{
					if (this.waitUntilFinished)
					{
						this.Continue();
					}
				});
				return;
			}
			this._audioSource.Value.volume = this.endVolume;
			this._audioSource.Value.loop = true;
			this._audioSource.Value.GetComponent<AudioSource>().Play();
		}

		// Token: 0x0600708F RID: 28815 RVA: 0x002A2BA8 File Offset: 0x002A0DA8
		protected virtual void PauseLoop()
		{
			if (this.fadeDuration > 0f)
			{
				LeanTween.value(this._audioSource.Value.gameObject, this._audioSource.Value.volume, 0f, this.fadeDuration).setOnUpdate(delegate(float updateVolume)
				{
					this._audioSource.Value.volume = updateVolume;
				}).setOnComplete(delegate()
				{
					this._audioSource.Value.GetComponent<AudioSource>().Pause();
					if (this.waitUntilFinished)
					{
						this.Continue();
					}
				});
				return;
			}
			this._audioSource.Value.GetComponent<AudioSource>().Pause();
		}

		// Token: 0x06007090 RID: 28816 RVA: 0x002A2C2C File Offset: 0x002A0E2C
		protected virtual void StopLoop(AudioSource source)
		{
			if (this.fadeDuration > 0f)
			{
				LeanTween.value(source.gameObject, this._audioSource.Value.volume, 0f, this.fadeDuration).setOnUpdate(delegate(float updateVolume)
				{
					source.volume = updateVolume;
				}).setOnComplete(delegate()
				{
					source.GetComponent<AudioSource>().Stop();
					if (this.waitUntilFinished)
					{
						this.Continue();
					}
				});
				return;
			}
			source.GetComponent<AudioSource>().Stop();
		}

		// Token: 0x06007091 RID: 28817 RVA: 0x002A2CBC File Offset: 0x002A0EBC
		protected virtual void ChangeVolume()
		{
			LeanTween.value(this._audioSource.Value.gameObject, this._audioSource.Value.volume, this.endVolume, this.fadeDuration).setOnUpdate(delegate(float updateVolume)
			{
				this._audioSource.Value.volume = updateVolume;
			}).setOnComplete(delegate()
			{
				if (this.waitUntilFinished)
				{
					this.Continue();
				}
			});
		}

		// Token: 0x06007092 RID: 28818 RVA: 0x0004C739 File Offset: 0x0004A939
		protected virtual void AudioFinished()
		{
			if (this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06007093 RID: 28819 RVA: 0x002A2D20 File Offset: 0x002A0F20
		public override void OnEnter()
		{
			if (this._audioSource.Value == null)
			{
				this.Continue();
				return;
			}
			if (this.control != ControlAudioType.ChangeVolume)
			{
				this._audioSource.Value.volume = this.endVolume;
			}
			switch (this.control)
			{
			case ControlAudioType.PlayOnce:
				this.StopAudioWithSameTag();
				this.PlayOnce();
				break;
			case ControlAudioType.PlayLoop:
				this.StopAudioWithSameTag();
				this.PlayLoop();
				break;
			case ControlAudioType.PauseLoop:
				this.PauseLoop();
				break;
			case ControlAudioType.StopLoop:
				this.StopLoop(this._audioSource.Value);
				break;
			case ControlAudioType.ChangeVolume:
				this.ChangeVolume();
				break;
			}
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06007094 RID: 28820 RVA: 0x002A2DD4 File Offset: 0x002A0FD4
		public override string GetSummary()
		{
			if (this._audioSource.Value == null)
			{
				return "Error: No sound clip selected";
			}
			string text = "";
			if (this.fadeDuration > 0f)
			{
				text = " Fade out";
				if (this.control != ControlAudioType.StopLoop)
				{
					text = " Fade in volume to " + this.endVolume;
				}
				if (this.control == ControlAudioType.ChangeVolume)
				{
					text = " to " + this.endVolume;
				}
				text = string.Concat(new object[]
				{
					text,
					" over ",
					this.fadeDuration,
					" seconds."
				});
			}
			return string.Concat(new string[]
			{
				this.control.ToString(),
				" \"",
				this._audioSource.Value.name,
				"\"",
				text
			});
		}

		// Token: 0x06007095 RID: 28821 RVA: 0x0004C749 File Offset: 0x0004A949
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x06007096 RID: 28822 RVA: 0x0004C769 File Offset: 0x0004A969
		public override bool HasReference(Variable variable)
		{
			return this._audioSource.audioSourceRef == variable || base.HasReference(variable);
		}

		// Token: 0x06007097 RID: 28823 RVA: 0x0004C787 File Offset: 0x0004A987
		protected virtual void OnEnable()
		{
			if (this.audioSourceOLD != null)
			{
				this._audioSource.Value = this.audioSourceOLD;
				this.audioSourceOLD = null;
			}
		}

		// Token: 0x04006314 RID: 25364
		[Tooltip("What to do to audio")]
		[SerializeField]
		protected ControlAudioType control;

		// Token: 0x04006315 RID: 25365
		[Tooltip("Audio clip to play")]
		[SerializeField]
		protected AudioSourceData _audioSource;

		// Token: 0x04006316 RID: 25366
		[Range(0f, 1f)]
		[Tooltip("Start audio at this volume")]
		[SerializeField]
		protected float startVolume = 1f;

		// Token: 0x04006317 RID: 25367
		[Range(0f, 1f)]
		[Tooltip("End audio at this volume")]
		[SerializeField]
		protected float endVolume = 1f;

		// Token: 0x04006318 RID: 25368
		[Tooltip("Time to fade between current volume level and target volume level.")]
		[SerializeField]
		protected float fadeDuration;

		// Token: 0x04006319 RID: 25369
		[Tooltip("Wait until this command has finished before executing the next command.")]
		[SerializeField]
		protected bool waitUntilFinished;

		// Token: 0x0400631A RID: 25370
		[HideInInspector]
		[FormerlySerializedAs("audioSource")]
		public AudioSource audioSourceOLD;
	}
}
