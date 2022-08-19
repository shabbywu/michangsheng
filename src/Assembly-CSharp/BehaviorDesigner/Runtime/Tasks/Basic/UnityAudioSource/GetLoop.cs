using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001167 RID: 4455
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the loop value of the AudioSource. Returns Success.")]
	public class GetLoop : Action
	{
		// Token: 0x06007630 RID: 30256 RVA: 0x002B5FE0 File Offset: 0x002B41E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007631 RID: 30257 RVA: 0x002B6020 File Offset: 0x002B4220
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.loop;
			return 2;
		}

		// Token: 0x06007632 RID: 30258 RVA: 0x002B6053 File Offset: 0x002B4253
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040061B2 RID: 25010
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061B3 RID: 25011
		[Tooltip("The loop value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040061B4 RID: 25012
		private AudioSource audioSource;

		// Token: 0x040061B5 RID: 25013
		private GameObject prevGameObject;
	}
}
