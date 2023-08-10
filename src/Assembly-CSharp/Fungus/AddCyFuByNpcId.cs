using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddCyFuByNpcId", "发送传音符", 0)]
[AddComponentMenu("")]
public class AddCyFuByNpcId : Command
{
	[Tooltip("发送的npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	[Tooltip("发送的c传音符类型")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable cyType;

	public override void OnEnter()
	{
		NpcJieSuanManager.inst.SendFungusCyByNpcId(cyType.Value, npcId.Value);
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
