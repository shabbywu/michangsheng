using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x020014FF RID: 5375
	[TaskCategory("Basic/String")]
	[TaskDescription("Stores the length of the string")]
	public class GetLength : Action
	{
		// Token: 0x06008022 RID: 32802 RVA: 0x000571B2 File Offset: 0x000553B2
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.targetString.Value.Length;
			return 2;
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x000571D0 File Offset: 0x000553D0
		public override void OnReset()
		{
			this.targetString = "";
			this.storeResult = 0;
		}

		// Token: 0x04006CFF RID: 27903
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x04006D00 RID: 27904
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedInt storeResult;
	}
}
