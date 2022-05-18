using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200150F RID: 5391
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedObject : Conditional
	{
		// Token: 0x06008056 RID: 32854 RVA: 0x002CB150 File Offset: 0x002C9350
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

		// Token: 0x06008057 RID: 32855 RVA: 0x000574D9 File Offset: 0x000556D9
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04006D2A RID: 27946
		[Tooltip("The first variable to compare")]
		public SharedObject variable;

		// Token: 0x04006D2B RID: 27947
		[Tooltip("The variable to compare to")]
		public SharedObject compareTo;
	}
}
