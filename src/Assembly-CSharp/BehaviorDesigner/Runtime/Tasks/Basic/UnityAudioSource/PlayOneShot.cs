using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001634 RID: 5684
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Plays an AudioClip, and scales the AudioSource volume by volumeScale. Returns Success.")]
	public class PlayOneShot : Action
	{
		// Token: 0x06008462 RID: 33890 RVA: 0x002CF8D8 File Offset: 0x002CDAD8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008463 RID: 33891 RVA: 0x002CF918 File Offset: 0x002CDB18
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

		// Token: 0x06008464 RID: 33892 RVA: 0x0005B7A0 File Offset: 0x000599A0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.clip = null;
			this.volumeScale = 1f;
		}

		// Token: 0x0400710A RID: 28938
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400710B RID: 28939
		[Tooltip("The clip being played")]
		public SharedObject clip;

		// Token: 0x0400710C RID: 28940
		[Tooltip("The scale of the volume (0-1)")]
		public SharedFloat volumeScale = 1f;

		// Token: 0x0400710D RID: 28941
		private AudioSource audioSource;

		// Token: 0x0400710E RID: 28942
		private GameObject prevGameObject;
	}
}
