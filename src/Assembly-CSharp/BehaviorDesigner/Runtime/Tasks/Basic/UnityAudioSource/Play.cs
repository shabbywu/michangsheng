using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001173 RID: 4467
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Plays the audio clip. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x06007660 RID: 30304 RVA: 0x002B663C File Offset: 0x002B483C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007661 RID: 30305 RVA: 0x002B667C File Offset: 0x002B487C
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.Play();
			return 2;
		}

		// Token: 0x06007662 RID: 30306 RVA: 0x002B66A4 File Offset: 0x002B48A4
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040061E0 RID: 25056
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061E1 RID: 25057
		private AudioSource audioSource;

		// Token: 0x040061E2 RID: 25058
		private GameObject prevGameObject;
	}
}
