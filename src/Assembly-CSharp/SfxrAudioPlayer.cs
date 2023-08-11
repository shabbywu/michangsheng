using UnityEngine;

[AddComponentMenu("")]
public class SfxrAudioPlayer : MonoBehaviour
{
	private bool isDestroyed;

	private bool needsToDestroy;

	private bool runningInEditMode;

	private SfxrSynth sfxrSynth;

	private void Start()
	{
		AudioSource obj = ((Component)this).gameObject.AddComponent<AudioSource>();
		obj.clip = null;
		obj.volume = 1f;
		obj.pitch = 1f;
		obj.priority = 128;
		obj.Play();
	}

	private void Update()
	{
		if (sfxrSynth == null)
		{
			needsToDestroy = true;
		}
		if (needsToDestroy)
		{
			needsToDestroy = false;
			Destroy();
		}
	}

	private void OnAudioFilterRead(float[] __data, int __channels)
	{
		if (!isDestroyed && !needsToDestroy && sfxrSynth != null && !sfxrSynth.GenerateAudioFilterData(__data, __channels))
		{
			needsToDestroy = true;
			_ = runningInEditMode;
		}
	}

	public void SetSfxrSynth(SfxrSynth __sfxrSynth)
	{
		sfxrSynth = __sfxrSynth;
	}

	public void SetRunningInEditMode(bool __runningInEditMode)
	{
		runningInEditMode = __runningInEditMode;
	}

	public void Destroy()
	{
		if (!isDestroyed)
		{
			isDestroyed = true;
			sfxrSynth = null;
			if (runningInEditMode || !Application.isPlaying)
			{
				Object.DestroyImmediate((Object)(object)((Component)this).gameObject);
			}
			else
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
		}
	}
}
