using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddEXP", "增加经验", 0)]
[AddComponentMenu("")]
public class AddEXP : Command
{
	[Tooltip("增加经验的数量")]
	[SerializeField]
	public int AddEXPNum;

	public override void OnEnter()
	{
		UIPopTip.Inst.Pop("你的修为提升了" + AddEXPNum, PopTipIconType.上箭头);
		Tools.instance.getPlayer().addEXP(AddEXPNum);
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
