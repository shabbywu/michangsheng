using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F7E RID: 3966
	[CommandInfo("YSTools", "检测任务是否过期", "检测任务是否过期", 0)]
	[AddComponentMenu("")]
	public class CheckTaskIsOut : Command
	{
		// Token: 0x06006F2E RID: 28462 RVA: 0x002A6854 File Offset: 0x002A4A54
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

		// Token: 0x06006F2F RID: 28463 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BF4 RID: 23540
		[Tooltip("TaskId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskId;

		// Token: 0x04005BF5 RID: 23541
		[Tooltip("返回结果")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable Result;
	}
}
