using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001646 RID: 5702
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stops playing the audio clip. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x060084AA RID: 33962 RVA: 0x002CFDA8 File Offset: 0x002CDFA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084AB RID: 33963 RVA: 0x0005BCF1 File Offset: 0x00059EF1
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

		// Token: 0x060084AC RID: 33964 RVA: 0x0005BD19 File Offset: 0x00059F19
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04007153 RID: 29011
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007154 RID: 29012
		private AudioSource audioSource;

		// Token: 0x04007155 RID: 29013
		private GameObject prevGameObject;
	}
}
