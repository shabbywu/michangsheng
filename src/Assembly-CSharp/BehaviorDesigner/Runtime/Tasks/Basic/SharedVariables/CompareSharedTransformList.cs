using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001515 RID: 5397
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedTransformList : Conditional
	{
		// Token: 0x06008068 RID: 32872 RVA: 0x002CB398 File Offset: 0x002C9598
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

		// Token: 0x06008069 RID: 32873 RVA: 0x0005756F File Offset: 0x0005576F
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04006D36 RID: 27958
		[Tooltip("The first variable to compare")]
		public SharedTransformList variable;

		// Token: 0x04006D37 RID: 27959
		[Tooltip("The variable to compare to")]
		public SharedTransformList compareTo;
	}
}
