using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "Talk是否结束", "Talk是否结束", 0)]
[AddComponentMenu("")]
public class IsEnd : Command, INoCommand
{
	[Tooltip("结果")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable result;

	public override void OnEnter()
	{
		result.Value = Tools.instance.getPlayer().StreamData.FungusSaveMgr.LastIsEnd;
		Continue();
	}
}
