using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001043 RID: 4163
	[TaskCategory("Basic/String")]
	[TaskDescription("Compares the first string to the second string. Returns an int which indicates whether the first string precedes, matches, or follows the second string.")]
	public class CompareTo : Action
	{
		// Token: 0x06007221 RID: 29217 RVA: 0x002AD4AB File Offset: 0x002AB6AB
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.firstString.Value.CompareTo(this.secondString.Value);
			return 2;
		}

		// Token: 0x06007222 RID: 29218 RVA: 0x002AD4D4 File Offset: 0x002AB6D4
		public override void OnReset()
		{
			this.firstString = "";
			this.secondString = "";
			this.storeResult = 0;
		}

		// Token: 0x04005DF8 RID: 24056
		[Tooltip("The string to compare")]
		public SharedString firstString;

		// Token: 0x04005DF9 RID: 24057
		[Tooltip("The string to compare to")]
		public SharedString secondString;

		// Token: 0x04005DFA RID: 24058
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedInt storeResult;
	}
}
