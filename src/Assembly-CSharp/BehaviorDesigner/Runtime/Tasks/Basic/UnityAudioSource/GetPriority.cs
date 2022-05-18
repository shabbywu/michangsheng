using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200162B RID: 5675
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the priority value of the AudioSource. Returns Success.")]
	public class GetPriority : Action
	{
		// Token: 0x0600843E RID: 33854 RVA: 0x002CF698 File Offset: 0x002CD898
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600843F RID: 33855 RVA: 0x0005B52C File Offset: 0x0005972C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.priority;
			return 2;
		}

		// Token: 0x06008440 RID: 33856 RVA: 0x0005B55F File Offset: 0x0005975F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1;
		}

		// Token: 0x040070E9 RID: 28905
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070EA RID: 28906
		[Tooltip("The priority value of the AudioSource")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x040070EB RID: 28907
		private AudioSource audioSource;

		// Token: 0x040070EC RID: 28908
		private GameObject prevGameObject;
	}
}
