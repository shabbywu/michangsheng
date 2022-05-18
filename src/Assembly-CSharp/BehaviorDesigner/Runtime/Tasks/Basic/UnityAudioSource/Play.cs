using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001632 RID: 5682
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Plays the audio clip. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x0600845A RID: 33882 RVA: 0x002CF858 File Offset: 0x002CDA58
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600845B RID: 33883 RVA: 0x0005B70B File Offset: 0x0005990B
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.Play();
			return 2;
		}

		// Token: 0x0600845C RID: 33884 RVA: 0x0005B733 File Offset: 0x00059933
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04007103 RID: 28931
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007104 RID: 28932
		private AudioSource audioSource;

		// Token: 0x04007105 RID: 28933
		private GameObject prevGameObject;
	}
}
