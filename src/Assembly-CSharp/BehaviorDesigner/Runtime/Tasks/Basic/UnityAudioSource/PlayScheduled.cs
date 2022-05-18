using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001635 RID: 5685
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Plays the audio clip with a delay specified in seconds. Returns Success.")]
	public class PlayScheduled : Action
	{
		// Token: 0x06008466 RID: 33894 RVA: 0x002CF968 File Offset: 0x002CDB68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008467 RID: 33895 RVA: 0x0005B7D8 File Offset: 0x000599D8
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.PlayScheduled((double)this.time.Value);
			return 2;
		}

		// Token: 0x06008468 RID: 33896 RVA: 0x0005B80C File Offset: 0x00059A0C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x0400710F RID: 28943
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007110 RID: 28944
		[Tooltip("Time in seconds on the absolute time-line that AudioSettings.dspTime refers to for when the sound should start playing")]
		public SharedFloat time = 0f;

		// Token: 0x04007111 RID: 28945
		private AudioSource audioSource;

		// Token: 0x04007112 RID: 28946
		private GameObject prevGameObject;
	}
}
