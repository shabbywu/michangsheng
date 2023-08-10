using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetNowTime", "获取当前时间并存储到year month day三个变量中", 0)]
[AddComponentMenu("")]
public class GetNowTime : Command
{
	[Tooltip("解释")]
	[SerializeField]
	protected string StaticValueID = "获取当前时间并存储到year month day三个变量中，需要创建对应变量，可单独创建一个";

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		Flowchart flowchart = GetFlowchart();
		DateTime nowTime = player.worldTimeMag.getNowTime();
		setHasVariable("year", nowTime.Year, flowchart);
		setHasVariable("month", nowTime.Month, flowchart);
		setHasVariable("day", nowTime.Day, flowchart);
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
