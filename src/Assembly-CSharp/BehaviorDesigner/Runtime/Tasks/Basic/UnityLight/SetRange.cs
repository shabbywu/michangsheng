using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x0200111B RID: 4379
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the range of the light.")]
	public class SetRange : Action
	{
		// Token: 0x06007522 RID: 29986 RVA: 0x002B3F1C File Offset: 0x002B211C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007523 RID: 29987 RVA: 0x002B3F5C File Offset: 0x002B215C
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.range = this.range.Value;
			return 2;
		}

		// Token: 0x06007524 RID: 29988 RVA: 0x002B3F8F File Offset: 0x002B218F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.range = 0f;
		}

		// Token: 0x040060CC RID: 24780
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060CD RID: 24781
		[Tooltip("The range to set")]
		public SharedFloat range;

		// Token: 0x040060CE RID: 24782
		private Light light;

		// Token: 0x040060CF RID: 24783
		private GameObject prevGameObject;
	}
}
