using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200151F RID: 5407
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedObject variable to the specified object. Returns Success.")]
	public class SetSharedObject : Action
	{
		// Token: 0x06008086 RID: 32902 RVA: 0x00057706 File Offset: 0x00055906
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return 2;
		}

		// Token: 0x06008087 RID: 32903 RVA: 0x0005771F File Offset: 0x0005591F
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04006D4B RID: 27979
		[Tooltip("The value to set the SharedObject to")]
		public SharedObject targetValue;

		// Token: 0x04006D4C RID: 27980
		[RequiredField]
		[Tooltip("The SharedTransform to set")]
		public SharedObject targetVariable;
	}
}
