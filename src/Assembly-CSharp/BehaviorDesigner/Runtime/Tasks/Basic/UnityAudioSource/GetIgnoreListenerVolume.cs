using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001166 RID: 4454
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the ignore listener volume value of the AudioSource. Returns Success.")]
	public class GetIgnoreListenerVolume : Action
	{
		// Token: 0x0600762C RID: 30252 RVA: 0x002B5F58 File Offset: 0x002B4158
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600762D RID: 30253 RVA: 0x002B5F98 File Offset: 0x002B4198
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.ignoreListenerVolume;
			return 2;
		}

		// Token: 0x0600762E RID: 30254 RVA: 0x002B5FCB File Offset: 0x002B41CB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040061AE RID: 25006
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061AF RID: 25007
		[Tooltip("The ignore listener volume value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040061B0 RID: 25008
		private AudioSource audioSource;

		// Token: 0x040061B1 RID: 25009
		private GameObject prevGameObject;
	}
}
