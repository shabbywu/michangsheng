using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001165 RID: 4453
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the ignore listener pause value of the AudioSource. Returns Success.")]
	public class GetIgnoreListenerPause : Action
	{
		// Token: 0x06007628 RID: 30248 RVA: 0x002B5ED0 File Offset: 0x002B40D0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007629 RID: 30249 RVA: 0x002B5F10 File Offset: 0x002B4110
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.ignoreListenerPause;
			return 2;
		}

		// Token: 0x0600762A RID: 30250 RVA: 0x002B5F43 File Offset: 0x002B4143
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040061AA RID: 25002
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061AB RID: 25003
		[Tooltip("The ignore listener pause value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040061AC RID: 25004
		private AudioSource audioSource;

		// Token: 0x040061AD RID: 25005
		private GameObject prevGameObject;
	}
}
