using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetNewNpcDeath", "设置npc死亡", 0)]
[AddComponentMenu("")]
public class SetNewNpcDeath : Command
{
	[Tooltip("指定npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	public override void OnEnter()
	{
		int key = npcId.Value;
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(key))
		{
			key = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[key];
		}
		if (NpcJieSuanManager.inst.isCanJieSuan)
		{
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(10, key);
		}
		else
		{
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(10, key, 0, after: true);
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
