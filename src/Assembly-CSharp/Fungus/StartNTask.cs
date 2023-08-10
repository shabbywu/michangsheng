using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTask", "StartNTask", "开始一个任务", 0)]
[AddComponentMenu("")]
public class StartNTask : Command
{
	[Tooltip("需要开始的任务ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NTaskID;

	public override void OnEnter()
	{
		Do(NTaskID.Value);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public static void Do(int _NTaskID)
	{
		Avatar player = Tools.instance.getPlayer();
		if (!player.nomelTaskMag.IsNTaskStart(_NTaskID))
		{
			player.nomelTaskMag.StartNTask(_NTaskID);
		}
	}
}
