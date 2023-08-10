using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckFuBenTime", "检测副本时间", 0)]
[AddComponentMenu("")]
public class CheckFuBenTime : Command
{
	[Tooltip("比较类型，大于 小于 等于")]
	[SerializeField]
	protected ItemCheck.CompareNum CompareType;

	[Tooltip("剩余时间：单位 /天")]
	[SerializeField]
	protected int Time;

	[Tooltip("将检测到的值赋给一个变量")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempBool;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		bool value = false;
		string screenName = Tools.getScreenName();
		int num = 0;
		if (jsonData.instance.FuBenInfoJsonData.HasField(screenName))
		{
			num = player.fubenContorl[screenName].ResidueTimeDay;
		}
		int time = Time;
		if (CompareType == ItemCheck.CompareNum.GreaterThan)
		{
			if (num > time)
			{
				value = true;
			}
		}
		else if (CompareType == ItemCheck.CompareNum.LessThan)
		{
			if (num < time)
			{
				value = true;
			}
		}
		else if (CompareType == ItemCheck.CompareNum.equalTo && num == time)
		{
			value = true;
		}
		TempBool.Value = value;
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
