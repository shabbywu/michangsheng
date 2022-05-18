using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001633 RID: 5683
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Plays the audio clip with a delay specified in seconds. Returns Success.")]
	public class PlayDelayed : Action
	{
		// Token: 0x0600845E RID: 33886 RVA: 0x002CF898 File Offset: 0x002CDA98
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600845F RID: 33887 RVA: 0x0005B73C File Offset: 0x0005993C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.PlayDelayed(this.delay.Value);
			return 2;
		}

		// Token: 0x06008460 RID: 33888 RVA: 0x0005B76F File Offset: 0x0005996F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.delay = 0f;
		}

		// Token: 0x04007106 RID: 28934
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007107 RID: 28935
		[Tooltip("Delay time specified in seconds")]
		public SharedFloat delay = 0f;

		// Token: 0x04007108 RID: 28936
		private AudioSource audioSource;

		// Token: 0x04007109 RID: 28937
		private GameObject prevGameObject;
	}
}
