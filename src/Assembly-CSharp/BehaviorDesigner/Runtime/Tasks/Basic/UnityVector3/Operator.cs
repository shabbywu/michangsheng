using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x0200100A RID: 4106
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Performs a math operation on two Vector3s: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class Operator : Action
	{
		// Token: 0x06007157 RID: 29015 RVA: 0x002AB888 File Offset: 0x002A9A88
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

		// Token: 0x06007158 RID: 29016 RVA: 0x002AB928 File Offset: 0x002A9B28
		public override void OnReset()
		{
			this.operation = Operator.Operation.Add;
			this.firstVector3 = (this.secondVector3 = (this.storeResult = Vector3.zero));
		}

		// Token: 0x04005D31 RID: 23857
		[Tooltip("The operation to perform")]
		public Operator.Operation operation;

		// Token: 0x04005D32 RID: 23858
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04005D33 RID: 23859
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04005D34 RID: 23860
		[Tooltip("The variable to store the result")]
		public SharedVector3 storeResult;

		// Token: 0x02001731 RID: 5937
		public enum Operation
		{
			// Token: 0x04007550 RID: 30032
			Add,
			// Token: 0x04007551 RID: 30033
			Subtract,
			// Token: 0x04007552 RID: 30034
			Scale
		}
	}
}
