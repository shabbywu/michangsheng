using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001170 RID: 4464
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the volume value of the AudioSource. Returns Success.")]
	public class GetVolume : Action
	{
		// Token: 0x06007654 RID: 30292 RVA: 0x002B64C4 File Offset: 0x002B46C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007655 RID: 30293 RVA: 0x002B6504 File Offset: 0x002B4704
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.volume;
			return 2;
		}

		// Token: 0x06007656 RID: 30294 RVA: 0x002B6537 File Offset: 0x002B4737
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040061D6 RID: 25046
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061D7 RID: 25047
		[Tooltip("The volume value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040061D8 RID: 25048
		private AudioSource audioSource;

		// Token: 0x040061D9 RID: 25049
		private GameObject prevGameObject;
	}
}
