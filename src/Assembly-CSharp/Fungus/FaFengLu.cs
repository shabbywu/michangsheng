using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "FaFengLu", "发放俸禄", 0)]
[AddComponentMenu("")]
public class FaFengLu : Command
{
	[Tooltip("说明")]
	[SerializeField]
	protected string init = "发放俸禄，发放后的总钱数保存到Money的值当中";

	[Tooltip("存放总俸禄钱数")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable Money;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		int allFengLuMoney = player.chenghaomag.GetAllFengLuMoney();
		if (allFengLuMoney > 0)
		{
			player.chenghaomag.GiveMoney();
		}
		Money.Value = allFengLuMoney;
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
