using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001525 RID: 5413
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedTransformList variable to the specified object. Returns Success.")]
	public class SetSharedTransformList : Action
	{
		// Token: 0x06008098 RID: 32920 RVA: 0x0005782B File Offset: 0x00055A2B
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06008099 RID: 32921 RVA: 0x00057844 File Offset: 0x00055A44
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04006D57 RID: 27991
		[Tooltip("The value to set the SharedTransformList to.")]
		public SharedTransformList targetValue;

		// Token: 0x04006D58 RID: 27992
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedTransformList targetVariable;
	}
}
