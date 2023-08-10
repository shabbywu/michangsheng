using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "SendItemToNpc", "玩家给npc物品", 0)]
[AddComponentMenu("")]
public class SendItemToNpc : Command
{
	[Tooltip("npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable WeiXieItem;

	public override void OnEnter()
	{
		if (NpcJieSuanManager.inst.IsDeath(npcId.Value))
		{
			Continue();
			return;
		}
		List<ITEM_INFO> values = Tools.instance.getPlayer().itemList.values;
		for (int i = 0; i < values.Count; i++)
		{
			if (values[i] != null && values[i].itemId == WeiXieItem.Value)
			{
				NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId.Value, values[i].itemId, 1, values[i].Seid);
				if (values[i].itemCount == 1)
				{
					values.Remove(values[i]);
				}
				else
				{
					values[i].itemCount--;
				}
				break;
			}
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
