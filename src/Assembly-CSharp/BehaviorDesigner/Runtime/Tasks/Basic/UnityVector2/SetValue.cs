using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014D6 RID: 5334
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Sets the value of the Vector2.")]
	public class SetValue : Action
	{
		// Token: 0x06007F87 RID: 32647 RVA: 0x0005670C File Offset: 0x0005490C
		public override TaskStatus OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Value.Value;
			return 2;
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x002CA2CC File Offset: 0x002C84CC
		public override void OnReset()
		{
			this.vector2Value = (this.vector2Variable = Vector2.zero);
		}

		// Token: 0x04006C65 RID: 27749
		[Tooltip("The Vector2 to get the values of")]
		public SharedVector2 vector2Value;

		// Token: 0x04006C66 RID: 27750
		[Tooltip("The Vector2 to set the values of")]
		public SharedVector2 vector2Variable;
	}
}
