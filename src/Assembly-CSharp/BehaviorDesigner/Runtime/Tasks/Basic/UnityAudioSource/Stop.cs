using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001187 RID: 4487
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stops playing the audio clip. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x060076B0 RID: 30384 RVA: 0x002B7184 File Offset: 0x002B5384
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076B1 RID: 30385 RVA: 0x002B71C4 File Offset: 0x002B53C4
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.Stop();
			return 2;
		}

		// Token: 0x060076B2 RID: 30386 RVA: 0x002B71EC File Offset: 0x002B53EC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006230 RID: 25136
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006231 RID: 25137
		private AudioSource audioSource;

		// Token: 0x04006232 RID: 25138
		private GameObject prevGameObject;
	}
}
