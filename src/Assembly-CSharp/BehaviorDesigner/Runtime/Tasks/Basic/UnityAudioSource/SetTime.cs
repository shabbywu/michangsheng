using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001184 RID: 4484
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the time value of the AudioSource. Returns Success.")]
	public class SetTime : Action
	{
		// Token: 0x060076A4 RID: 30372 RVA: 0x002B6FEC File Offset: 0x002B51EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x002B702C File Offset: 0x002B522C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.time = this.time.Value;
			return 2;
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x002B705F File Offset: 0x002B525F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 1f;
		}

		// Token: 0x04006224 RID: 25124
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006225 RID: 25125
		[Tooltip("The time value of the AudioSource")]
		public SharedFloat time;

		// Token: 0x04006226 RID: 25126
		private AudioSource audioSource;

		// Token: 0x04006227 RID: 25127
		private GameObject prevGameObject;
	}
}
