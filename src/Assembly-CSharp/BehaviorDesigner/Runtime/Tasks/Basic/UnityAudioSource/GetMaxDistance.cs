using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001627 RID: 5671
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the max distance value of the AudioSource. Returns Success.")]
	public class GetMaxDistance : Action
	{
		// Token: 0x0600842E RID: 33838 RVA: 0x002CF598 File Offset: 0x002CD798
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600842F RID: 33839 RVA: 0x0005B400 File Offset: 0x00059600
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

		// Token: 0x06008430 RID: 33840 RVA: 0x0005B433 File Offset: 0x00059633
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040070D9 RID: 28889
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070DA RID: 28890
		[Tooltip("The max distance value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040070DB RID: 28891
		private AudioSource audioSource;

		// Token: 0x040070DC RID: 28892
		private GameObject prevGameObject;
	}
}
