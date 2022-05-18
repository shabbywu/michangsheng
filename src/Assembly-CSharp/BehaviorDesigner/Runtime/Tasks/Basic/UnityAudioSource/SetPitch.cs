using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200163D RID: 5693
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the pitch value of the AudioSource. Returns Success.")]
	public class SetPitch : Action
	{
		// Token: 0x06008486 RID: 33926 RVA: 0x002CFB68 File Offset: 0x002CDD68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008487 RID: 33927 RVA: 0x0005BA33 File Offset: 0x00059C33
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.pitch = this.pitch.Value;
			return 2;
		}

		// Token: 0x06008488 RID: 33928 RVA: 0x0005BA66 File Offset: 0x00059C66
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.pitch = 1f;
		}

		// Token: 0x0400712F RID: 28975
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007130 RID: 28976
		[Tooltip("The pitch value of the AudioSource")]
		public SharedFloat pitch;

		// Token: 0x04007131 RID: 28977
		private AudioSource audioSource;

		// Token: 0x04007132 RID: 28978
		private GameObject prevGameObject;
	}
}
