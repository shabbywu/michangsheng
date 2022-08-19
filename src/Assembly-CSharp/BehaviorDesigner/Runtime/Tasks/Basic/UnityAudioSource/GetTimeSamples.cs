using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200116F RID: 4463
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the time samples value of the AudioSource. Returns Success.")]
	public class GetTimeSamples : Action
	{
		// Token: 0x06007650 RID: 30288 RVA: 0x002B6434 File Offset: 0x002B4634
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007651 RID: 30289 RVA: 0x002B6474 File Offset: 0x002B4674
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = (float)this.audioSource.timeSamples;
			return 2;
		}

		// Token: 0x06007652 RID: 30290 RVA: 0x002B64A8 File Offset: 0x002B46A8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040061D2 RID: 25042
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061D3 RID: 25043
		[Tooltip("The time samples value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040061D4 RID: 25044
		private AudioSource audioSource;

		// Token: 0x040061D5 RID: 25045
		private GameObject prevGameObject;
	}
}
