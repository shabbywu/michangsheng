using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200117D RID: 4477
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the mute value of the AudioSource. Returns Success.")]
	public class SetMute : Action
	{
		// Token: 0x06007688 RID: 30344 RVA: 0x002B6BF4 File Offset: 0x002B4DF4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007689 RID: 30345 RVA: 0x002B6C34 File Offset: 0x002B4E34
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

		// Token: 0x0600768A RID: 30346 RVA: 0x002B6C67 File Offset: 0x002B4E67
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mute = false;
		}

		// Token: 0x04006208 RID: 25096
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006209 RID: 25097
		[Tooltip("The mute value of the AudioSource")]
		public SharedBool mute;

		// Token: 0x0400620A RID: 25098
		private AudioSource audioSource;

		// Token: 0x0400620B RID: 25099
		private GameObject prevGameObject;
	}
}
