using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001168 RID: 4456
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the max distance value of the AudioSource. Returns Success.")]
	public class GetMaxDistance : Action
	{
		// Token: 0x06007634 RID: 30260 RVA: 0x002B6068 File Offset: 0x002B4268
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007635 RID: 30261 RVA: 0x002B60A8 File Offset: 0x002B42A8
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.maxDistance;
			return 2;
		}

		// Token: 0x06007636 RID: 30262 RVA: 0x002B60DB File Offset: 0x002B42DB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040061B6 RID: 25014
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061B7 RID: 25015
		[Tooltip("The max distance value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040061B8 RID: 25016
		private AudioSource audioSource;

		// Token: 0x040061B9 RID: 25017
		private GameObject prevGameObject;
	}
}
