using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001513 RID: 5395
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedString : Conditional
	{
		// Token: 0x06008062 RID: 32866 RVA: 0x0005751B File Offset: 0x0005571B
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008063 RID: 32867 RVA: 0x0005753D File Offset: 0x0005573D
		public override void OnReset()
		{
			this.variable = "";
			this.compareTo = "";
		}

		// Token: 0x04006D32 RID: 27954
		[Tooltip("The first variable to compare")]
		public SharedString variable;

		// Token: 0x04006D33 RID: 27955
		[Tooltip("The variable to compare to")]
		public SharedString compareTo;
	}
}
