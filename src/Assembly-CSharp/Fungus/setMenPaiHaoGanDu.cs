using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "setMenPaiHaoGanDu", "设置门派id", 0)]
[AddComponentMenu("")]
public class setMenPaiHaoGanDu : Command
{
	public enum SetType
	{
		set,
		add
	}

	[Tooltip("设置的方式set表示将值设置为该值，add表示在原有的值的基础上进行加减")]
	[SerializeField]
	protected SetType Type;

	[Tooltip("设置门派的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable MenPaiID;

	[Tooltip("设置的值")]
	[SerializeField]
	protected int Value;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		if (Type == SetType.set)
		{
			player.MenPaiHaoGanDu.SetField(((object)MenPaiID).ToString(), Value);
		}
		else if (Type == SetType.add)
		{
			int num = (player.MenPaiHaoGanDu.HasField(((object)MenPaiID).ToString()) ? player.MenPaiHaoGanDu[((object)MenPaiID).ToString()].I : 0);
			player.MenPaiHaoGanDu.SetField(((object)MenPaiID).ToString(), num + Value);
			if (Value > 0)
			{
				UIPopTip.Inst.Pop("你在" + ShiLiHaoGanDuName.DataDict[MenPaiID.Value].ChinaText + "的声望提升了" + Value, PopTipIconType.上箭头);
			}
			else if (Value < 0)
			{
				UIPopTip.Inst.Pop("你在" + ShiLiHaoGanDuName.DataDict[MenPaiID.Value].ChinaText + "的声望降低了" + Mathf.Abs(Value), PopTipIconType.下箭头);
			}
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
