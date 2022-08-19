using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001051 RID: 4177
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedFloat : Conditional
	{
		// Token: 0x06007250 RID: 29264 RVA: 0x002ADAB0 File Offset: 0x002ABCB0
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007251 RID: 29265 RVA: 0x002ADAE0 File Offset: 0x002ABCE0
		public override void OnReset()
		{
			this.variable = 0f;
			this.compareTo = 0f;
		}

		// Token: 0x04005E22 RID: 24098
		[Tooltip("The first variable to compare")]
		public SharedFloat variable;

		// Token: 0x04005E23 RID: 24099
		[Tooltip("The variable to compare to")]
		public SharedFloat compareTo;
	}
}
