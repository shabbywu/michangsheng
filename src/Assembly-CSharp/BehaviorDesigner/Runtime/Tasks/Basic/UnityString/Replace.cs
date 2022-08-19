using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001049 RID: 4169
	[TaskCategory("Basic/String")]
	[TaskDescription("Replaces a string with the new string")]
	public class Replace : Action
	{
		// Token: 0x06007234 RID: 29236 RVA: 0x002AD728 File Offset: 0x002AB928
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.targetString.Value.Replace(this.oldString.Value, this.newString.Value);
			return 2;
		}

		// Token: 0x06007235 RID: 29237 RVA: 0x002AD75C File Offset: 0x002AB95C
		public override void OnReset()
		{
			this.targetString = "";
			this.oldString = "";
			this.newString = "";
			this.storeResult = "";
		}

		// Token: 0x04005E08 RID: 24072
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x04005E09 RID: 24073
		[Tooltip("The old replace")]
		public SharedString oldString;

		// Token: 0x04005E0A RID: 24074
		[Tooltip("The new string")]
		public SharedString newString;

		// Token: 0x04005E0B RID: 24075
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
