using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001628 RID: 5672
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Stores the min distance value of the AudioSource. Returns Success.")]
	public class GetMinDistance : Action
	{
		// Token: 0x06008432 RID: 33842 RVA: 0x002CF5D8 File Offset: 0x002CD7D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008433 RID: 33843 RVA: 0x0005B44C File Offset: 0x0005964C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.storeValue.Value = this.audioSource.minDistance;
			return 2;
		}

		// Token: 0x06008434 RID: 33844 RVA: 0x0005B47F File Offset: 0x0005967F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 1f;
		}

		// Token: 0x040070DD RID: 28893
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070DE RID: 28894
		[Tooltip("The min distance value of the AudioSource")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040070DF RID: 28895
		private AudioSource audioSource;

		// Token: 0x040070E0 RID: 28896
		private GameObject prevGameObject;
	}
}
