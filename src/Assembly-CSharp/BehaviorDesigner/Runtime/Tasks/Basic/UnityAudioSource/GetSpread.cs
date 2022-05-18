using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200162C RID: 5676
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the spread value of the AudioSource. Returns Success.")]
	public class GetSpread : Action
	{
		// Token: 0x06008442 RID: 33858 RVA: 0x002CF6D8 File Offset: 0x002CD8D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008443 RID: 33859 RVA: 0x0005B574 File Offset: 0x00059774
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.spread;
			return 2;
		}

		// Token: 0x06008444 RID: 33860 RVA: 0x0005B5A7 File Offset: 0x000597A7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040070ED RID: 28909
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070EE RID: 28910
		[Tooltip("The spread value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040070EF RID: 28911
		private AudioSource audioSource;

		// Token: 0x040070F0 RID: 28912
		private GameObject prevGameObject;
	}
}
