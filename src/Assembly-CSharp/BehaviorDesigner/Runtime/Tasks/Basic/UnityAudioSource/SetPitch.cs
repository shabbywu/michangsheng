using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200117E RID: 4478
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the pitch value of the AudioSource. Returns Success.")]
	public class SetPitch : Action
	{
		// Token: 0x0600768C RID: 30348 RVA: 0x002B6C7C File Offset: 0x002B4E7C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600768D RID: 30349 RVA: 0x002B6CBC File Offset: 0x002B4EBC
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

		// Token: 0x0600768E RID: 30350 RVA: 0x002B6CEF File Offset: 0x002B4EEF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.pitch = 1f;
		}

		// Token: 0x0400620C RID: 25100
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400620D RID: 25101
		[Tooltip("The pitch value of the AudioSource")]
		public SharedFloat pitch;

		// Token: 0x0400620E RID: 25102
		private AudioSource audioSource;

		// Token: 0x0400620F RID: 25103
		private GameObject prevGameObject;
	}
}
