using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200163F RID: 5695
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the rolloff mode of the AudioSource. Returns Success.")]
	public class SetRolloffMode : Action
	{
		// Token: 0x0600848E RID: 33934 RVA: 0x002CFBE8 File Offset: 0x002CDDE8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600848F RID: 33935 RVA: 0x0005BAC7 File Offset: 0x00059CC7
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.rolloffMode = this.rolloffMode;
			return 2;
		}

		// Token: 0x06008490 RID: 33936 RVA: 0x0005BAF5 File Offset: 0x00059CF5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rolloffMode = 0;
		}

		// Token: 0x04007137 RID: 28983
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007138 RID: 28984
		[Tooltip("The rolloff mode of the AudioSource")]
		public AudioRolloffMode rolloffMode;

		// Token: 0x04007139 RID: 28985
		private AudioSource audioSource;

		// Token: 0x0400713A RID: 28986
		private GameObject prevGameObject;
	}
}
