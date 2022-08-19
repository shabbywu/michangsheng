using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x0200110D RID: 4365
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a float value")]
	public class SetFloat : Action
	{
		// Token: 0x060074EC RID: 29932 RVA: 0x002B382E File Offset: 0x002B1A2E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.floatValue.Value;
			return 2;
		}

		// Token: 0x060074ED RID: 29933 RVA: 0x002B3847 File Offset: 0x002B1A47
		public override void OnReset()
		{
			this.floatValue.Value = 0f;
			this.storeResult.Value = 0f;
		}

		// Token: 0x04006098 RID: 24728
		[Tooltip("The float value to set")]
		public SharedFloat floatValue;

		// Token: 0x04006099 RID: 24729
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
