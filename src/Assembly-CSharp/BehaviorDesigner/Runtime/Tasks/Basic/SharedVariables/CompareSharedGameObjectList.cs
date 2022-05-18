using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200150D RID: 5389
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedGameObjectList : Conditional
	{
		// Token: 0x06008050 RID: 32848 RVA: 0x002CB070 File Offset: 0x002C9270
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

		// Token: 0x06008051 RID: 32849 RVA: 0x000574AF File Offset: 0x000556AF
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04006D26 RID: 27942
		[Tooltip("The first variable to compare")]
		public SharedGameObjectList variable;

		// Token: 0x04006D27 RID: 27943
		[Tooltip("The variable to compare to")]
		public SharedGameObjectList compareTo;
	}
}
