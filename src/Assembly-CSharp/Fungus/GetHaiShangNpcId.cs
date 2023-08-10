using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetHaiShangNpcId", "根据静态变量Id获取NPCId", 0)]
[AddComponentMenu("")]
public class GetHaiShangNpcId : Command
{
	[Tooltip("静态变量Id")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable staticId;

	[Tooltip("NpcId存放位置")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NpcId;

	public override void OnEnter()
	{
		NpcId.Value = NPCEx.GetSeaNPCID(staticId.Value);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
