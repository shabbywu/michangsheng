using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001183 RID: 4483
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the spread value of the AudioSource. Returns Success.")]
	public class SetSpread : Action
	{
		// Token: 0x060076A0 RID: 30368 RVA: 0x002B6F60 File Offset: 0x002B5160
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x002B6FA0 File Offset: 0x002B51A0
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.spread = this.spread.Value;
			return 2;
		}

		// Token: 0x060076A2 RID: 30370 RVA: 0x002B6FD3 File Offset: 0x002B51D3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.spread = 1f;
		}

		// Token: 0x04006220 RID: 25120
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006221 RID: 25121
		[Tooltip("The spread value of the AudioSource")]
		public SharedFloat spread;

		// Token: 0x04006222 RID: 25122
		private AudioSource audioSource;

		// Token: 0x04006223 RID: 25123
		private GameObject prevGameObject;
	}
}
