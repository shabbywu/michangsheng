using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200150B RID: 5387
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedFloat : Conditional
	{
		// Token: 0x0600804A RID: 32842 RVA: 0x002CAFC0 File Offset: 0x002C91C0
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600804B RID: 32843 RVA: 0x0005747D File Offset: 0x0005567D
		public override void OnReset()
		{
			this.variable = 0f;
			this.compareTo = 0f;
		}

		// Token: 0x04006D22 RID: 27938
		[Tooltip("The first variable to compare")]
		public SharedFloat variable;

		// Token: 0x04006D23 RID: 27939
		[Tooltip("The variable to compare to")]
		public SharedFloat compareTo;
	}
}
