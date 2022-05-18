using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001510 RID: 5392
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedObjectList : Conditional
	{
		// Token: 0x06008059 RID: 32857 RVA: 0x002CB1D0 File Offset: 0x002C93D0
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
			if (this.variable.Value.Count != this.compareTo.Value.Count)
			{
				return 1;
			}
			for (int i = 0; i < this.variable.Value.Count; i++)
			{
				if (this.variable.Value[i] != this.compareTo.Value[i])
				{
					return 1;
				}
			}
			return 2;
		}

		// Token: 0x0600805A RID: 32858 RVA: 0x000574E9 File Offset: 0x000556E9
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04006D2C RID: 27948
		[Tooltip("The first variable to compare")]
		public SharedObjectList variable;

		// Token: 0x04006D2D RID: 27949
		[Tooltip("The variable to compare to")]
		public SharedObjectList compareTo;
	}
}
