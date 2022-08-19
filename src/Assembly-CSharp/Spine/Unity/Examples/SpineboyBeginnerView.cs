using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AE2 RID: 2786
	public class SpineboyBeginnerView : MonoBehaviour
	{
		// Token: 0x06004DEE RID: 19950 RVA: 0x00214B20 File Offset: 0x00212D20
		private void Start()
		{
			if (this.skeletonAnimation == null)
			{
				return;
			}
			this.model.ShootEvent += this.PlayShoot;
			this.skeletonAnimation.AnimationState.Event += new AnimationState.TrackEntryEventDelegate(this.HandleEvent);
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x00214B6F File Offset: 0x00212D6F
		private void HandleEvent(TrackEntry trackEntry, Event e)
		{
			if (e.Data == this.footstepEvent.EventData)
			{
				this.PlayFootstepSound();
			}
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x00214B8C File Offset: 0x00212D8C
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

		// Token: 0x06004DF1 RID: 19953 RVA: 0x00214C10 File Offset: 0x00212E10
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

		// Token: 0x06004DF2 RID: 19954 RVA: 0x00214C8B File Offset: 0x00212E8B
		private void PlayFootstepSound()
		{
			this.footstepSource.Play();
			this.footstepSource.pitch = this.GetRandomPitch(this.footstepPitchOffset);
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x00214CAF File Offset: 0x00212EAF
		[ContextMenu("Check Tracks")]
		private void CheckTracks()
		{
			AnimationState animationState = this.skeletonAnimation.AnimationState;
			Debug.Log(animationState.GetCurrent(0));
			Debug.Log(animationState.GetCurrent(1));
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x00214CD4 File Offset: 0x00212ED4
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

		// Token: 0x06004DF5 RID: 19957 RVA: 0x00214D65 File Offset: 0x00212F65
		public void Turn(bool facingLeft)
		{
			this.skeletonAnimation.Skeleton.ScaleX = (facingLeft ? -1f : 1f);
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x00214D86 File Offset: 0x00212F86
		public float GetRandomPitch(float maxPitchOffset)
		{
			return 1f + Random.Range(-maxPitchOffset, maxPitchOffset);
		}

		// Token: 0x04004D46 RID: 19782
		[Header("Components")]
		public SpineboyBeginnerModel model;

		// Token: 0x04004D47 RID: 19783
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004D48 RID: 19784
		public AnimationReferenceAsset run;

		// Token: 0x04004D49 RID: 19785
		public AnimationReferenceAsset idle;

		// Token: 0x04004D4A RID: 19786
		public AnimationReferenceAsset shoot;

		// Token: 0x04004D4B RID: 19787
		public AnimationReferenceAsset jump;

		// Token: 0x04004D4C RID: 19788
		public EventDataReferenceAsset footstepEvent;

		// Token: 0x04004D4D RID: 19789
		[Header("Audio")]
		public float footstepPitchOffset = 0.2f;

		// Token: 0x04004D4E RID: 19790
		public float gunsoundPitchOffset = 0.13f;

		// Token: 0x04004D4F RID: 19791
		public AudioSource footstepSource;

		// Token: 0x04004D50 RID: 19792
		public AudioSource gunSource;

		// Token: 0x04004D51 RID: 19793
		public AudioSource jumpSource;

		// Token: 0x04004D52 RID: 19794
		[Header("Effects")]
		public ParticleSystem gunParticles;

		// Token: 0x04004D53 RID: 19795
		private SpineBeginnerBodyState previousViewState;
	}
}
