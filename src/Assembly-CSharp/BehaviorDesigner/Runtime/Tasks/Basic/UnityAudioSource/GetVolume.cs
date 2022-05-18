using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200162F RID: 5679
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the volume value of the AudioSource. Returns Success.")]
	public class GetVolume : Action
	{
		// Token: 0x0600844E RID: 33870 RVA: 0x002CF798 File Offset: 0x002CD998
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600844F RID: 33871 RVA: 0x0005B659 File Offset: 0x00059859
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.volume;
			return 2;
		}

		// Token: 0x06008450 RID: 33872 RVA: 0x0005B68C File Offset: 0x0005988C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040070F9 RID: 28921
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070FA RID: 28922
		[Tooltip("The volume value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040070FB RID: 28923
		private AudioSource audioSource;

		// Token: 0x040070FC RID: 28924
		private GameObject prevGameObject;
	}
}
