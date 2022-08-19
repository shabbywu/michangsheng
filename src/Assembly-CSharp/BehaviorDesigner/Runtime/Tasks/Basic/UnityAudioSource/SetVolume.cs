using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001186 RID: 4486
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the volume value of the AudioSource. Returns Success.")]
	public class SetVolume : Action
	{
		// Token: 0x060076AC RID: 30380 RVA: 0x002B70F8 File Offset: 0x002B52F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076AD RID: 30381 RVA: 0x002B7138 File Offset: 0x002B5338
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.volume = this.volume.Value;
			return 2;
		}

		// Token: 0x060076AE RID: 30382 RVA: 0x002B716B File Offset: 0x002B536B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.volume = 1f;
		}

		// Token: 0x0400622C RID: 25132
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400622D RID: 25133
		[Tooltip("The volume value of the AudioSource")]
		public SharedFloat volume;

		// Token: 0x0400622E RID: 25134
		private AudioSource audioSource;

		// Token: 0x0400622F RID: 25135
		private GameObject prevGameObject;
	}
}
