using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetRestTime", "检测副本剩余时间是否大于0", 0)]
[AddComponentMenu("")]
public class GetRestTime : Command
{
	[Tooltip("返回是否拥有的值")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempBool;

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
		string screenName = Tools.getScreenName();
		TempBool.Value = true;
		if (jsonData.instance.FuBenInfoJsonData.HasField(screenName) && player.fubenContorl[screenName].ResidueTimeDay <= 0)
		{
			TempBool.Value = false;
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
