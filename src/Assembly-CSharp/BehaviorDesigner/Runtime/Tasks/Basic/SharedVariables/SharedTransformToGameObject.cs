using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001072 RID: 4210
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Gets the GameObject from the Transform component. Returns Success.")]
	public class SharedTransformToGameObject : Action
	{
		// Token: 0x060072B5 RID: 29365 RVA: 0x002AE629 File Offset: 0x002AC829
		public override TaskStatus OnUpdate()
		{
			if (this.sharedTransform.Value == null)
			{
				return 1;
			}
			this.sharedGameObject.Value = this.sharedTransform.Value.gameObject;
			return 2;
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x002AE65C File Offset: 0x002AC85C
		public override void OnReset()
		{
			this.sharedTransform = null;
			this.sharedGameObject = null;
		}

		// Token: 0x04005E65 RID: 24165
		[Tooltip("The Transform component")]
		public SharedTransform sharedTransform;

		// Token: 0x04005E66 RID: 24166
		[RequiredField]
		[Tooltip("The GameObject to set")]
		public SharedGameObject sharedGameObject;
	}
}
