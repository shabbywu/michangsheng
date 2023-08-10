using UnityEngine;

public class EGA_EffectSound : MonoBehaviour
{
	public bool Repeating = true;

	public float RepeatTime = 2f;

	public float StartTime;

	public bool RandomVolume;

	public float minVolume = 0.4f;

	public float maxVolume = 1f;

	private AudioClip clip;

	private AudioSource soundComponent;

	private void Start()
	{
		soundComponent = ((Component)this).GetComponent<AudioSource>();
		clip = soundComponent.clip;
		if (RandomVolume)
		{
			soundComponent.volume = Random.Range(minVolume, maxVolume);
			RepeatSound();
		}
		if (Repeating)
		{
			((MonoBehaviour)this).InvokeRepeating("RepeatSound", StartTime, RepeatTime);
		}
	}

	private void RepeatSound()
	{
		soundComponent.PlayOneShot(clip);
	}
}
