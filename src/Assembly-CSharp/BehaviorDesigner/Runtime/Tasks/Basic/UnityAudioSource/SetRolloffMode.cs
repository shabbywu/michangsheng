using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001180 RID: 4480
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the rolloff mode of the AudioSource. Returns Success.")]
	public class SetRolloffMode : Action
	{
		// Token: 0x06007694 RID: 30356 RVA: 0x002B6D90 File Offset: 0x002B4F90
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007695 RID: 30357 RVA: 0x002B6DD0 File Offset: 0x002B4FD0
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.rolloffMode = this.rolloffMode;
			return 2;
		}

		// Token: 0x06007696 RID: 30358 RVA: 0x002B6DFE File Offset: 0x002B4FFE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rolloffMode = 0;
		}

		// Token: 0x04006214 RID: 25108
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006215 RID: 25109
		[Tooltip("The rolloff mode of the AudioSource")]
		public AudioRolloffMode rolloffMode;

		// Token: 0x04006216 RID: 25110
		private AudioSource audioSource;

		// Token: 0x04006217 RID: 25111
		private GameObject prevGameObject;
	}
}
