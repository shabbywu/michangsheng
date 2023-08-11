using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTask", "FinishNTaskNowChild", "完成一个杂闻任务", 0)]
[AddComponentMenu("")]
public class FinishNTaskNowChild : Command
{
	[Tooltip("需要完成子项的任务ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NTaskID;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		int index = player.nomelTaskMag.nowChildNTask(NTaskID.Value);
		player.nomelTaskMag.setTalkIndex(NTaskID.Value, index);
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
