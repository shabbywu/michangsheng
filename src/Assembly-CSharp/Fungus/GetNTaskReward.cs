using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTask", "GetNTaskReward", "获取任务奖励文字描述", 0)]
[AddComponentMenu("")]
public class GetNTaskReward : Command
{
	[Tooltip("需要获取的任务ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NTaskID;

	[Tooltip("奖励描述")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable Desc;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		int money = 0;
		int menpaihuobi = 0;
		player.nomelTaskMag.getReward(NTaskID.Value, ref money, ref menpaihuobi);
		int i = jsonData.instance.NTaskAllType[NTaskID.Value.ToString()]["menpaihuobi"].I;
		if (i == 0 && money > 0)
		{
			Desc.Value = money + "灵石";
		}
		if (i > 0 && menpaihuobi > 0)
		{
			Desc.Value = menpaihuobi + "枚" + _ItemJsonData.DataDict[i].name;
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
