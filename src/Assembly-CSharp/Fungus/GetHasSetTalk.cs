using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetHasSetTalk", "根据流派和境界获取NpcId", 0)]
[AddComponentMenu("")]
public class GetHasSetTalk : Command
{
	[Tooltip("是否有setTalk")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable flag;

	public override void OnEnter()
	{
		if (GlobalValue.Get(0, GetCommandSourceDesc()) > 0)
		{
			flag.Value = true;
		}
		else
		{
			flag.Value = false;
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
