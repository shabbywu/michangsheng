using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001171 RID: 4465
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Returns Success if the AudioClip is playing, otherwise Failure.")]
	public class IsPlaying : Conditional
	{
		// Token: 0x06007658 RID: 30296 RVA: 0x002B6550 File Offset: 0x002B4750
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007659 RID: 30297 RVA: 0x002B6590 File Offset: 0x002B4790
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			if (!this.audioSource.isPlaying)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600765A RID: 30298 RVA: 0x002B65BC File Offset: 0x002B47BC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040061DA RID: 25050
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061DB RID: 25051
		private AudioSource audioSource;

		// Token: 0x040061DC RID: 25052
		private GameObject prevGameObject;
	}
}
