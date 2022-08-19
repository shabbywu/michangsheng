using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001042 RID: 4162
	[TaskCategory("Basic/String")]
	[TaskDescription("Creates a string from multiple other strings.")]
	public class BuildString : Action
	{
		// Token: 0x0600721E RID: 29214 RVA: 0x002AD45C File Offset: 0x002AB65C
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.source.Length; i++)
			{
				SharedString sharedString = this.storeResult;
				sharedString.Value += this.source[i];
			}
			return 2;
		}

		// Token: 0x0600721F RID: 29215 RVA: 0x002AD49B File Offset: 0x002AB69B
		public override void OnReset()
		{
			this.source = null;
			this.storeResult = null;
		}

		// Token: 0x04005DF6 RID: 24054
		[Tooltip("The array of strings")]
		public SharedString[] source;

		// Token: 0x04005DF7 RID: 24055
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
