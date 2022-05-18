using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001435 RID: 5173
	[CommandInfo("YSTools", "检测任务是否过期", "检测任务是否过期", 0)]
	[AddComponentMenu("")]
	public class CheckTaskIsOut : Command
	{
		// Token: 0x06007D1E RID: 32030 RVA: 0x002C6370 File Offset: 0x002C4570
		public override void OnEnter()
		{
			if (Tools.instance.getPlayer().taskMag._TaskData["Task"].HasField(this.TaskId.ToString()))
			{
				this.Result.Value = TaskUIManager.checkIsGuoShi(Tools.instance.getPlayer().taskMag._TaskData["Task"][this.TaskId.ToString()]);
			}
			else
			{
				this.Result.Value = false;
			}
			this.Continue();
		}

		// Token: 0x06007D1F RID: 32031 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D20 RID: 32032 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AC8 RID: 27336
		[Tooltip("TaskId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskId;

		// Token: 0x04006AC9 RID: 27337
		[Tooltip("返回结果")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable Result;
	}
}
