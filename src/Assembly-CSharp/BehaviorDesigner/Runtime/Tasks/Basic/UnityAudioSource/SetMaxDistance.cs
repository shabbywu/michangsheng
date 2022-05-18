using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200163A RID: 5690
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the max distance value of the AudioSource. Returns Success.")]
	public class SetMaxDistance : Action
	{
		// Token: 0x0600847A RID: 33914 RVA: 0x002CFAA8 File Offset: 0x002CDCA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600847B RID: 33915 RVA: 0x0005B953 File Offset: 0x00059B53
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.maxDistance = this.maxDistance.Value;
			return 2;
		}

		// Token: 0x0600847C RID: 33916 RVA: 0x0005B986 File Offset: 0x00059B86
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.maxDistance = 1f;
		}

		// Token: 0x04007123 RID: 28963
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007124 RID: 28964
		[Tooltip("The max distance value of the AudioSource")]
		public SharedFloat maxDistance;

		// Token: 0x04007125 RID: 28965
		private AudioSource audioSource;

		// Token: 0x04007126 RID: 28966
		private GameObject prevGameObject;
	}
}
