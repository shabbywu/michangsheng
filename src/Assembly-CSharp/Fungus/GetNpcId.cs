using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetNpcId", "根据流派和境界获取NpcId", 0)]
[AddComponentMenu("")]
public class GetNpcId : Command
{
	[Tooltip("Npc流派")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NpcLiuPai;

	[Tooltip("Npc境界")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NpcLevel;

	[Tooltip("NpcId存放位置")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NpcId;

	[SerializeField]
	protected bool IsNoFriend;

	[SerializeField]
	protected bool IsNoImportantNpcId;

	[SerializeField]
	protected SexType SetType;

	public override void OnEnter()
	{
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		List<int> list = new List<int>();
		int value = NpcLiuPai.Value;
		int value2 = NpcLevel.Value;
		foreach (string key in avatarJsonData.keys)
		{
			if (int.Parse(key) < 20000 || avatarJsonData[key]["LiuPai"].I != value || value2 != avatarJsonData[key]["Level"].I)
			{
				continue;
			}
			if (SetType == SexType.随机)
			{
				if ((!IsNoFriend || !Tools.instance.getPlayer().emailDateMag.IsFriend(avatarJsonData[key]["id"].I)) && (!IsNoImportantNpcId || !NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsValue(avatarJsonData[key]["id"].I)))
				{
					list.Add(avatarJsonData[key]["id"].I);
				}
			}
			else if (SetType == (SexType)avatarJsonData[key]["SexType"].I && (!IsNoFriend || !Tools.instance.getPlayer().emailDateMag.IsFriend(avatarJsonData[key]["id"].I)) && (!IsNoImportantNpcId || !NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsValue(avatarJsonData[key]["id"].I)))
			{
				list.Add(avatarJsonData[key]["id"].I);
			}
		}
		if (list.Count < 1)
		{
			NpcId.Value = FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(value, value2, (int)SetType);
		}
		else
		{
			NpcId.Value = list[NpcJieSuanManager.inst.getRandomInt(0, list.Count - 1)];
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
