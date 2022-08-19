using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200116A RID: 4458
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the mute value of the AudioSource. Returns Success.")]
	public class GetMute : Action
	{
		// Token: 0x0600763C RID: 30268 RVA: 0x002B6180 File Offset: 0x002B4380
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600763D RID: 30269 RVA: 0x002B61C0 File Offset: 0x002B43C0
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.mute;
			return 2;
		}

		// Token: 0x0600763E RID: 30270 RVA: 0x002B61F3 File Offset: 0x002B43F3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040061BE RID: 25022
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061BF RID: 25023
		[Tooltip("The mute value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040061C0 RID: 25024
		private AudioSource audioSource;

		// Token: 0x040061C1 RID: 25025
		private GameObject prevGameObject;
	}
}
