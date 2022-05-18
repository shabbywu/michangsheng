using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001514 RID: 5396
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedTransform : Conditional
	{
		// Token: 0x06008065 RID: 32869 RVA: 0x002CB318 File Offset: 0x002C9518
		public override TaskStatus OnUpdate()
		{
			if (this.variable.Value == null && this.compareTo.Value != null)
			{
				return 1;
			}
			if (this.variable.Value == null && this.compareTo.Value == null)
			{
				return 2;
			}
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008066 RID: 32870 RVA: 0x0005755F File Offset: 0x0005575F
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04006D34 RID: 27956
		[Tooltip("The first variable to compare")]
		public SharedTransform variable;

		// Token: 0x04006D35 RID: 27957
		[Tooltip("The variable to compare to")]
		public SharedTransform compareTo;
	}
}
