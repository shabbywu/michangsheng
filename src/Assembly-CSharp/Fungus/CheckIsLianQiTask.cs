using System;
using JSONClass;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "检测是否是炼器任务", "检测是否是炼器任务", 0)]
[AddComponentMenu("")]
public class CheckIsLianQiTask : Command
{
	[Tooltip("任务Id")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TaskId;

	[Tooltip("结果")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable Result;

	public override void OnEnter()
	{
		try
		{
			bool num = NTaskAllType.DataDict[TaskId.Value].Type == 1;
			int i = Tools.instance.getPlayer().NomelTaskJson[TaskId.Value.ToString()]["TaskChild"][0].I;
			bool flag = NTaskSuiJI.DataDict[i].Str.Contains("lianqi");
			if (num && flag)
			{
				Result.Value = true;
			}
			else
			{
				Result.Value = false;
			}
		}
		catch (Exception)
		{
			Result.Value = false;
		}
		Continue();
	}
}
