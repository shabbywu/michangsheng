using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001625 RID: 5669
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the ignore listener volume value of the AudioSource. Returns Success.")]
	public class GetIgnoreListenerVolume : Action
	{
		// Token: 0x06008426 RID: 33830 RVA: 0x002CF518 File Offset: 0x002CD718
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008427 RID: 33831 RVA: 0x0005B370 File Offset: 0x00059570
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

		// Token: 0x06008428 RID: 33832 RVA: 0x0005B3A3 File Offset: 0x000595A3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040070D1 RID: 28881
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070D2 RID: 28882
		[Tooltip("The ignore listener volume value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040070D3 RID: 28883
		private AudioSource audioSource;

		// Token: 0x040070D4 RID: 28884
		private GameObject prevGameObject;
	}
}
