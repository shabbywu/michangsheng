using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200163C RID: 5692
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the mute value of the AudioSource. Returns Success.")]
	public class SetMute : Action
	{
		// Token: 0x06008482 RID: 33922 RVA: 0x002CFB28 File Offset: 0x002CDD28
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008483 RID: 33923 RVA: 0x0005B9EB File Offset: 0x00059BEB
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.mute = this.mute.Value;
			return 2;
		}

		// Token: 0x06008484 RID: 33924 RVA: 0x0005BA1E File Offset: 0x00059C1E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mute = false;
		}

		// Token: 0x0400712B RID: 28971
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400712C RID: 28972
		[Tooltip("The mute value of the AudioSource")]
		public SharedBool mute;

		// Token: 0x0400712D RID: 28973
		private AudioSource audioSource;

		// Token: 0x0400712E RID: 28974
		private GameObject prevGameObject;
	}
}
