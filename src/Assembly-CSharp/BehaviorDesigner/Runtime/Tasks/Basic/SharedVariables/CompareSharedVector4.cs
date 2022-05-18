using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001518 RID: 5400
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector4 : Conditional
	{
		// Token: 0x06008071 RID: 32881 RVA: 0x002CB4A8 File Offset: 0x002C96A8
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008072 RID: 32882 RVA: 0x000575C3 File Offset: 0x000557C3
		public override void OnReset()
		{
			this.variable = Vector4.zero;
			this.compareTo = Vector4.zero;
		}

		// Token: 0x04006D3C RID: 27964
		[Tooltip("The first variable to compare")]
		public SharedVector4 variable;

		// Token: 0x04006D3D RID: 27965
		[Tooltip("The variable to compare to")]
		public SharedVector4 compareTo;
	}
}
