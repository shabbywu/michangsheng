using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001645 RID: 5701
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the volume value of the AudioSource. Returns Success.")]
	public class SetVolume : Action
	{
		// Token: 0x060084A6 RID: 33958 RVA: 0x002CFD68 File Offset: 0x002CDF68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084A7 RID: 33959 RVA: 0x0005BCA5 File Offset: 0x00059EA5
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.volume = this.volume.Value;
			return 2;
		}

		// Token: 0x060084A8 RID: 33960 RVA: 0x0005BCD8 File Offset: 0x00059ED8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.volume = 1f;
		}

		// Token: 0x0400714F RID: 29007
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007150 RID: 29008
		[Tooltip("The volume value of the AudioSource")]
		public SharedFloat volume;

		// Token: 0x04007151 RID: 29009
		private AudioSource audioSource;

		// Token: 0x04007152 RID: 29010
		private GameObject prevGameObject;
	}
}
