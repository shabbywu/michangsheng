using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "resetNTask", "重置任务", 0)]
[AddComponentMenu("")]
public class resetNTask : Command
{
	[Tooltip("任务的ID")]
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
		PlayerEx.Player.nomelTaskMag.randomTask(_NTaskID, Reset: true);
	}
}
