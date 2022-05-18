using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000250 RID: 592
[CommandInfo("YSTools", "召唤掌门到玩家位置", "召唤掌门到玩家位置", 0)]
[AddComponentMenu("")]
public class CallZhangMenToThis : Command
{
	// Token: 0x0600120A RID: 4618 RVA: 0x000ADCB4 File Offset: 0x000ABEB4
	public override void OnEnter()
	{
		this.NpcId.Value = -1;
		if (NpcJieSuanManager.inst.isCanJieSuan)
		{
			int zhangMenId = Tools.instance.getPlayer().GetZhangMenId(this.MenPaiId.Value);
			if (zhangMenId > 0)
			{
				string name = SceneManager.GetActiveScene().name;
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
						bigMapNPCDictionary.Add(nowMapIndex, new List<int>
						{
							zhangMenId
						});
					}
				}
				NpcJieSuanManager.inst.isUpDateNpcList = true;
				this.NpcId.Value = zhangMenId;
			}
		}
		else
		{
			Debug.LogError("召唤掌门到玩家位置指令在结算时被调用,此行为不安全");
		}
		this.Continue();
	}

	// Token: 0x04000E90 RID: 3728
	[SerializeField]
	[Tooltip("门派Id")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable MenPaiId;

	// Token: 0x04000E91 RID: 3729
	[SerializeField]
	[Tooltip("掌门Id")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NpcId;
}
