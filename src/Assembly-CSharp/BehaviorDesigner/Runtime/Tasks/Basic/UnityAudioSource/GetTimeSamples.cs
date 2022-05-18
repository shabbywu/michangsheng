using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200162E RID: 5678
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the time samples value of the AudioSource. Returns Success.")]
	public class GetTimeSamples : Action
	{
		// Token: 0x0600844A RID: 33866 RVA: 0x002CF758 File Offset: 0x002CD958
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600844B RID: 33867 RVA: 0x0005B60C File Offset: 0x0005980C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = (float)this.audioSource.timeSamples;
			return 2;
		}

		// Token: 0x0600844C RID: 33868 RVA: 0x0005B640 File Offset: 0x00059840
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040070F5 RID: 28917
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070F6 RID: 28918
		[Tooltip("The time samples value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040070F7 RID: 28919
		private AudioSource audioSource;

		// Token: 0x040070F8 RID: 28920
		private GameObject prevGameObject;
	}
}
