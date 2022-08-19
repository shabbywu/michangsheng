using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001174 RID: 4468
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Plays the audio clip with a delay specified in seconds. Returns Success.")]
	public class PlayDelayed : Action
	{
		// Token: 0x06007664 RID: 30308 RVA: 0x002B66B0 File Offset: 0x002B48B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x002B66F0 File Offset: 0x002B48F0
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

		// Token: 0x06007666 RID: 30310 RVA: 0x002B6723 File Offset: 0x002B4923
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.delay = 0f;
		}

		// Token: 0x040061E3 RID: 25059
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061E4 RID: 25060
		[Tooltip("Delay time specified in seconds")]
		public SharedFloat delay = 0f;

		// Token: 0x040061E5 RID: 25061
		private AudioSource audioSource;

		// Token: 0x040061E6 RID: 25062
		private GameObject prevGameObject;
	}
}
