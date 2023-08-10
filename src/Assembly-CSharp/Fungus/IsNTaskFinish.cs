using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTask", "IsNTaskFinish", "判断任务是否在CD中", 0)]
[AddComponentMenu("")]
public class IsNTaskFinish : Command
{
	[Tooltip("需要判断是否开始的任务ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NTaskID;

	[Tooltip("将判断后的值保存到一个变量中")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable IsStart;

	public override void OnEnter()
	{
		IsStart.Value = Do(NTaskID.Value);
		Continue();
	}

	public static bool Do(int NTaskID)
	{
		Avatar player = Tools.instance.getPlayer();
		if (!player.NomelTaskJson.HasField(NTaskID.ToString()))
		{
			return true;
		}
		if (player.NomelTaskJson[NTaskID.ToString()].HasField("IsFirstStart") && player.NomelTaskJson[NTaskID.ToString()]["IsFirstStart"].b && player.NomelTaskJson[NTaskID.ToString()].HasField("IsEnd") && player.NomelTaskJson[NTaskID.ToString()]["IsEnd"].b)
		{
			return true;
		}
		return false;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
