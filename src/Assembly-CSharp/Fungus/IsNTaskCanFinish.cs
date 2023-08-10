using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTask", "IsNTaskCanFinish", "判断是否做完所有子项任务", 0)]
[AddComponentMenu("")]
public class IsNTaskCanFinish : Command
{
	[Tooltip("需要判断的任务ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NTaskID;

	[Tooltip("将判断后的值保存到一个变量中")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable IsStart;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		IsStart.Value = player.nomelTaskMag.AllXiangXiTaskIsEnd(NTaskID.Value);
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
