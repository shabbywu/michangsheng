using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddMoney", "增加或减少金钱（减少的话直接填负数）", 0)]
[AddComponentMenu("")]
public class AddMoney : Command
{
	[Tooltip("增加金钱的数量")]
	[SerializeField]
	public int AddNum;

	[Tooltip("增加金钱的数量")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable AddSum;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		int num = (int)player.money;
		int num2 = num + AddNum;
		if (AddNum == 0)
		{
			num2 = num + AddSum.Value;
			if (AddSum.Value >= 0)
			{
				UIPopTip.Inst.Pop(Tools.getStr("AddMoney1").Replace("{X}", AddSum.Value.ToString()), PopTipIconType.上箭头);
			}
			else
			{
				UIPopTip.Inst.Pop(Tools.getStr("AddMoney2").Replace("{X}", (-AddSum.Value).ToString()), PopTipIconType.下箭头);
			}
		}
		else if (AddNum >= 0)
		{
			UIPopTip.Inst.Pop(Tools.getStr("AddMoney1").Replace("{X}", AddNum.ToString()), PopTipIconType.上箭头);
		}
		else
		{
			UIPopTip.Inst.Pop(Tools.getStr("AddMoney2").Replace("{X}", (-AddNum).ToString()), PopTipIconType.下箭头);
		}
		if (num2 >= 0)
		{
			player.money = (uint)num2;
		}
		else
		{
			player.money = 0uL;
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
