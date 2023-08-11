using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "CheckJieSha", "检查是否能截杀", 0)]
[AddComponentMenu("")]
public class CheckJieSha : Command
{
	[Tooltip("npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	[Tooltip("是否截杀")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable JieSha;

	[Tooltip("威胁物品")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable WeiXieItem;

	public override void OnEnter()
	{
		int nowMapIndex = Tools.instance.getPlayer().NowMapIndex;
		JieSha.Value = false;
		if (NpcJieSuanManager.inst.isCanJieSuan)
		{
			List<int> jieShaNpcList = NpcJieSuanManager.inst.GetJieShaNpcList(nowMapIndex);
			List<ITEM_INFO> values = Tools.instance.getPlayer().itemList.values;
			if (jieShaNpcList.Count > 0)
			{
				foreach (int item in jieShaNpcList)
				{
					if (JieSha.Value)
					{
						break;
					}
					int i = jsonData.instance.AvatarJsonData[item.ToString()]["Level"].I;
					int i2 = jsonData.instance.NpcLevelShouYiDate[i.ToString()]["jieshapanduan"].I;
					foreach (ITEM_INFO item2 in values)
					{
						if (item2 != null && item2.itemId > 0 && jsonData.instance.ItemJsonData[item2.itemId.ToString()]["price"].I >= i2)
						{
							JieSha.Value = true;
							npcId.Value = item;
							UINPCJiaoHu.Inst.JiaoHuItemID = item2.itemId;
							WeiXieItem.Value = item2.itemId;
							break;
						}
					}
				}
			}
			else
			{
				JieSha.Value = false;
			}
		}
		else
		{
			JieSha.Value = false;
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
