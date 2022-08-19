using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001179 RID: 4473
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the ignore listener pause value of the AudioSource. Returns Success.")]
	public class SetIgnoreListenerPause : Action
	{
		// Token: 0x06007678 RID: 30328 RVA: 0x002B69CC File Offset: 0x002B4BCC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007679 RID: 30329 RVA: 0x002B6A0C File Offset: 0x002B4C0C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.ignoreListenerPause = this.ignoreListenerPause.Value;
			return 2;
		}

		// Token: 0x0600767A RID: 30330 RVA: 0x002B6A3F File Offset: 0x002B4C3F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.ignoreListenerPause = false;
		}

		// Token: 0x040061F8 RID: 25080
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061F9 RID: 25081
		[Tooltip("The ignore listener pause value of the AudioSource")]
		public SharedBool ignoreListenerPause;

		// Token: 0x040061FA RID: 25082
		private AudioSource audioSource;

		// Token: 0x040061FB RID: 25083
		private GameObject prevGameObject;
	}
}
