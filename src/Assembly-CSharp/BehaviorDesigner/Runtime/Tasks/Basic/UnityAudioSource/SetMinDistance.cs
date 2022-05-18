using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200163B RID: 5691
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the min distance value of the AudioSource. Returns Success.")]
	public class SetMinDistance : Action
	{
		// Token: 0x0600847E RID: 33918 RVA: 0x002CFAE8 File Offset: 0x002CDCE8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600847F RID: 33919 RVA: 0x0005B99F File Offset: 0x00059B9F
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.minDistance = this.minDistance.Value;
			return 2;
		}

		// Token: 0x06008480 RID: 33920 RVA: 0x0005B9D2 File Offset: 0x00059BD2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.minDistance = 1f;
		}

		// Token: 0x04007127 RID: 28967
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007128 RID: 28968
		[Tooltip("The min distance value of the AudioSource")]
		public SharedFloat minDistance;

		// Token: 0x04007129 RID: 28969
		private AudioSource audioSource;

		// Token: 0x0400712A RID: 28970
		private GameObject prevGameObject;
	}
}
