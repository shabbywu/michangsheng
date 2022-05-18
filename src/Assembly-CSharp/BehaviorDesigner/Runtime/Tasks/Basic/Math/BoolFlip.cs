using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015B5 RID: 5557
	[TaskCategory("Basic/Math")]
	[TaskDescription("Flips the value of the bool.")]
	public class BoolFlip : Action
	{
		// Token: 0x060082B0 RID: 33456 RVA: 0x00059A03 File Offset: 0x00057C03
		public override TaskStatus OnUpdate()
		{
			this.boolVariable.Value = !this.boolVariable.Value;
			return 2;
		}

		// Token: 0x060082B1 RID: 33457 RVA: 0x00059A1F File Offset: 0x00057C1F
		public override void OnReset()
		{
			this.boolVariable.Value = false;
		}

		// Token: 0x04006F68 RID: 28520
		[Tooltip("The bool to flip the value of")]
		public SharedBool boolVariable;
	}
}
