using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200152C RID: 5420
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Gets the GameObject from the Transform component. Returns Success.")]
	public class SharedTransformToGameObject : Action
	{
		// Token: 0x060080AF RID: 32943 RVA: 0x0005798C File Offset: 0x00055B8C
		public override TaskStatus OnUpdate()
		{
			if (this.sharedTransform.Value == null)
			{
				return 1;
			}
			this.sharedGameObject.Value = this.sharedTransform.Value.gameObject;
			return 2;
		}

		// Token: 0x060080B0 RID: 32944 RVA: 0x000579BF File Offset: 0x00055BBF
		public override void OnReset()
		{
			this.sharedTransform = null;
			this.sharedGameObject = null;
		}

		// Token: 0x04006D65 RID: 28005
		[Tooltip("The Transform component")]
		public SharedTransform sharedTransform;

		// Token: 0x04006D66 RID: 28006
		[RequiredField]
		[Tooltip("The GameObject to set")]
		public SharedGameObject sharedGameObject;
	}
}
