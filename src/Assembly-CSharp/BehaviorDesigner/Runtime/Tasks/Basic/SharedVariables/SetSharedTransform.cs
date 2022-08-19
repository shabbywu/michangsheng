using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200106A RID: 4202
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedTransform variable to the specified object. Returns Success.")]
	public class SetSharedTransform : Action
	{
		// Token: 0x0600729B RID: 29339 RVA: 0x002AE3C0 File Offset: 0x002AC5C0
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = ((this.targetValue.Value != null) ? this.targetValue.Value : this.transform);
			return 2;
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x002AE3F4 File Offset: 0x002AC5F4
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04005E55 RID: 24149
		[Tooltip("The value to set the SharedTransform to. If null the variable will be set to the current Transform")]
		public SharedTransform targetValue;

		// Token: 0x04005E56 RID: 24150
		[RequiredField]
		[Tooltip("The SharedTransform to set")]
		public SharedTransform targetVariable;
	}
}
