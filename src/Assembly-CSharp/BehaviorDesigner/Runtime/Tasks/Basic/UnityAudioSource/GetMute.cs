using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001629 RID: 5673
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the mute value of the AudioSource. Returns Success.")]
	public class GetMute : Action
	{
		// Token: 0x06008436 RID: 33846 RVA: 0x002CF618 File Offset: 0x002CD818
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008437 RID: 33847 RVA: 0x0005B498 File Offset: 0x00059698
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.mute;
			return 2;
		}

		// Token: 0x06008438 RID: 33848 RVA: 0x0005B4CB File Offset: 0x000596CB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040070E1 RID: 28897
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070E2 RID: 28898
		[Tooltip("The mute value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040070E3 RID: 28899
		private AudioSource audioSource;

		// Token: 0x040070E4 RID: 28900
		private GameObject prevGameObject;
	}
}
