using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetNowSeaID", "获取当前海域ID", 0)]
[AddComponentMenu("")]
public class GetNowSeaID : Command
{
	[Tooltip("海域ID存放的位置")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable SeaID;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		SeaID.Value = Do();
		Continue();
	}

	public static int Do()
	{
		Tools.instance.getPlayer();
		int result = 0;
		int.TryParse(Tools.getScreenName().Replace("Sea", ""), out result);
		return result;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
