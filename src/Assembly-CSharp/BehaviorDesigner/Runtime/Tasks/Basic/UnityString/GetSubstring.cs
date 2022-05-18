using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001501 RID: 5377
	[TaskCategory("Basic/String")]
	[TaskDescription("Stores a substring of the target string")]
	public class GetSubstring : Action
	{
		// Token: 0x06008028 RID: 32808 RVA: 0x002CAD98 File Offset: 0x002C8F98
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

		// Token: 0x06008029 RID: 32809 RVA: 0x00057226 File Offset: 0x00055426
		public override void OnReset()
		{
			this.targetString = "";
			this.startIndex = 0;
			this.length = -1;
			this.storeResult = "";
		}

		// Token: 0x04006D03 RID: 27907
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x04006D04 RID: 27908
		[Tooltip("The start substring index")]
		public SharedInt startIndex = 0;

		// Token: 0x04006D05 RID: 27909
		[Tooltip("The length of the substring. Don't use if -1")]
		public SharedInt length = -1;

		// Token: 0x04006D06 RID: 27910
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
