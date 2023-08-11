using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckKeFangTime", "检测客房是否有剩余时间", 0)]
[AddComponentMenu("")]
public class CheckKeFangTime : Command
{
	[Tooltip("需要检测时间的客房的场景名称")]
	[SerializeField]
	protected string ScenceName = "";

	[Tooltip("将检测到的值赋给一个变量")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempBool;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		TempBool.Value = player.zulinContorl.HasTime(ScenceName);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
