using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200150E RID: 5390
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedInt : Conditional
	{
		// Token: 0x06008053 RID: 32851 RVA: 0x002CB120 File Offset: 0x002C9320
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008054 RID: 32852 RVA: 0x000574BF File Offset: 0x000556BF
		public override void OnReset()
		{
			this.variable = 0;
			this.compareTo = 0;
		}

		// Token: 0x04006D28 RID: 27944
		[Tooltip("The first variable to compare")]
		public SharedInt variable;

		// Token: 0x04006D29 RID: 27945
		[Tooltip("The variable to compare to")]
		public SharedInt compareTo;
	}
}
