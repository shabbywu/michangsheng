using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString
{
	// Token: 0x02001045 RID: 4165
	[TaskCategory("Basic/String")]
	[TaskDescription("Stores the length of the string")]
	public class GetLength : Action
	{
		// Token: 0x06007228 RID: 29224 RVA: 0x002AD5BC File Offset: 0x002AB7BC
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.targetString.Value.Length;
			return 2;
		}

		// Token: 0x06007229 RID: 29225 RVA: 0x002AD5DA File Offset: 0x002AB7DA
		public override void OnReset()
		{
			this.targetString = "";
			this.storeResult = 0;
		}

		// Token: 0x04005DFF RID: 24063
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x04005E00 RID: 24064
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedInt storeResult;
	}
}
