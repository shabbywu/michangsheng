using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001630 RID: 5680
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Returns Success if the AudioClip is playing, otherwise Failure.")]
	public class IsPlaying : Conditional
	{
		// Token: 0x06008452 RID: 33874 RVA: 0x002CF7D8 File Offset: 0x002CD9D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008453 RID: 33875 RVA: 0x0005B6A5 File Offset: 0x000598A5
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			if (!this.audioSource.isPlaying)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008454 RID: 33876 RVA: 0x0005B6D1 File Offset: 0x000598D1
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040070FD RID: 28925
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070FE RID: 28926
		private AudioSource audioSource;

		// Token: 0x040070FF RID: 28927
		private GameObject prevGameObject;
	}
}
