using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001169 RID: 4457
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the min distance value of the AudioSource. Returns Success.")]
	public class GetMinDistance : Action
	{
		// Token: 0x06007638 RID: 30264 RVA: 0x002B60F4 File Offset: 0x002B42F4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007639 RID: 30265 RVA: 0x002B6134 File Offset: 0x002B4334
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.minDistance;
			return 2;
		}

		// Token: 0x0600763A RID: 30266 RVA: 0x002B6167 File Offset: 0x002B4367
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040061BA RID: 25018
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061BB RID: 25019
		[Tooltip("The min distance value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040061BC RID: 25020
		private AudioSource audioSource;

		// Token: 0x040061BD RID: 25021
		private GameObject prevGameObject;
	}
}
