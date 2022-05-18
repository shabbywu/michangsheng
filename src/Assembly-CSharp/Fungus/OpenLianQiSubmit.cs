using System;
using script.Submit;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001448 RID: 5192
	[CommandInfo("YSTools", "打开炼器提交界面", "打开炼器提交界面", 0)]
	[AddComponentMenu("")]
	public class OpenLianQiSubmit : Command
	{
		// Token: 0x06007D6F RID: 32111 RVA: 0x00054D5B File Offset: 0x00052F5B
		public override void OnEnter()
		{
			SubmitOpenMag.OpenLianQiSub(this.TaskId.Value);
			this.Continue();
		}

		// Token: 0x04006AE2 RID: 27362
		[Tooltip("任务Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskId;
	}
}
