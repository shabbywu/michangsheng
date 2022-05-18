using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001624 RID: 5668
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the ignore listener pause value of the AudioSource. Returns Success.")]
	public class GetIgnoreListenerPause : Action
	{
		// Token: 0x06008422 RID: 33826 RVA: 0x002CF4D8 File Offset: 0x002CD6D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008423 RID: 33827 RVA: 0x0005B328 File Offset: 0x00059528
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

		// Token: 0x06008424 RID: 33828 RVA: 0x0005B35B File Offset: 0x0005955B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040070CD RID: 28877
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070CE RID: 28878
		[Tooltip("The ignore listener pause value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040070CF RID: 28879
		private AudioSource audioSource;

		// Token: 0x040070D0 RID: 28880
		private GameObject prevGameObject;
	}
}
