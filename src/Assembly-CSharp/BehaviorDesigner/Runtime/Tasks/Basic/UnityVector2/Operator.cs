using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x0200101B RID: 4123
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Performs a math operation on two Vector2s: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class Operator : Action
	{
		// Token: 0x0600718A RID: 29066 RVA: 0x002ABF64 File Offset: 0x002AA164
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case Operator.Operation.Add:
				this.storeResult.Value = this.firstVector2.Value + this.secondVector2.Value;
				break;
			case Operator.Operation.Subtract:
				this.storeResult.Value = this.firstVector2.Value - this.secondVector2.Value;
				break;
			case Operator.Operation.Scale:
				this.storeResult.Value = Vector2.Scale(this.firstVector2.Value, this.secondVector2.Value);
				break;
			}
			return 2;
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x002AC004 File Offset: 0x002AA204
		public override void OnReset()
		{
			this.operation = Operator.Operation.Add;
			this.firstVector2 = (this.secondVector2 = (this.storeResult = Vector2.zero));
		}

		// Token: 0x04005D61 RID: 23905
		[Tooltip("The operation to perform")]
		public Operator.Operation operation;

		// Token: 0x04005D62 RID: 23906
		[Tooltip("The first Vector2")]
		public SharedVector2 firstVector2;

		// Token: 0x04005D63 RID: 23907
		[Tooltip("The second Vector2")]
		public SharedVector2 secondVector2;

		// Token: 0x04005D64 RID: 23908
		[Tooltip("The variable to store the result")]
		public SharedVector2 storeResult;

		// Token: 0x02001732 RID: 5938
		public enum Operation
		{
			// Token: 0x04007554 RID: 30036
			Add,
			// Token: 0x04007555 RID: 30037
			Subtract,
			// Token: 0x04007556 RID: 30038
			Scale
		}
	}
}
