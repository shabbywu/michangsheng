using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001178 RID: 4472
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the ignore listener volume value of the AudioSource. Returns Success.")]
	public class SetIgnoreListenerVolume : Action
	{
		// Token: 0x06007674 RID: 30324 RVA: 0x002B6944 File Offset: 0x002B4B44
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007675 RID: 30325 RVA: 0x002B6984 File Offset: 0x002B4B84
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.ignoreListenerVolume = this.ignoreListenerVolume.Value;
			return 2;
		}

		// Token: 0x06007676 RID: 30326 RVA: 0x002B69B7 File Offset: 0x002B4BB7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.ignoreListenerVolume = false;
		}

		// Token: 0x040061F4 RID: 25076
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061F5 RID: 25077
		[Tooltip("The ignore listener volume value of the AudioSource")]
		public SharedBool ignoreListenerVolume;

		// Token: 0x040061F6 RID: 25078
		private AudioSource audioSource;

		// Token: 0x040061F7 RID: 25079
		private GameObject prevGameObject;
	}
}
