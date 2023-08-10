using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "打开炼器提交界面", "打开炼器提交界面", 0)]
[AddComponentMenu("")]
public class OpenLianQiTiJiao : Command
{
	[Tooltip("TaskId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TaskId;

	public override void OnEnter()
	{
		Continue();
	}
}
