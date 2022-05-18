using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200163E RID: 5694
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the priority value of the AudioSource. Returns Success.")]
	public class SetPriority : Action
	{
		// Token: 0x0600848A RID: 33930 RVA: 0x002CFBA8 File Offset: 0x002CDDA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600848B RID: 33931 RVA: 0x0005BA7F File Offset: 0x00059C7F
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.priority = this.priority.Value;
			return 2;
		}

		// Token: 0x0600848C RID: 33932 RVA: 0x0005BAB2 File Offset: 0x00059CB2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.priority = 1;
		}

		// Token: 0x04007133 RID: 28979
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007134 RID: 28980
		[Tooltip("The priority value of the AudioSource")]
		public SharedInt priority;

		// Token: 0x04007135 RID: 28981
		private AudioSource audioSource;

		// Token: 0x04007136 RID: 28982
		private GameObject prevGameObject;
	}
}
