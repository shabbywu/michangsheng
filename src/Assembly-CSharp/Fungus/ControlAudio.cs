using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000DC0 RID: 3520
	[CommandInfo("Audio", "Control Audio", "Plays, loops, or stops an audiosource. Any AudioSources with the same tag as the target Audio Source will automatically be stoped.", 0)]
	[ExecuteInEditMode]
	public class ControlAudio : Command
	{
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600641D RID: 25629 RVA: 0x0027D87F File Offset: 0x0027BA7F
		public virtual ControlAudioType Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x0027D888 File Offset: 0x0027BA88
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

		// Token: 0x0600641F RID: 25631 RVA: 0x0027D914 File Offset: 0x0027BB14
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

		// Token: 0x06006420 RID: 25632 RVA: 0x0027D9A6 File Offset: 0x0027BBA6
		protected virtual IEnumerator WaitAndContinue()
		{
			while (this._audioSource.Value.isPlaying)
			{
				yield return null;
			}
			this.Continue();
			yield break;
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x0027D9B8 File Offset: 0x0027BBB8
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

		// Token: 0x06006422 RID: 25634 RVA: 0x0027DA98 File Offset: 0x0027BC98
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

		// Token: 0x06006423 RID: 25635 RVA: 0x0027DB1C File Offset: 0x0027BD1C
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

		// Token: 0x06006424 RID: 25636 RVA: 0x0027DBAC File Offset: 0x0027BDAC
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

		// Token: 0x06006425 RID: 25637 RVA: 0x0027DC0D File Offset: 0x0027BE0D
		protected virtual void AudioFinished()
		{
			if (this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x0027DC20 File Offset: 0x0027BE20
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

		// Token: 0x06006427 RID: 25639 RVA: 0x0027DCD4 File Offset: 0x0027BED4
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

		// Token: 0x06006428 RID: 25640 RVA: 0x0027DDC5 File Offset: 0x0027BFC5
		public override Color GetButtonColor()
		{
			return new Color32(242, 209, 176, byte.MaxValue);
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x0027DDE5 File Offset: 0x0027BFE5
		public override bool HasReference(Variable variable)
		{
			return this._audioSource.audioSourceRef == variable || base.HasReference(variable);
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x0027DE03 File Offset: 0x0027C003
		protected virtual void OnEnable()
		{
			if (this.audioSourceOLD != null)
			{
				this._audioSource.Value = this.audioSourceOLD;
				this.audioSourceOLD = null;
			}
		}

		// Token: 0x0400561F RID: 22047
		[Tooltip("What to do to audio")]
		[SerializeField]
		protected ControlAudioType control;

		// Token: 0x04005620 RID: 22048
		[Tooltip("Audio clip to play")]
		[SerializeField]
		protected AudioSourceData _audioSource;

		// Token: 0x04005621 RID: 22049
		[Range(0f, 1f)]
		[Tooltip("Start audio at this volume")]
		[SerializeField]
		protected float startVolume = 1f;

		// Token: 0x04005622 RID: 22050
		[Range(0f, 1f)]
		[Tooltip("End audio at this volume")]
		[SerializeField]
		protected float endVolume = 1f;

		// Token: 0x04005623 RID: 22051
		[Tooltip("Time to fade between current volume level and target volume level.")]
		[SerializeField]
		protected float fadeDuration;

		// Token: 0x04005624 RID: 22052
		[Tooltip("Wait until this command has finished before executing the next command.")]
		[SerializeField]
		protected bool waitUntilFinished;

		// Token: 0x04005625 RID: 22053
		[HideInInspector]
		[FormerlySerializedAs("audioSource")]
		public AudioSource audioSourceOLD;
	}
}
