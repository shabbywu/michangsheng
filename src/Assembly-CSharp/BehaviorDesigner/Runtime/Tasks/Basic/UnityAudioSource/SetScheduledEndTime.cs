using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001181 RID: 4481
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Changes the time at which a sound that has already been scheduled to play will end. Notice that depending on the timing not all rescheduling requests can be fulfilled. Returns Success.")]
	public class SetScheduledEndTime : Action
	{
		// Token: 0x06007698 RID: 30360 RVA: 0x002B6E10 File Offset: 0x002B5010
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007699 RID: 30361 RVA: 0x002B6E50 File Offset: 0x002B5050
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

		// Token: 0x0600769A RID: 30362 RVA: 0x002B6E84 File Offset: 0x002B5084
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x04006218 RID: 25112
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006219 RID: 25113
		[Tooltip("Time in seconds")]
		public SharedFloat time = 0f;

		// Token: 0x0400621A RID: 25114
		private AudioSource audioSource;

		// Token: 0x0400621B RID: 25115
		private GameObject prevGameObject;
	}
}
