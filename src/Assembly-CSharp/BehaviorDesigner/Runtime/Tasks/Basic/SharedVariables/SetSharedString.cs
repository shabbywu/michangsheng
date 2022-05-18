using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001523 RID: 5411
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedString variable to the specified object. Returns Success.")]
	public class SetSharedString : Action
	{
		// Token: 0x06008092 RID: 32914 RVA: 0x000577AC File Offset: 0x000559AC
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06008093 RID: 32915 RVA: 0x000577C5 File Offset: 0x000559C5
		public override void OnReset()
		{
			this.targetValue = "";
			this.targetVariable = "";
		}

		// Token: 0x04006D53 RID: 27987
		[Tooltip("The value to set the SharedString to")]
		public SharedString targetValue;

		// Token: 0x04006D54 RID: 27988
		[RequiredField]
		[Tooltip("The SharedString to set")]
		public SharedString targetVariable;
	}
}
