using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200150C RID: 5388
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedGameObject : Conditional
	{
		// Token: 0x0600804D RID: 32845 RVA: 0x002CAFF0 File Offset: 0x002C91F0
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

		// Token: 0x0600804E RID: 32846 RVA: 0x0005749F File Offset: 0x0005569F
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04006D24 RID: 27940
		[Tooltip("The first variable to compare")]
		public SharedGameObject variable;

		// Token: 0x04006D25 RID: 27941
		[Tooltip("The variable to compare to")]
		public SharedGameObject compareTo;
	}
}
