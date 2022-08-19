using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200116B RID: 4459
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the pitch value of the AudioSource. Returns Success.")]
	public class GetPitch : Action
	{
		// Token: 0x06007640 RID: 30272 RVA: 0x002B6208 File Offset: 0x002B4408
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007641 RID: 30273 RVA: 0x002B6248 File Offset: 0x002B4448
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.pitch;
			return 2;
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x002B627B File Offset: 0x002B447B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040061C2 RID: 25026
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061C3 RID: 25027
		[Tooltip("The pitch value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040061C4 RID: 25028
		private AudioSource audioSource;

		// Token: 0x040061C5 RID: 25029
		private GameObject prevGameObject;
	}
}
