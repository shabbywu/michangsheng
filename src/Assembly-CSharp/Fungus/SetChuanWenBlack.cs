using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "将传闻置灰", "将传闻置灰", 0)]
[AddComponentMenu("")]
public class SetChuanWenBlack : Command
{
	[Tooltip("需要置灰的任务的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TaskID;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().taskMag.SetChuanWenBlack(TaskID.Value);
		Continue();
	}
}
