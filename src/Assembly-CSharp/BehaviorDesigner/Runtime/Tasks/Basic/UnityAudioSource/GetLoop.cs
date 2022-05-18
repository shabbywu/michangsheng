using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001626 RID: 5670
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the loop value of the AudioSource. Returns Success.")]
	public class GetLoop : Action
	{
		// Token: 0x0600842A RID: 33834 RVA: 0x002CF558 File Offset: 0x002CD758
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600842B RID: 33835 RVA: 0x0005B3B8 File Offset: 0x000595B8
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.loop;
			return 2;
		}

		// Token: 0x0600842C RID: 33836 RVA: 0x0005B3EB File Offset: 0x000595EB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040070D5 RID: 28885
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070D6 RID: 28886
		[Tooltip("The loop value of the AudioSource")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040070D7 RID: 28887
		private AudioSource audioSource;

		// Token: 0x040070D8 RID: 28888
		private GameObject prevGameObject;
	}
}
