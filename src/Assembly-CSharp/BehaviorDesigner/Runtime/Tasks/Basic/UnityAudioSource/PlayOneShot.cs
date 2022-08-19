using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001175 RID: 4469
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Plays an AudioClip, and scales the AudioSource volume by volumeScale. Returns Success.")]
	public class PlayOneShot : Action
	{
		// Token: 0x06007668 RID: 30312 RVA: 0x002B6754 File Offset: 0x002B4954
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007669 RID: 30313 RVA: 0x002B6794 File Offset: 0x002B4994
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.PlayOneShot((AudioClip)this.clip.Value, this.volumeScale.Value);
			return 2;
		}

		// Token: 0x0600766A RID: 30314 RVA: 0x002B67E2 File Offset: 0x002B49E2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.clip = null;
			this.volumeScale = 1f;
		}

		// Token: 0x040061E7 RID: 25063
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061E8 RID: 25064
		[Tooltip("The clip being played")]
		public SharedObject clip;

		// Token: 0x040061E9 RID: 25065
		[Tooltip("The scale of the volume (0-1)")]
		public SharedFloat volumeScale = 1f;

		// Token: 0x040061EA RID: 25066
		private AudioSource audioSource;

		// Token: 0x040061EB RID: 25067
		private GameObject prevGameObject;
	}
}
