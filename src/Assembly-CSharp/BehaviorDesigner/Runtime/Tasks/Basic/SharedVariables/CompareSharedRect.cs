using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001058 RID: 4184
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedRect : Conditional
	{
		// Token: 0x06007265 RID: 29285 RVA: 0x002ADE44 File Offset: 0x002AC044
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x002ADE74 File Offset: 0x002AC074
		public override void OnReset()
		{
			this.variable = default(Rect);
			this.compareTo = default(Rect);
		}

		// Token: 0x04005E30 RID: 24112
		[Tooltip("The first variable to compare")]
		public SharedRect variable;

		// Token: 0x04005E31 RID: 24113
		[Tooltip("The variable to compare to")]
		public SharedRect compareTo;
	}
}
