using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001639 RID: 5689
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the loop value of the AudioSource. Returns Success.")]
	public class SetLoop : Action
	{
		// Token: 0x06008476 RID: 33910 RVA: 0x002CFA68 File Offset: 0x002CDC68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008477 RID: 33911 RVA: 0x0005B90B File Offset: 0x00059B0B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.loop = this.loop.Value;
			return 2;
		}

		// Token: 0x06008478 RID: 33912 RVA: 0x0005B93E File Offset: 0x00059B3E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.loop = false;
		}

		// Token: 0x0400711F RID: 28959
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007120 RID: 28960
		[Tooltip("The loop value of the AudioSource")]
		public SharedBool loop;

		// Token: 0x04007121 RID: 28961
		private AudioSource audioSource;

		// Token: 0x04007122 RID: 28962
		private GameObject prevGameObject;
	}
}
