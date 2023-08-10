using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetXinJin", "获取玩家心境保存到TempValue中", 0)]
[AddComponentMenu("")]
public class GetXinJin : Command
{
	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		GetFlowchart().SetIntegerVariable("TempValue", player.xinjin);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
