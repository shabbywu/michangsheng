using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTask", "GetNTaskChildValueList", "获取某个任务的子项的值", 0)]
[AddComponentMenu("")]
public class GetNTaskChildValueList : Command
{
	[Tooltip("需要获取的任务ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NTaskID;

	[Tooltip("值列表")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	public List<IntegerVariable> ValueList;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		List<JSONObject> nTaskXiangXiList = player.nomelTaskMag.GetNTaskXiangXiList(NTaskID.Value);
		int num = 0;
		foreach (JSONObject item in nTaskXiangXiList)
		{
			_ = item;
			int chilidID = player.nomelTaskMag.getChilidID(NTaskID.Value, num);
			JSONObject jSONObject = jsonData.instance.NTaskSuiJI[chilidID.ToString()];
			if ((Object)(object)ValueList[num] != (Object)null)
			{
				ValueList[num].Value = jSONObject["Value"].I;
			}
			num++;
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
