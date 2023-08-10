using UnityEngine;

namespace Spine.Unity.Examples;

public class SpineboyBeginnerView : MonoBehaviour
{
	[Header("Components")]
	public SpineboyBeginnerModel model;

	public SkeletonAnimation skeletonAnimation;

	public AnimationReferenceAsset run;

	public AnimationReferenceAsset idle;

	public AnimationReferenceAsset shoot;

	public AnimationReferenceAsset jump;

	public EventDataReferenceAsset footstepEvent;

	[Header("Audio")]
	public float footstepPitchOffset = 0.2f;

	public float gunsoundPitchOffset = 0.13f;

	public AudioSource footstepSource;

	public AudioSource gunSource;

	public AudioSource jumpSource;

	[Header("Effects")]
	public ParticleSystem gunParticles;

	private SpineBeginnerBodyState previousViewState;

	private void Start()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		if (!((Object)(object)skeletonAnimation == (Object)null))
		{
			model.ShootEvent += PlayShoot;
			skeletonAnimation.AnimationState.Event += new TrackEntryEventDelegate(HandleEvent);
		}
	}

	private void HandleEvent(TrackEntry trackEntry, Event e)
	{
		if (e.Data == footstepEvent.EventData)
		{
			PlayFootstepSound();
		}
	}

	private void Update()
	{
		if (!((Object)(object)skeletonAnimation == (Object)null) && !((Object)(object)model == (Object)null))
		{
			if (((SkeletonRenderer)skeletonAnimation).skeleton.ScaleX < 0f != model.facingLeft)
			{
				Turn(model.facingLeft);
			}
			SpineBeginnerBodyState state = model.state;
			if (previousViewState != state)
			{
				PlayNewStableAnimation();
			}
			previousViewState = state;
		}
	}

	private void PlayNewStableAnimation()
	{
		SpineBeginnerBodyState state = model.state;
		if (previousViewState == SpineBeginnerBodyState.Jumping && state != SpineBeginnerBodyState.Jumping)
		{
			PlayFootstepSound();
		}
		Animation val;
		switch (state)
		{
		case SpineBeginnerBodyState.Jumping:
			jumpSource.Play();
			val = AnimationReferenceAsset.op_Implicit(jump);
			break;
		case SpineBeginnerBodyState.Running:
			val = AnimationReferenceAsset.op_Implicit(run);
			break;
		default:
			val = AnimationReferenceAsset.op_Implicit(idle);
			break;
		}
		skeletonAnimation.AnimationState.SetAnimation(0, val, true);
	}

	private void PlayFootstepSound()
	{
		footstepSource.Play();
		footstepSource.pitch = GetRandomPitch(footstepPitchOffset);
	}

	[ContextMenu("Check Tracks")]
	private void CheckTracks()
	{
		AnimationState animationState = skeletonAnimation.AnimationState;
		Debug.Log((object)animationState.GetCurrent(0));
		Debug.Log((object)animationState.GetCurrent(1));
	}

	public void PlayShoot()
	{
		TrackEntry obj = skeletonAnimation.AnimationState.SetAnimation(1, AnimationReferenceAsset.op_Implicit(shoot), false);
		obj.AttachmentThreshold = 1f;
		obj.MixDuration = 0f;
		skeletonAnimation.state.AddEmptyAnimation(1, 0.5f, 0.1f).AttachmentThreshold = 1f;
		gunSource.pitch = GetRandomPitch(gunsoundPitchOffset);
		gunSource.Play();
		gunParticles.Play();
	}

	public void Turn(bool facingLeft)
	{
		((SkeletonRenderer)skeletonAnimation).Skeleton.ScaleX = (facingLeft ? (-1f) : 1f);
	}

	public float GetRandomPitch(float maxPitchOffset)
	{
		return 1f + Random.Range(0f - maxPitchOffset, maxPitchOffset);
	}
}
