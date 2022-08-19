using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001172 RID: 4466
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Pauses the audio clip. Returns Success.")]
	public class Pause : Action
	{
		// Token: 0x0600765C RID: 30300 RVA: 0x002B65C8 File Offset: 0x002B47C8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600765D RID: 30301 RVA: 0x002B6608 File Offset: 0x002B4808
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

		// Token: 0x0600765E RID: 30302 RVA: 0x002B6630 File Offset: 0x002B4830
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040061DD RID: 25053
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061DE RID: 25054
		private AudioSource audioSource;

		// Token: 0x040061DF RID: 25055
		private GameObject prevGameObject;
	}
}
