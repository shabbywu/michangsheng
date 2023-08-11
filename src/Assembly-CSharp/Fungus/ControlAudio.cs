using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Audio", "Control Audio", "Plays, loops, or stops an audiosource. Any AudioSources with the same tag as the target Audio Source will automatically be stoped.", 0)]
[ExecuteInEditMode]
public class ControlAudio : Command
{
	[Tooltip("What to do to audio")]
	[SerializeField]
	protected ControlAudioType control;

	[Tooltip("Audio clip to play")]
	[SerializeField]
	protected AudioSourceData _audioSource;

	[Range(0f, 1f)]
	[Tooltip("Start audio at this volume")]
	[SerializeField]
	protected float startVolume = 1f;

	[Range(0f, 1f)]
	[Tooltip("End audio at this volume")]
	[SerializeField]
	protected float endVolume = 1f;

	[Tooltip("Time to fade between current volume level and target volume level.")]
	[SerializeField]
	protected float fadeDuration;

	[Tooltip("Wait until this command has finished before executing the next command.")]
	[SerializeField]
	protected bool waitUntilFinished;

	[HideInInspector]
	[FormerlySerializedAs("audioSource")]
	public AudioSource audioSourceOLD;

	public virtual ControlAudioType Control => control;

	protected virtual void StopAudioWithSameTag()
	{
		if ((Object)(object)_audioSource.Value == (Object)null || ((Component)_audioSource.Value).tag == "Untagged")
		{
			return;
		}
		AudioSource[] array = Object.FindObjectsOfType<AudioSource>();
		foreach (AudioSource val in array)
		{
			if ((Object)(object)val != (Object)(object)_audioSource.Value && ((Component)val).tag == ((Component)_audioSource.Value).tag)
			{
				StopLoop(val);
			}
		}
	}

	protected virtual void PlayOnce()
	{
		if (fadeDuration > 0f)
		{
			LeanTween.value(((Component)_audioSource.Value).gameObject, _audioSource.Value.volume, endVolume, fadeDuration).setOnUpdate(delegate(float updateVolume)
			{
				_audioSource.Value.volume = updateVolume;
			});
		}
		_audioSource.Value.PlayOneShot(_audioSource.Value.clip);
		if (waitUntilFinished)
		{
			((MonoBehaviour)this).StartCoroutine(WaitAndContinue());
		}
	}

	protected virtual IEnumerator WaitAndContinue()
	{
		while (_audioSource.Value.isPlaying)
		{
			yield return null;
		}
		Continue();
	}

	protected virtual void PlayLoop()
	{
		if (fadeDuration > 0f)
		{
			_audioSource.Value.volume = 0f;
			_audioSource.Value.loop = true;
			((Component)_audioSource.Value).GetComponent<AudioSource>().Play();
			LeanTween.value(((Component)_audioSource.Value).gameObject, 0f, endVolume, fadeDuration).setOnUpdate(delegate(float updateVolume)
			{
				_audioSource.Value.volume = updateVolume;
			}).setOnComplete((Action)delegate
			{
				if (waitUntilFinished)
				{
					Continue();
				}
			});
		}
		else
		{
			_audioSource.Value.volume = endVolume;
			_audioSource.Value.loop = true;
			((Component)_audioSource.Value).GetComponent<AudioSource>().Play();
		}
	}

	protected virtual void PauseLoop()
	{
		if (fadeDuration > 0f)
		{
			LeanTween.value(((Component)_audioSource.Value).gameObject, _audioSource.Value.volume, 0f, fadeDuration).setOnUpdate(delegate(float updateVolume)
			{
				_audioSource.Value.volume = updateVolume;
			}).setOnComplete((Action)delegate
			{
				((Component)_audioSource.Value).GetComponent<AudioSource>().Pause();
				if (waitUntilFinished)
				{
					Continue();
				}
			});
		}
		else
		{
			((Component)_audioSource.Value).GetComponent<AudioSource>().Pause();
		}
	}

	protected virtual void StopLoop(AudioSource source)
	{
		if (fadeDuration > 0f)
		{
			LeanTween.value(((Component)source).gameObject, _audioSource.Value.volume, 0f, fadeDuration).setOnUpdate(delegate(float updateVolume)
			{
				source.volume = updateVolume;
			}).setOnComplete((Action)delegate
			{
				((Component)source).GetComponent<AudioSource>().Stop();
				if (waitUntilFinished)
				{
					Continue();
				}
			});
		}
		else
		{
			((Component)source).GetComponent<AudioSource>().Stop();
		}
	}

	protected virtual void ChangeVolume()
	{
		LeanTween.value(((Component)_audioSource.Value).gameObject, _audioSource.Value.volume, endVolume, fadeDuration).setOnUpdate(delegate(float updateVolume)
		{
			_audioSource.Value.volume = updateVolume;
		}).setOnComplete((Action)delegate
		{
			if (waitUntilFinished)
			{
				Continue();
			}
		});
	}

	protected virtual void AudioFinished()
	{
		if (waitUntilFinished)
		{
			Continue();
		}
	}

	public override void OnEnter()
	{
		if ((Object)(object)_audioSource.Value == (Object)null)
		{
			Continue();
			return;
		}
		if (control != ControlAudioType.ChangeVolume)
		{
			_audioSource.Value.volume = endVolume;
		}
		switch (control)
		{
		case ControlAudioType.PlayOnce:
			StopAudioWithSameTag();
			PlayOnce();
			break;
		case ControlAudioType.PlayLoop:
			StopAudioWithSameTag();
			PlayLoop();
			break;
		case ControlAudioType.PauseLoop:
			PauseLoop();
			break;
		case ControlAudioType.StopLoop:
			StopLoop(_audioSource.Value);
			break;
		case ControlAudioType.ChangeVolume:
			ChangeVolume();
			break;
		}
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		if ((Object)(object)_audioSource.Value == (Object)null)
		{
			return "Error: No sound clip selected";
		}
		string text = "";
		if (fadeDuration > 0f)
		{
			text = " Fade out";
			if (control != ControlAudioType.StopLoop)
			{
				text = " Fade in volume to " + endVolume;
			}
			if (control == ControlAudioType.ChangeVolume)
			{
				text = " to " + endVolume;
			}
			text = text + " over " + fadeDuration + " seconds.";
		}
		return control.ToString() + " \"" + ((Object)_audioSource.Value).name + "\"" + text;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)242, (byte)209, (byte)176, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_audioSource.audioSourceRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if ((Object)(object)audioSourceOLD != (Object)null)
		{
			_audioSource.Value = audioSourceOLD;
			audioSourceOLD = null;
		}
	}
}
