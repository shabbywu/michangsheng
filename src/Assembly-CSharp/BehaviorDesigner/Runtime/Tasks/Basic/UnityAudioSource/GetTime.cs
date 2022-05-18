using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200162D RID: 5677
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the time value of the AudioSource. Returns Success.")]
	public class GetTime : Action
	{
		// Token: 0x06008446 RID: 33862 RVA: 0x002CF718 File Offset: 0x002CD918
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008447 RID: 33863 RVA: 0x0005B5C0 File Offset: 0x000597C0
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.time;
			return 2;
		}

		// Token: 0x06008448 RID: 33864 RVA: 0x0005B5F3 File Offset: 0x000597F3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040070F1 RID: 28913
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070F2 RID: 28914
		[Tooltip("The time value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040070F3 RID: 28915
		private AudioSource audioSource;

		// Token: 0x040070F4 RID: 28916
		private GameObject prevGameObject;
	}
}
