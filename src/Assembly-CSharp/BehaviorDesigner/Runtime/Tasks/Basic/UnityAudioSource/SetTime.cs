using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001643 RID: 5699
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the time value of the AudioSource. Returns Success.")]
	public class SetTime : Action
	{
		// Token: 0x0600849E RID: 33950 RVA: 0x002CFCE8 File Offset: 0x002CDEE8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600849F RID: 33951 RVA: 0x0005BC1B File Offset: 0x00059E1B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.time = this.time.Value;
			return 2;
		}

		// Token: 0x060084A0 RID: 33952 RVA: 0x0005BC4E File Offset: 0x00059E4E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 1f;
		}

		// Token: 0x04007147 RID: 28999
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007148 RID: 29000
		[Tooltip("The time value of the AudioSource")]
		public SharedFloat time;

		// Token: 0x04007149 RID: 29001
		private AudioSource audioSource;

		// Token: 0x0400714A RID: 29002
		private GameObject prevGameObject;
	}
}
