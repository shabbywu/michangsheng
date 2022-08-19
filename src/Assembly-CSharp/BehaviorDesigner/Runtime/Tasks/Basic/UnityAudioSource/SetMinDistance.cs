using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200117C RID: 4476
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the min distance value of the AudioSource. Returns Success.")]
	public class SetMinDistance : Action
	{
		// Token: 0x06007684 RID: 30340 RVA: 0x002B6B68 File Offset: 0x002B4D68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007685 RID: 30341 RVA: 0x002B6BA8 File Offset: 0x002B4DA8
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

		// Token: 0x06007686 RID: 30342 RVA: 0x002B6BDB File Offset: 0x002B4DDB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.minDistance = 1f;
		}

		// Token: 0x04006204 RID: 25092
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006205 RID: 25093
		[Tooltip("The min distance value of the AudioSource")]
		public SharedFloat minDistance;

		// Token: 0x04006206 RID: 25094
		private AudioSource audioSource;

		// Token: 0x04006207 RID: 25095
		private GameObject prevGameObject;
	}
}
