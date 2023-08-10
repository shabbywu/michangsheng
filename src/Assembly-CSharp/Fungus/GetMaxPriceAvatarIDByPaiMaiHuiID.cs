using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetMaxPriceAvatarIDByPaiMaiHuiID", "获取拍卖行出价最高的武将ID", 0)]
[AddComponentMenu("")]
public class GetMaxPriceAvatarIDByPaiMaiHuiID : Command
{
	[Tooltip("拍卖行ID")]
	[SerializeField]
	protected int PaiMaiHuiID;

	[Tooltip("存放值的变量")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable AvatarID;

	public override void OnEnter()
	{
		_ = jsonData.instance.AvatarRandomJsonData[AvatarID.Value.ToString()]["HaoGanDu"].n;
		int num = Tools.instance.getPlayer().PaiMaiMaxMoneyAvatarDate[PaiMaiHuiID.ToString()].I;
		if (jsonData.instance.MonstarIsDeath(num))
		{
			num = -1;
		}
		AvatarID.Value = num;
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
