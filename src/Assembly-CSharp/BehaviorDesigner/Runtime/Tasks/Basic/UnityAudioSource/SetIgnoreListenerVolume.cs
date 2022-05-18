using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001637 RID: 5687
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the ignore listener volume value of the AudioSource. Returns Success.")]
	public class SetIgnoreListenerVolume : Action
	{
		// Token: 0x0600846E RID: 33902 RVA: 0x002CF9E8 File Offset: 0x002CDBE8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600846F RID: 33903 RVA: 0x0005B87B File Offset: 0x00059A7B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.ignoreListenerVolume = this.ignoreListenerVolume.Value;
			return 2;
		}

		// Token: 0x06008470 RID: 33904 RVA: 0x0005B8AE File Offset: 0x00059AAE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.ignoreListenerVolume = false;
		}

		// Token: 0x04007117 RID: 28951
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007118 RID: 28952
		[Tooltip("The ignore listener volume value of the AudioSource")]
		public SharedBool ignoreListenerVolume;

		// Token: 0x04007119 RID: 28953
		private AudioSource audioSource;

		// Token: 0x0400711A RID: 28954
		private GameObject prevGameObject;
	}
}
