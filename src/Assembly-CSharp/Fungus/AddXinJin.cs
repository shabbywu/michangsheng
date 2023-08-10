using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddXinJin", "增加心境", 0)]
[AddComponentMenu("")]
public class AddXinJin : Command
{
	[Tooltip("增加心境的数量")]
	[SerializeField]
	public int AddXinjinNum;

	public override void OnEnter()
	{
		string text = ((AddXinjinNum >= 0) ? ("提升了" + Math.Abs(AddXinjinNum)) : ("降低了" + Math.Abs(AddXinjinNum)));
		PopTipIconType iconType = ((AddXinjinNum >= 0) ? PopTipIconType.上箭头 : PopTipIconType.下箭头);
		UIPopTip.Inst.Pop("你的心境" + text, iconType);
		Tools.instance.getPlayer().xinjin += AddXinjinNum;
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
