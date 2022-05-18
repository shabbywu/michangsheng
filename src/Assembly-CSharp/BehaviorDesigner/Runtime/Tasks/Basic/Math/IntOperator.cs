using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015C2 RID: 5570
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs a math operation on two integers: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class IntOperator : Action
	{
		// Token: 0x060082CB RID: 33483 RVA: 0x002CE1A0 File Offset: 0x002CC3A0
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case IntOperator.Operation.Add:
				this.storeResult.Value = this.integer1.Value + this.integer2.Value;
				break;
			case IntOperator.Operation.Subtract:
				this.storeResult.Value = this.integer1.Value - this.integer2.Value;
				break;
			case IntOperator.Operation.Multiply:
				this.storeResult.Value = this.integer1.Value * this.integer2.Value;
				break;
			case IntOperator.Operation.Divide:
				this.storeResult.Value = this.integer1.Value / this.integer2.Value;
				break;
			case IntOperator.Operation.Min:
				this.storeResult.Value = Mathf.Min(this.integer1.Value, this.integer2.Value);
				break;
			case IntOperator.Operation.Max:
				this.storeResult.Value = Mathf.Max(this.integer1.Value, this.integer2.Value);
				break;
			case IntOperator.Operation.Modulo:
				this.storeResult.Value = this.integer1.Value % this.integer2.Value;
				break;
			}
			return 2;
		}

		// Token: 0x060082CC RID: 33484 RVA: 0x00059BA1 File Offset: 0x00057DA1
		public override void OnReset()
		{
			this.operation = IntOperator.Operation.Add;
			this.integer1.Value = 0;
			this.integer2.Value = 0;
			this.storeResult.Value = 0;
		}

		// Token: 0x04006F9A RID: 28570
		[Tooltip("The operation to perform")]
		public IntOperator.Operation operation;

		// Token: 0x04006F9B RID: 28571
		[Tooltip("The first integer")]
		public SharedInt integer1;

		// Token: 0x04006F9C RID: 28572
		[Tooltip("The second integer")]
		public SharedInt integer2;

		// Token: 0x04006F9D RID: 28573
		[RequiredField]
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;

		// Token: 0x020015C3 RID: 5571
		public enum Operation
		{
			// Token: 0x04006F9F RID: 28575
			Add,
			// Token: 0x04006FA0 RID: 28576
			Subtract,
			// Token: 0x04006FA1 RID: 28577
			Multiply,
			// Token: 0x04006FA2 RID: 28578
			Divide,
			// Token: 0x04006FA3 RID: 28579
			Min,
			// Token: 0x04006FA4 RID: 28580
			Max,
			// Token: 0x04006FA5 RID: 28581
			Modulo
		}
	}
}
