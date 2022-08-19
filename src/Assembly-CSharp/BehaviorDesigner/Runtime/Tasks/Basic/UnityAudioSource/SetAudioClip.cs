using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001177 RID: 4471
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the clip value of the AudioSource. Returns Success.")]
	public class SetAudioClip : Action
	{
		// Token: 0x06007670 RID: 30320 RVA: 0x002B68C4 File Offset: 0x002B4AC4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007671 RID: 30321 RVA: 0x002B6904 File Offset: 0x002B4B04
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.clip = this.audioClip;
			return 2;
		}

		// Token: 0x06007672 RID: 30322 RVA: 0x002B6932 File Offset: 0x002B4B32
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.audioClip = null;
		}

		// Token: 0x040061F0 RID: 25072
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061F1 RID: 25073
		[Tooltip("The AudioSource clip")]
		public AudioClip audioClip;

		// Token: 0x040061F2 RID: 25074
		private AudioSource audioSource;

		// Token: 0x040061F3 RID: 25075
		private GameObject prevGameObject;
	}
}
