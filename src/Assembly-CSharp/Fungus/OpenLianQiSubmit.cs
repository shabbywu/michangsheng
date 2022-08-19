using System;
using script.Submit;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F95 RID: 3989
	[CommandInfo("YSTools", "打开炼器提交界面", "打开炼器提交界面", 0)]
	[AddComponentMenu("")]
	public class OpenLianQiSubmit : Command
	{
		// Token: 0x06006F88 RID: 28552 RVA: 0x002A70EA File Offset: 0x002A52EA
		public override void OnEnter()
		{
			SubmitOpenMag.OpenLianQiSub(this.TaskId.Value);
			this.Continue();
		}

		// Token: 0x04005C13 RID: 23571
		[Tooltip("任务Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskId;
	}
}
