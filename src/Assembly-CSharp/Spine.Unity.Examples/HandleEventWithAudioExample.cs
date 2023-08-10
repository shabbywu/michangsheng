using UnityEngine;

namespace Spine.Unity.Examples;

public class HandleEventWithAudioExample : MonoBehaviour
{
	public SkeletonAnimation skeletonAnimation;

	[SpineEvent("", "skeletonAnimation", true, true, false)]
	public string eventName;

	[Space]
	public AudioSource audioSource;

	public AudioClip audioClip;

	public float basePitch = 1f;

	public float randomPitchOffset = 0.1f;

	[Space]
	public bool logDebugMessage;

	private EventData eventData;

	private void OnValidate()
	{
		if ((Object)(object)skeletonAnimation == (Object)null)
		{
			((Component)this).GetComponent<SkeletonAnimation>();
		}
		if ((Object)(object)audioSource == (Object)null)
		{
			((Component)this).GetComponent<AudioSource>();
		}
	}

	private void Start()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		if (!((Object)(object)audioSource == (Object)null) && !((Object)(object)skeletonAnimation == (Object)null))
		{
			((SkeletonRenderer)skeletonAnimation).Initialize(false);
			if (((SkeletonRenderer)skeletonAnimation).valid)
			{
				eventData = ((SkeletonRenderer)skeletonAnimation).Skeleton.Data.FindEvent(eventName);
				skeletonAnimation.AnimationState.Event += new TrackEntryEventDelegate(HandleAnimationStateEvent);
			}
		}
	}

	private void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
	{
		if (logDebugMessage)
		{
			Debug.Log((object)("Event fired! " + e.Data.Name));
		}
		if (eventData == e.Data)
		{
			Play();
		}
	}

	public void Play()
	{
		audioSource.pitch = basePitch + Random.Range(0f - randomPitchOffset, randomPitchOffset);
		audioSource.clip = audioClip;
		audioSource.Play();
	}
}
