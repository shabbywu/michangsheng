using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddItemToNpc", "给npc背包添加物品", 0)]
[AddComponentMenu("")]
public class AddItemToNpc : Command
{
	[Tooltip("NpcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	[Tooltip("物品Id")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable item;

	[Tooltip("物品数量")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable itemCount;

	[Tooltip("是否是重要NPC")]
	[SerializeField]
	public bool isImprotant;

	public override void OnEnter()
	{
		int num = npcId.Value;
		if (isImprotant)
		{
			num = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId.Value];
		}
		NpcJieSuanManager.inst.AddItemToNpcBackpack(num, item.Value, itemCount.Value);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
