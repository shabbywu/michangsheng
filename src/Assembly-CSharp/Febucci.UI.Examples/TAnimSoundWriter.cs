using Febucci.Attributes;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Febucci.UI.Examples;

[AddComponentMenu("Febucci/TextAnimator/SoundWriter")]
[RequireComponent(typeof(TAnimPlayerBase))]
public class TAnimSoundWriter : MonoBehaviour
{
	[Header("References")]
	public AudioSource source;

	[Header("Management")]
	[Tooltip("How much time has to pass before playing the next sound")]
	[SerializeField]
	[MinValue(0f)]
	private float minSoundDelay = 0.07f;

	[Tooltip("True if you want the new sound to cut the previous one\nFalse if each sound will continue until its end")]
	[SerializeField]
	private bool interruptPreviousSound = true;

	[Header("Audio Clips")]
	[Tooltip("True if sounds will be picked random from the array\nFalse if they'll be chosen in order")]
	[SerializeField]
	private bool randomSequence;

	[SerializeField]
	private AudioClip[] sounds = (AudioClip[])(object)new AudioClip[0];

	private float latestTimePlayed = -1f;

	private int clipIndex;

	private void Awake()
	{
		if (!((Object)(object)source == (Object)null) && sounds.Length != 0)
		{
			source.playOnAwake = false;
			source.loop = false;
			((UnityEvent<char>)(object)((Component)this).GetComponent<TAnimPlayerBase>()?.onCharacterVisible).AddListener((UnityAction<char>)OnCharacter);
			clipIndex = (randomSequence ? Random.Range(0, sounds.Length) : 0);
		}
	}

	private void OnCharacter(char character)
	{
		if (Time.time - latestTimePlayed <= minSoundDelay)
		{
			return;
		}
		source.clip = sounds[clipIndex];
		if (interruptPreviousSound)
		{
			source.Play();
		}
		else
		{
			source.PlayOneShot(source.clip);
		}
		if (randomSequence)
		{
			clipIndex = Random.Range(0, sounds.Length);
		}
		else
		{
			clipIndex++;
			if (clipIndex >= sounds.Length)
			{
				clipIndex = 0;
			}
		}
		latestTimePlayed = Time.time;
	}
}
