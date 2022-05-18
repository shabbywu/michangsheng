using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001638 RID: 5688
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the ignore listener pause value of the AudioSource. Returns Success.")]
	public class SetIgnoreListenerPause : Action
	{
		// Token: 0x06008472 RID: 33906 RVA: 0x002CFA28 File Offset: 0x002CDC28
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008473 RID: 33907 RVA: 0x0005B8C3 File Offset: 0x00059AC3
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

		// Token: 0x06008474 RID: 33908 RVA: 0x0005B8F6 File Offset: 0x00059AF6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.ignoreListenerPause = false;
		}

		// Token: 0x0400711B RID: 28955
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400711C RID: 28956
		[Tooltip("The ignore listener pause value of the AudioSource")]
		public SharedBool ignoreListenerPause;

		// Token: 0x0400711D RID: 28957
		private AudioSource audioSource;

		// Token: 0x0400711E RID: 28958
		private GameObject prevGameObject;
	}
}
