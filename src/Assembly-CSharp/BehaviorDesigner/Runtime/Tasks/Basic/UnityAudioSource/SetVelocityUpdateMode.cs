using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001644 RID: 5700
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the rolloff mode of the AudioSource. Returns Success.")]
	public class SetVelocityUpdateMode : Action
	{
		// Token: 0x060084A2 RID: 33954 RVA: 0x002CFD28 File Offset: 0x002CDF28
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084A3 RID: 33955 RVA: 0x0005BC67 File Offset: 0x00059E67
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.velocityUpdateMode = this.velocityUpdateMode;
			return 2;
		}

		// Token: 0x060084A4 RID: 33956 RVA: 0x0005BC95 File Offset: 0x00059E95
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocityUpdateMode = 0;
		}

		// Token: 0x0400714B RID: 29003
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400714C RID: 29004
		[Tooltip("The velocity update mode of the AudioSource")]
		public AudioVelocityUpdateMode velocityUpdateMode;

		// Token: 0x0400714D RID: 29005
		private AudioSource audioSource;

		// Token: 0x0400714E RID: 29006
		private GameObject prevGameObject;
	}
}
