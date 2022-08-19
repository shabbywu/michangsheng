using System;
using JSONClass;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F7B RID: 3963
	[CommandInfo("YSTools", "检测是否是炼器任务", "检测是否是炼器任务", 0)]
	[AddComponentMenu("")]
	public class CheckIsLianQiTask : Command
	{
		// Token: 0x06006F24 RID: 28452 RVA: 0x002A6558 File Offset: 0x002A4758
		public override void OnEnter()
		{
			try
			{
				bool flag = NTaskAllType.DataDict[this.TaskId.Value].Type == 1;
				int i = Tools.instance.getPlayer().NomelTaskJson[this.TaskId.Value.ToString()]["TaskChild"][0].I;
				bool flag2 = NTaskSuiJI.DataDict[i].Str.Contains("lianqi");
				if (flag && flag2)
				{
					this.Result.Value = true;
				}
				else
				{
					this.Result.Value = false;
				}
			}
			catch (Exception)
			{
				this.Result.Value = false;
			}
			this.Continue();
		}

		// Token: 0x04005BED RID: 23533
		[Tooltip("任务Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskId;

		// Token: 0x04005BEE RID: 23534
		[Tooltip("结果")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable Result;
	}
}
