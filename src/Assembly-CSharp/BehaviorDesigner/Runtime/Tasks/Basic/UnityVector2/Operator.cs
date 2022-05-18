using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014D4 RID: 5332
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Performs a math operation on two Vector2s: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class Operator : Action
	{
		// Token: 0x06007F84 RID: 32644 RVA: 0x002CA1F4 File Offset: 0x002C83F4
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

		// Token: 0x06007F85 RID: 32645 RVA: 0x002CA294 File Offset: 0x002C8494
		public override void OnReset()
		{
			this.operation = Operator.Operation.Add;
			this.firstVector2 = (this.secondVector2 = (this.storeResult = Vector2.zero));
		}

		// Token: 0x04006C5D RID: 27741
		[Tooltip("The operation to perform")]
		public Operator.Operation operation;

		// Token: 0x04006C5E RID: 27742
		[Tooltip("The first Vector2")]
		public SharedVector2 firstVector2;

		// Token: 0x04006C5F RID: 27743
		[Tooltip("The second Vector2")]
		public SharedVector2 secondVector2;

		// Token: 0x04006C60 RID: 27744
		[Tooltip("The variable to store the result")]
		public SharedVector2 storeResult;

		// Token: 0x020014D5 RID: 5333
		public enum Operation
		{
			// Token: 0x04006C62 RID: 27746
			Add,
			// Token: 0x04006C63 RID: 27747
			Subtract,
			// Token: 0x04006C64 RID: 27748
			Scale
		}
	}
}
