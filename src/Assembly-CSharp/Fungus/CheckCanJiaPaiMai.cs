using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckCanJiaPaiMai", "检测是否参加过拍卖", 0)]
[AddComponentMenu("")]
public class CheckCanJiaPaiMai : Command
{
	[Tooltip("拍卖行的ID")]
	[SerializeField]
	protected int PaiMaiID;

	[Tooltip("获取到的值存放位置")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempValue;

	public override void OnEnter()
	{
		TempValue.Value = Tools.instance.getPlayer().StreamData.PaiMaiDataMag.IsJoin(PaiMaiID);
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
