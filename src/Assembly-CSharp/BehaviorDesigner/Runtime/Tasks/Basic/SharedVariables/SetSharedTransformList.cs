using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200106B RID: 4203
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedTransformList variable to the specified object. Returns Success.")]
	public class SetSharedTransformList : Action
	{
		// Token: 0x0600729E RID: 29342 RVA: 0x002AE404 File Offset: 0x002AC604
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x002AE41D File Offset: 0x002AC61D
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04005E57 RID: 24151
		[Tooltip("The value to set the SharedTransformList to.")]
		public SharedTransformList targetValue;

		// Token: 0x04005E58 RID: 24152
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedTransformList targetVariable;
	}
}
