using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "检测任务是否过期", "检测任务是否过期", 0)]
[AddComponentMenu("")]
public class CheckTaskIsOut : Command
{
	[Tooltip("TaskId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TaskId;

	[Tooltip("返回结果")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable Result;

	public override void OnEnter()
	{
		if (Tools.instance.getPlayer().taskMag._TaskData["Task"].HasField(((object)TaskId).ToString()))
		{
			Result.Value = TaskUIManager.checkIsGuoShi(Tools.instance.getPlayer().taskMag._TaskData["Task"][((object)TaskId).ToString()]);
		}
		else
		{
			Result.Value = false;
		}
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
