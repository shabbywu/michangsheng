using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E34 RID: 3636
	public class HandleEventWithAudioExample : MonoBehaviour
	{
		// Token: 0x0600577F RID: 22399 RVA: 0x0003E8A6 File Offset: 0x0003CAA6
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

		// Token: 0x06005780 RID: 22400 RVA: 0x00245188 File Offset: 0x00243388
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

		// Token: 0x06005781 RID: 22401 RVA: 0x0003E8D2 File Offset: 0x0003CAD2
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

		// Token: 0x06005782 RID: 22402 RVA: 0x0024520C File Offset: 0x0024340C
		public void Play()
		{
			this.audioSource.pitch = this.basePitch + Random.Range(-this.randomPitchOffset, this.randomPitchOffset);
			this.audioSource.clip = this.audioClip;
			this.audioSource.Play();
		}

		// Token: 0x0400576A RID: 22378
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x0400576B RID: 22379
		[SpineEvent("", "skeletonAnimation", true, true, false)]
		public string eventName;

		// Token: 0x0400576C RID: 22380
		[Space]
		public AudioSource audioSource;

		// Token: 0x0400576D RID: 22381
		public AudioClip audioClip;

		// Token: 0x0400576E RID: 22382
		public float basePitch = 1f;

		// Token: 0x0400576F RID: 22383
		public float randomPitchOffset = 0.1f;

		// Token: 0x04005770 RID: 22384
		[Space]
		public bool logDebugMessage;

		// Token: 0x04005771 RID: 22385
		private EventData eventData;
	}
}
