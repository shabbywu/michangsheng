using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200117A RID: 4474
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the loop value of the AudioSource. Returns Success.")]
	public class SetLoop : Action
	{
		// Token: 0x0600767C RID: 30332 RVA: 0x002B6A54 File Offset: 0x002B4C54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600767D RID: 30333 RVA: 0x002B6A94 File Offset: 0x002B4C94
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.loop = this.loop.Value;
			return 2;
		}

		// Token: 0x0600767E RID: 30334 RVA: 0x002B6AC7 File Offset: 0x002B4CC7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.loop = false;
		}

		// Token: 0x040061FC RID: 25084
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061FD RID: 25085
		[Tooltip("The loop value of the AudioSource")]
		public SharedBool loop;

		// Token: 0x040061FE RID: 25086
		private AudioSource audioSource;

		// Token: 0x040061FF RID: 25087
		private GameObject prevGameObject;
	}
}
