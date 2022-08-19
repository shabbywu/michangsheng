using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001182 RID: 4482
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Changes the time at which a sound that has already been scheduled to play will start. Returns Success.")]
	public class SetScheduledStartTime : Action
	{
		// Token: 0x0600769C RID: 30364 RVA: 0x002B6EB8 File Offset: 0x002B50B8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600769D RID: 30365 RVA: 0x002B6EF8 File Offset: 0x002B50F8
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

		// Token: 0x0600769E RID: 30366 RVA: 0x002B6F2C File Offset: 0x002B512C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x0400621C RID: 25116
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400621D RID: 25117
		[Tooltip("Time in seconds")]
		public SharedFloat time = 0f;

		// Token: 0x0400621E RID: 25118
		private AudioSource audioSource;

		// Token: 0x0400621F RID: 25119
		private GameObject prevGameObject;
	}
}
