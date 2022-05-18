using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001641 RID: 5697
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Changes the time at which a sound that has already been scheduled to play will start. Returns Success.")]
	public class SetScheduledStartTime : Action
	{
		// Token: 0x06008496 RID: 33942 RVA: 0x002CFC68 File Offset: 0x002CDE68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008497 RID: 33943 RVA: 0x0005BB6A File Offset: 0x00059D6A
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.SetScheduledStartTime((double)this.time.Value);
			return 2;
		}

		// Token: 0x06008498 RID: 33944 RVA: 0x0005BB9E File Offset: 0x00059D9E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x0400713F RID: 28991
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007140 RID: 28992
		[Tooltip("Time in seconds")]
		public SharedFloat time = 0f;

		// Token: 0x04007141 RID: 28993
		private AudioSource audioSource;

		// Token: 0x04007142 RID: 28994
		private GameObject prevGameObject;
	}
}
