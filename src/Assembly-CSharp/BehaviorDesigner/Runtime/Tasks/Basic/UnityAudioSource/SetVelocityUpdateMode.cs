using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001185 RID: 4485
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the rolloff mode of the AudioSource. Returns Success.")]
	public class SetVelocityUpdateMode : Action
	{
		// Token: 0x060076A8 RID: 30376 RVA: 0x002B7078 File Offset: 0x002B5278
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076A9 RID: 30377 RVA: 0x002B70B8 File Offset: 0x002B52B8
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

		// Token: 0x060076AA RID: 30378 RVA: 0x002B70E6 File Offset: 0x002B52E6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocityUpdateMode = 0;
		}

		// Token: 0x04006228 RID: 25128
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006229 RID: 25129
		[Tooltip("The velocity update mode of the AudioSource")]
		public AudioVelocityUpdateMode velocityUpdateMode;

		// Token: 0x0400622A RID: 25130
		private AudioSource audioSource;

		// Token: 0x0400622B RID: 25131
		private GameObject prevGameObject;
	}
}
