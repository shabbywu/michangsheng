using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200116E RID: 4462
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the time value of the AudioSource. Returns Success.")]
	public class GetTime : Action
	{
		// Token: 0x0600764C RID: 30284 RVA: 0x002B63A8 File Offset: 0x002B45A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600764D RID: 30285 RVA: 0x002B63E8 File Offset: 0x002B45E8
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

		// Token: 0x0600764E RID: 30286 RVA: 0x002B641B File Offset: 0x002B461B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040061CE RID: 25038
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061CF RID: 25039
		[Tooltip("The time value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040061D0 RID: 25040
		private AudioSource audioSource;

		// Token: 0x040061D1 RID: 25041
		private GameObject prevGameObject;
	}
}
