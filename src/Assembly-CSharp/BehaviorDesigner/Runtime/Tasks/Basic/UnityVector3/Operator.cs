using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014C2 RID: 5314
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Performs a math operation on two Vector3s: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class Operator : Action
	{
		// Token: 0x06007F51 RID: 32593 RVA: 0x002C9D4C File Offset: 0x002C7F4C
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case Operator.Operation.Add:
				this.storeResult.Value = this.firstVector3.Value + this.secondVector3.Value;
				break;
			case Operator.Operation.Subtract:
				this.storeResult.Value = this.firstVector3.Value - this.secondVector3.Value;
				break;
			case Operator.Operation.Scale:
				this.storeResult.Value = Vector3.Scale(this.firstVector3.Value, this.secondVector3.Value);
				break;
			}
			return 2;
		}

		// Token: 0x06007F52 RID: 32594 RVA: 0x002C9DEC File Offset: 0x002C7FEC
		public override void OnReset()
		{
			this.operation = Operator.Operation.Add;
			this.firstVector3 = (this.secondVector3 = (this.storeResult = Vector3.zero));
		}

		// Token: 0x04006C29 RID: 27689
		[Tooltip("The operation to perform")]
		public Operator.Operation operation;

		// Token: 0x04006C2A RID: 27690
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04006C2B RID: 27691
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04006C2C RID: 27692
		[Tooltip("The variable to store the result")]
		public SharedVector3 storeResult;

		// Token: 0x020014C3 RID: 5315
		public enum Operation
		{
			// Token: 0x04006C2E RID: 27694
			Add,
			// Token: 0x04006C2F RID: 27695
			Subtract,
			// Token: 0x04006C30 RID: 27696
			Scale
		}
	}
}
