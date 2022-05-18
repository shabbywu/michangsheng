using System;
using JSONClass;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001432 RID: 5170
	[CommandInfo("YSTools", "检测是否是炼器任务", "检测是否是炼器任务", 0)]
	[AddComponentMenu("")]
	public class CheckIsLianQiTask : Command
	{
		// Token: 0x06007D14 RID: 32020 RVA: 0x00054A23 File Offset: 0x00052C23
		public override void OnEnter()
		{
			this.Result.Value = (NTaskAllType.DataDict[this.TaskId.Value].Type == 1);
			this.Continue();
		}

		// Token: 0x04006AC1 RID: 27329
		[Tooltip("任务Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskId;

		// Token: 0x04006AC2 RID: 27330
		[Tooltip("结果")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable Result;
	}
}
