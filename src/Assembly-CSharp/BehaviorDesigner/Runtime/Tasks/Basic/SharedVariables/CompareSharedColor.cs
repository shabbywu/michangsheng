using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200150A RID: 5386
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedColor : Conditional
	{
		// Token: 0x06008047 RID: 32839 RVA: 0x002CAF90 File Offset: 0x002C9190
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008048 RID: 32840 RVA: 0x0005745B File Offset: 0x0005565B
		public override void OnReset()
		{
			this.variable = Color.black;
			this.compareTo = Color.black;
		}

		// Token: 0x04006D20 RID: 27936
		[Tooltip("The first variable to compare")]
		public SharedColor variable;

		// Token: 0x04006D21 RID: 27937
		[Tooltip("The variable to compare to")]
		public SharedColor compareTo;
	}
}
