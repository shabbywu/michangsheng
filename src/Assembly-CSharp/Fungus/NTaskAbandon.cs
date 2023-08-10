using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTask", "NTaskAbandon", "放弃任务", 0)]
[AddComponentMenu("")]
public class NTaskAbandon : Command
{
	[Tooltip("需要放弃的任务ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NTaskID;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().nomelTaskMag.TimeOutEndTask(NTaskID.Value);
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
