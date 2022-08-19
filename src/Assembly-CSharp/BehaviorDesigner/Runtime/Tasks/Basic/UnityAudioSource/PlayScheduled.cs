using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001176 RID: 4470
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Plays the audio clip with a delay specified in seconds. Returns Success.")]
	public class PlayScheduled : Action
	{
		// Token: 0x0600766C RID: 30316 RVA: 0x002B681C File Offset: 0x002B4A1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600766D RID: 30317 RVA: 0x002B685C File Offset: 0x002B4A5C
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

		// Token: 0x0600766E RID: 30318 RVA: 0x002B6890 File Offset: 0x002B4A90
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x040061EC RID: 25068
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061ED RID: 25069
		[Tooltip("Time in seconds on the absolute time-line that AudioSettings.dspTime refers to for when the sound should start playing")]
		public SharedFloat time = 0f;

		// Token: 0x040061EE RID: 25070
		private AudioSource audioSource;

		// Token: 0x040061EF RID: 25071
		private GameObject prevGameObject;
	}
}
