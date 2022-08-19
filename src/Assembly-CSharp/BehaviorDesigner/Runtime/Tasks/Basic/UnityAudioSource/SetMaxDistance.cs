using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200117B RID: 4475
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the max distance value of the AudioSource. Returns Success.")]
	public class SetMaxDistance : Action
	{
		// Token: 0x06007680 RID: 30336 RVA: 0x002B6ADC File Offset: 0x002B4CDC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007681 RID: 30337 RVA: 0x002B6B1C File Offset: 0x002B4D1C
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

		// Token: 0x06007682 RID: 30338 RVA: 0x002B6B4F File Offset: 0x002B4D4F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.maxDistance = 1f;
		}

		// Token: 0x04006200 RID: 25088
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006201 RID: 25089
		[Tooltip("The max distance value of the AudioSource")]
		public SharedFloat maxDistance;

		// Token: 0x04006202 RID: 25090
		private AudioSource audioSource;

		// Token: 0x04006203 RID: 25091
		private GameObject prevGameObject;
	}
}
