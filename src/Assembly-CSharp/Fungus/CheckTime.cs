using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckTime", "检测当前时间", 0)]
[AddComponentMenu("")]
public class CheckTime : Command
{
	[Tooltip("比较类型，大于 小于 等于")]
	[SerializeField]
	protected ItemCheck.CompareNum CompareType;

	[Tooltip("获取到的修为值存放位置")]
	[SerializeField]
	protected string Time;

	[Tooltip("将检测到的值赋给一个变量")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempBool;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		bool value = false;
		DateTime dateTime = DateTime.Parse(Time);
		DateTime nowTime = player.worldTimeMag.getNowTime();
		if (CompareType == ItemCheck.CompareNum.GreaterThan)
		{
			if (dateTime > nowTime)
			{
				value = true;
			}
		}
		else if (CompareType == ItemCheck.CompareNum.LessThan)
		{
			if (dateTime < nowTime)
			{
				value = true;
			}
		}
		else if (CompareType == ItemCheck.CompareNum.equalTo && dateTime == nowTime)
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
