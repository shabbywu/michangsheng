using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x0200162A RID: 5674
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the pitch value of the AudioSource. Returns Success.")]
	public class GetPitch : Action
	{
		// Token: 0x0600843A RID: 33850 RVA: 0x002CF658 File Offset: 0x002CD858
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600843B RID: 33851 RVA: 0x0005B4E0 File Offset: 0x000596E0
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.pitch;
			return 2;
		}

		// Token: 0x0600843C RID: 33852 RVA: 0x0005B513 File Offset: 0x00059713
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040070E5 RID: 28901
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070E6 RID: 28902
		[Tooltip("The pitch value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040070E7 RID: 28903
		private AudioSource audioSource;

		// Token: 0x040070E8 RID: 28904
		private GameObject prevGameObject;
	}
}
