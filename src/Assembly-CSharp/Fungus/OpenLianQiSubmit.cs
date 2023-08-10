using System;
using UnityEngine;
using script.Submit;

namespace Fungus;

[CommandInfo("YSTools", "打开炼器提交界面", "打开炼器提交界面", 0)]
[AddComponentMenu("")]
public class OpenLianQiSubmit : Command
{
	[Tooltip("任务Id")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TaskId;

	public override void OnEnter()
	{
		SubmitOpenMag.OpenLianQiSub(TaskId.Value);
		Continue();
	}
}
