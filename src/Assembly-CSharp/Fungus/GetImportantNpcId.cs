using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetImportantNpcId", "根据固定NpcId获取绑定该Id的NpcId", 0)]
[AddComponentMenu("")]
public class GetImportantNpcId : Command
{
	[Tooltip("Npc绑定Id")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NpcBingDingId;

	[Tooltip("NpcId存放位置")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NpcId;

	public override void OnEnter()
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(NpcBingDingId.Value))
		{
			NpcId.Value = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[NpcBingDingId.Value];
		}
		else
		{
			NpcId.Value = 0;
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
