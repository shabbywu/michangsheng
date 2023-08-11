using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddCyFriend", "增加传音符好友", 0)]
[AddComponentMenu("")]
public class AddCyFriend : Command
{
	[Tooltip("增加npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().AddFriend(npcId.Value);
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
