using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200116C RID: 4460
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the priority value of the AudioSource. Returns Success.")]
	public class GetPriority : Action
	{
		// Token: 0x06007644 RID: 30276 RVA: 0x002B6294 File Offset: 0x002B4494
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007645 RID: 30277 RVA: 0x002B62D4 File Offset: 0x002B44D4
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.priority;
			return 2;
		}

		// Token: 0x06007646 RID: 30278 RVA: 0x002B6307 File Offset: 0x002B4507
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1;
		}

		// Token: 0x040061C6 RID: 25030
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061C7 RID: 25031
		[Tooltip("The priority value of the AudioSource")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x040061C8 RID: 25032
		private AudioSource audioSource;

		// Token: 0x040061C9 RID: 25033
		private GameObject prevGameObject;
	}
}
