using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001503 RID: 5379
	[TaskCategory("Basic/String")]
	[TaskDescription("Replaces a string with the new string")]
	public class Replace : Action
	{
		// Token: 0x0600802E RID: 32814 RVA: 0x000572A9 File Offset: 0x000554A9
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.targetString.Value.Replace(this.oldString.Value, this.newString.Value);
			return 2;
		}

		// Token: 0x0600802F RID: 32815 RVA: 0x002CAE10 File Offset: 0x002C9010
		public override void OnReset()
		{
			this.targetString = "";
			this.oldString = "";
			this.newString = "";
			this.storeResult = "";
		}

		// Token: 0x04006D08 RID: 27912
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x04006D09 RID: 27913
		[Tooltip("The old replace")]
		public SharedString oldString;

		// Token: 0x04006D0A RID: 27914
		[Tooltip("The new string")]
		public SharedString newString;

		// Token: 0x04006D0B RID: 27915
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
