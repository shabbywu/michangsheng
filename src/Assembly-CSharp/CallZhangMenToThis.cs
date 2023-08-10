using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

[CommandInfo("YSTools", "召唤掌门到玩家位置", "召唤掌门到玩家位置", 0)]
[AddComponentMenu("")]
public class CallZhangMenToThis : Command
{
	[SerializeField]
	[Tooltip("门派Id")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable MenPaiId;

	[SerializeField]
	[Tooltip("掌门Id")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NpcId;

	public override void OnEnter()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		NpcId.Value = -1;
		if (NpcJieSuanManager.inst.isCanJieSuan)
		{
			int zhangMenId = Tools.instance.getPlayer().GetZhangMenId(MenPaiId.Value);
			if (zhangMenId > 0)
			{
				Scene activeScene = SceneManager.GetActiveScene();
				string name = ((Scene)(ref activeScene)).name;
				if (name.StartsWith("S"))
				{
					if (!NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[name].Contains(zhangMenId))
					{
						NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[name].Add(zhangMenId);
					}
				}
				else if (name.Contains("AllMaps"))
				{
					int nowMapIndex = PlayerEx.Player.NowMapIndex;
					Dictionary<int, List<int>> bigMapNPCDictionary = NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary;
					if (bigMapNPCDictionary.ContainsKey(nowMapIndex))
					{
						if (!bigMapNPCDictionary[nowMapIndex].Contains(zhangMenId))
						{
							bigMapNPCDictionary[nowMapIndex].Add(zhangMenId);
						}
					}
					else
					{
						bigMapNPCDictionary.Add(nowMapIndex, new List<int> { zhangMenId });
					}
				}
				NpcJieSuanManager.inst.isUpDateNpcList = true;
				NpcId.Value = zhangMenId;
			}
		}
		else
		{
			Debug.LogError((object)"召唤掌门到玩家位置指令在结算时被调用,此行为不安全");
		}
		Continue();
	}
}
