using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001047 RID: 4167
	[TaskCategory("Basic/String")]
	[TaskDescription("Stores a substring of the target string")]
	public class GetSubstring : Action
	{
		// Token: 0x0600722E RID: 29230 RVA: 0x002AD630 File Offset: 0x002AB830
		public override TaskStatus OnUpdate()
		{
			if (this.length.Value != -1)
			{
				this.storeResult.Value = this.targetString.Value.Substring(this.startIndex.Value, this.length.Value);
			}
			else
			{
				this.storeResult.Value = this.targetString.Value.Substring(this.startIndex.Value);
			}
			return 2;
		}

		// Token: 0x0600722F RID: 29231 RVA: 0x002AD6A5 File Offset: 0x002AB8A5
		public override void OnReset()
		{
			this.targetString = "";
			this.startIndex = 0;
			this.length = -1;
			this.storeResult = "";
		}

		// Token: 0x04005E03 RID: 24067
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x04005E04 RID: 24068
		[Tooltip("The start substring index")]
		public SharedInt startIndex = 0;

		// Token: 0x04005E05 RID: 24069
		[Tooltip("The length of the substring. Don't use if -1")]
		public SharedInt length = -1;

		// Token: 0x04005E06 RID: 24070
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
