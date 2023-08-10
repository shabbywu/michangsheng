using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetWuDaoLv", "获取悟道等级保存到TempValue中", 0)]
[AddComponentMenu("")]
public class GetWuDaoLv : Command
{
	[Tooltip("需要获取悟道属性的类型")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable WuDaoType;

	[Tooltip("保存到TempValue中")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TempValue;

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
		try
		{
			TempValue.Value = player.wuDaoMag.getWuDaoLevelByType(WuDaoType.Value);
		}
		catch (Exception)
		{
			Debug.LogError((object)("尚未获得" + WuDaoType.Value + "该类型悟道值"));
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
