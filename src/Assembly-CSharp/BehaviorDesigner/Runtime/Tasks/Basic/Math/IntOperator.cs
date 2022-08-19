using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001104 RID: 4356
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs a math operation on two integers: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class IntOperator : Action
	{
		// Token: 0x060074D1 RID: 29905 RVA: 0x002B3404 File Offset: 0x002B1604
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

		// Token: 0x060074D2 RID: 29906 RVA: 0x002B354B File Offset: 0x002B174B
		public override void OnReset()
		{
			this.operation = IntOperator.Operation.Add;
			this.integer1.Value = 0;
			this.integer2.Value = 0;
			this.storeResult.Value = 0;
		}

		// Token: 0x0400607F RID: 24703
		[Tooltip("The operation to perform")]
		public IntOperator.Operation operation;

		// Token: 0x04006080 RID: 24704
		[Tooltip("The first integer")]
		public SharedInt integer1;

		// Token: 0x04006081 RID: 24705
		[Tooltip("The second integer")]
		public SharedInt integer2;

		// Token: 0x04006082 RID: 24706
		[RequiredField]
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;

		// Token: 0x02001737 RID: 5943
		public enum Operation
		{
			// Token: 0x04007573 RID: 30067
			Add,
			// Token: 0x04007574 RID: 30068
			Subtract,
			// Token: 0x04007575 RID: 30069
			Multiply,
			// Token: 0x04007576 RID: 30070
			Divide,
			// Token: 0x04007577 RID: 30071
			Min,
			// Token: 0x04007578 RID: 30072
			Max,
			// Token: 0x04007579 RID: 30073
			Modulo
		}
	}
}
