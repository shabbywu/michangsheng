using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001524 RID: 5412
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedTransform variable to the specified object. Returns Success.")]
	public class SetSharedTransform : Action
	{
		// Token: 0x06008095 RID: 32917 RVA: 0x000577E7 File Offset: 0x000559E7
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = ((this.targetValue.Value != null) ? this.targetValue.Value : this.transform);
			return 2;
		}

		// Token: 0x06008096 RID: 32918 RVA: 0x0005781B File Offset: 0x00055A1B
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04006D55 RID: 27989
		[Tooltip("The value to set the SharedTransform to. If null the variable will be set to the current Transform")]
		public SharedTransform targetValue;

		// Token: 0x04006D56 RID: 27990
		[RequiredField]
		[Tooltip("The SharedTransform to set")]
		public SharedTransform targetVariable;
	}
}
