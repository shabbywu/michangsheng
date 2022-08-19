using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200117F RID: 4479
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the priority value of the AudioSource. Returns Success.")]
	public class SetPriority : Action
	{
		// Token: 0x06007690 RID: 30352 RVA: 0x002B6D08 File Offset: 0x002B4F08
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007691 RID: 30353 RVA: 0x002B6D48 File Offset: 0x002B4F48
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.priority = this.priority.Value;
			return 2;
		}

		// Token: 0x06007692 RID: 30354 RVA: 0x002B6D7B File Offset: 0x002B4F7B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.priority = 1;
		}

		// Token: 0x04006210 RID: 25104
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006211 RID: 25105
		[Tooltip("The priority value of the AudioSource")]
		public SharedInt priority;

		// Token: 0x04006212 RID: 25106
		private AudioSource audioSource;

		// Token: 0x04006213 RID: 25107
		private GameObject prevGameObject;
	}
}
