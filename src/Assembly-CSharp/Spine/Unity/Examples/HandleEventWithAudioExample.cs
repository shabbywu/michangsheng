using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AE6 RID: 2790
	public class HandleEventWithAudioExample : MonoBehaviour
	{
		// Token: 0x06004E03 RID: 19971 RVA: 0x0021502B File Offset: 0x0021322B
		private void OnValidate()
		{
			if (this.skeletonAnimation == null)
			{
				base.GetComponent<SkeletonAnimation>();
			}
			if (this.audioSource == null)
			{
				base.GetComponent<AudioSource>();
			}
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x00215058 File Offset: 0x00213258
		private void Start()
		{
			if (this.audioSource == null)
			{
				return;
			}
			if (this.skeletonAnimation == null)
			{
				return;
			}
			this.skeletonAnimation.Initialize(false);
			if (!this.skeletonAnimation.valid)
			{
				return;
			}
			this.eventData = this.skeletonAnimation.Skeleton.Data.FindEvent(this.eventName);
			this.skeletonAnimation.AnimationState.Event += new AnimationState.TrackEntryEventDelegate(this.HandleAnimationStateEvent);
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x002150DA File Offset: 0x002132DA
		private void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
		{
			if (this.logDebugMessage)
			{
				Debug.Log("Event fired! " + e.Data.Name);
			}
			if (this.eventData == e.Data)
			{
				this.Play();
			}
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x00215114 File Offset: 0x00213314
		public void Play()
		{
			this.audioSource.pitch = this.basePitch + Random.Range(-this.randomPitchOffset, this.randomPitchOffset);
			this.audioSource.clip = this.audioClip;
			this.audioSource.Play();
		}

		// Token: 0x04004D5E RID: 19806
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004D5F RID: 19807
		[SpineEvent("", "skeletonAnimation", true, true, false)]
		public string eventName;

		// Token: 0x04004D60 RID: 19808
		[Space]
		public AudioSource audioSource;

		// Token: 0x04004D61 RID: 19809
		public AudioClip audioClip;

		// Token: 0x04004D62 RID: 19810
		public float basePitch = 1f;

		// Token: 0x04004D63 RID: 19811
		public float randomPitchOffset = 0.1f;

		// Token: 0x04004D64 RID: 19812
		[Space]
		public bool logDebugMessage;

		// Token: 0x04004D65 RID: 19813
		private EventData eventData;
	}
}
