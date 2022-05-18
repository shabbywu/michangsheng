using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x020014FC RID: 5372
	[TaskCategory("Basic/String")]
	[TaskDescription("Creates a string from multiple other strings.")]
	public class BuildString : Action
	{
		// Token: 0x06008018 RID: 32792 RVA: 0x002CACD4 File Offset: 0x002C8ED4
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.source.Length; i++)
			{
				SharedString sharedString = this.storeResult;
				sharedString.Value += this.source[i];
			}
			return 2;
		}

		// Token: 0x06008019 RID: 32793 RVA: 0x00057116 File Offset: 0x00055316
		public override void OnReset()
		{
			this.source = null;
			this.storeResult = null;
		}

		// Token: 0x04006CF6 RID: 27894
		[Tooltip("The array of strings")]
		public SharedString[] source;

		// Token: 0x04006CF7 RID: 27895
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
