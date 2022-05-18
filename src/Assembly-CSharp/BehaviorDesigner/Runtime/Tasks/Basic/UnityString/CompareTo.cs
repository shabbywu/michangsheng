using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x020014FD RID: 5373
	[TaskCategory("Basic/String")]
	[TaskDescription("Compares the first string to the second string. Returns an int which indicates whether the first string precedes, matches, or follows the second string.")]
	public class CompareTo : Action
	{
		// Token: 0x0600801B RID: 32795 RVA: 0x00057126 File Offset: 0x00055326
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.firstString.Value.CompareTo(this.secondString.Value);
			return 2;
		}

		// Token: 0x0600801C RID: 32796 RVA: 0x0005714F File Offset: 0x0005534F
		public override void OnReset()
		{
			this.firstString = "";
			this.secondString = "";
			this.storeResult = 0;
		}

		// Token: 0x04006CF8 RID: 27896
		[Tooltip("The string to compare")]
		public SharedString firstString;

		// Token: 0x04006CF9 RID: 27897
		[Tooltip("The string to compare to")]
		public SharedString secondString;

		// Token: 0x04006CFA RID: 27898
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedInt storeResult;
	}
}
