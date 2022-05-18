using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E2F RID: 3631
	public class SpineboyBeginnerView : MonoBehaviour
	{
		// Token: 0x0600576A RID: 22378 RVA: 0x00244DE0 File Offset: 0x00242FE0
		private void Start()
		{
			if (this.skeletonAnimation == null)
			{
				return;
			}
			this.model.ShootEvent += this.PlayShoot;
			this.skeletonAnimation.AnimationState.Event += new AnimationState.TrackEntryEventDelegate(this.HandleEvent);
		}

		// Token: 0x0600576B RID: 22379 RVA: 0x0003E742 File Offset: 0x0003C942
		private void HandleEvent(TrackEntry trackEntry, Event e)
		{
			if (e.Data == this.footstepEvent.EventData)
			{
				this.PlayFootstepSound();
			}
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x00244E30 File Offset: 0x00243030
		private void Update()
		{
			if (this.skeletonAnimation == null)
			{
				return;
			}
			if (this.model == null)
			{
				return;
			}
			if (this.skeletonAnimation.skeleton.ScaleX < 0f != this.model.facingLeft)
			{
				this.Turn(this.model.facingLeft);
			}
			SpineBeginnerBodyState state = this.model.state;
			if (this.previousViewState != state)
			{
				this.PlayNewStableAnimation();
			}
			this.previousViewState = state;
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x00244EB4 File Offset: 0x002430B4
		private void PlayNewStableAnimation()
		{
			SpineBeginnerBodyState state = this.model.state;
			if (this.previousViewState == SpineBeginnerBodyState.Jumping && state != SpineBeginnerBodyState.Jumping)
			{
				this.PlayFootstepSound();
			}
			Animation animation;
			if (state == SpineBeginnerBodyState.Jumping)
			{
				this.jumpSource.Play();
				animation = this.jump;
			}
			else if (state == SpineBeginnerBodyState.Running)
			{
				animation = this.run;
			}
			else
			{
				animation = this.idle;
			}
			this.skeletonAnimation.AnimationState.SetAnimation(0, animation, true);
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x0003E75D File Offset: 0x0003C95D
		private void PlayFootstepSound()
		{
			this.footstepSource.Play();
			this.footstepSource.pitch = this.GetRandomPitch(this.footstepPitchOffset);
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x0003E781 File Offset: 0x0003C981
		[ContextMenu("Check Tracks")]
		private void CheckTracks()
		{
			AnimationState animationState = this.skeletonAnimation.AnimationState;
			Debug.Log(animationState.GetCurrent(0));
			Debug.Log(animationState.GetCurrent(1));
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x00244F30 File Offset: 0x00243130
		public void PlayShoot()
		{
			TrackEntry trackEntry = this.skeletonAnimation.AnimationState.SetAnimation(1, this.shoot, false);
			trackEntry.AttachmentThreshold = 1f;
			trackEntry.MixDuration = 0f;
			this.skeletonAnimation.state.AddEmptyAnimation(1, 0.5f, 0.1f).AttachmentThreshold = 1f;
			this.gunSource.pitch = this.GetRandomPitch(this.gunsoundPitchOffset);
			this.gunSource.Play();
			this.gunParticles.Play();
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x0003E7A5 File Offset: 0x0003C9A5
		public void Turn(bool facingLeft)
		{
			this.skeletonAnimation.Skeleton.ScaleX = (facingLeft ? -1f : 1f);
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x0003E7C6 File Offset: 0x0003C9C6
		public float GetRandomPitch(float maxPitchOffset)
		{
			return 1f + Random.Range(-maxPitchOffset, maxPitchOffset);
		}

		// Token: 0x0400574F RID: 22351
		[Header("Components")]
		public SpineboyBeginnerModel model;

		// Token: 0x04005750 RID: 22352
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04005751 RID: 22353
		public AnimationReferenceAsset run;

		// Token: 0x04005752 RID: 22354
		public AnimationReferenceAsset idle;

		// Token: 0x04005753 RID: 22355
		public AnimationReferenceAsset shoot;

		// Token: 0x04005754 RID: 22356
		public AnimationReferenceAsset jump;

		// Token: 0x04005755 RID: 22357
		public EventDataReferenceAsset footstepEvent;

		// Token: 0x04005756 RID: 22358
		[Header("Audio")]
		public float footstepPitchOffset = 0.2f;

		// Token: 0x04005757 RID: 22359
		public float gunsoundPitchOffset = 0.13f;

		// Token: 0x04005758 RID: 22360
		public AudioSource footstepSource;

		// Token: 0x04005759 RID: 22361
		public AudioSource gunSource;

		// Token: 0x0400575A RID: 22362
		public AudioSource jumpSource;

		// Token: 0x0400575B RID: 22363
		[Header("Effects")]
		public ParticleSystem gunParticles;

		// Token: 0x0400575C RID: 22364
		private SpineBeginnerBodyState previousViewState;
	}
}
