using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200116D RID: 4461
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the spread value of the AudioSource. Returns Success.")]
	public class GetSpread : Action
	{
		// Token: 0x06007648 RID: 30280 RVA: 0x002B631C File Offset: 0x002B451C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007649 RID: 30281 RVA: 0x002B635C File Offset: 0x002B455C
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

		// Token: 0x0600764A RID: 30282 RVA: 0x002B638F File Offset: 0x002B458F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040061CA RID: 25034
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061CB RID: 25035
		[Tooltip("The spread value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040061CC RID: 25036
		private AudioSource audioSource;

		// Token: 0x040061CD RID: 25037
		private GameObject prevGameObject;
	}
}
