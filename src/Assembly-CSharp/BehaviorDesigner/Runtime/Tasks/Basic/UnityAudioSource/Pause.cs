using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001631 RID: 5681
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Pauses the audio clip. Returns Success.")]
	public class Pause : Action
	{
		// Token: 0x06008456 RID: 33878 RVA: 0x002CF818 File Offset: 0x002CDA18
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008457 RID: 33879 RVA: 0x0005B6DA File Offset: 0x000598DA
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.Pause();
			return 2;
		}

		// Token: 0x06008458 RID: 33880 RVA: 0x0005B702 File Offset: 0x00059902
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04007100 RID: 28928
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007101 RID: 28929
		private AudioSource audioSource;

		// Token: 0x04007102 RID: 28930
		private GameObject prevGameObject;
	}
}
