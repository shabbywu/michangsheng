using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001640 RID: 5696
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Changes the time at which a sound that has already been scheduled to play will end. Notice that depending on the timing not all rescheduling requests can be fulfilled. Returns Success.")]
	public class SetScheduledEndTime : Action
	{
		// Token: 0x06008492 RID: 33938 RVA: 0x002CFC28 File Offset: 0x002CDE28
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008493 RID: 33939 RVA: 0x0005BB05 File Offset: 0x00059D05
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.SetScheduledEndTime((double)this.time.Value);
			return 2;
		}

		// Token: 0x06008494 RID: 33940 RVA: 0x0005BB39 File Offset: 0x00059D39
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x0400713B RID: 28987
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400713C RID: 28988
		[Tooltip("Time in seconds")]
		public SharedFloat time = 0f;

		// Token: 0x0400713D RID: 28989
		private AudioSource audioSource;

		// Token: 0x0400713E RID: 28990
		private GameObject prevGameObject;
	}
}
