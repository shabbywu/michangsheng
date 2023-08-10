using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "IsCyFriend", "检查是否是传音符好友", 0)]
[AddComponentMenu("")]
public class IsCyFriend : Command
{
	[Tooltip("npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	[Tooltip("结果")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable result;

	public override void OnEnter()
	{
		if (Tools.instance.getPlayer().emailDateMag.IsFriend(npcId.Value))
		{
			result.Value = true;
		}
		else
		{
			result.Value = false;
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
