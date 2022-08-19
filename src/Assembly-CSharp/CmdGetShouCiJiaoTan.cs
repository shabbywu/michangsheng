using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x02000242 RID: 578
[CommandInfo("YSNPCJiaoHu", "获取首次聊天文本", "获取当前NPC的初次聊天内容，赋值到TmpTalkString，好感度变化赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetShouCiJiaoTan : Command
{
	// Token: 0x0600162C RID: 5676 RVA: 0x00096034 File Offset: 0x00094234
	private static void Init()
	{
		if (!CmdGetShouCiJiaoTan.isInited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkShouCiJiaoTanData.list)
			{
				int i = jsonobject["id"].I;
				CmdGetShouCiJiaoTan._JingJieDict.Add(i, jsonobject["JingJie"].I);
				CmdGetShouCiJiaoTan._ShengWangDict.Add(i, jsonobject["ShengWang"].I);
				CmdGetShouCiJiaoTan._XingGeDict.Add(i, jsonobject["XingGe"].I);
				CmdGetShouCiJiaoTan._FavorDict.Add(i, jsonobject["HaoGanDu"].I);
				CmdGetShouCiJiaoTan._FirstTalkDict.Add(i, jsonobject["FirstTalk"].Str);
			}
			CmdGetShouCiJiaoTan.isInited = true;
		}
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x00096138 File Offset: 0x00094338
	public override void OnEnter()
	{
		CmdGetShouCiJiaoTan.Init();
		Flowchart flowchart = this.GetFlowchart();
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int>();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		int levelType = player.getLevelType();
		foreach (KeyValuePair<int, int> keyValuePair in CmdGetShouCiJiaoTan._JingJieDict)
		{
			if (keyValuePair.Value == 1 && nowJiaoHuNPC.BigLevel < levelType)
			{
				list.Add(keyValuePair.Key);
			}
			if (keyValuePair.Value == 2 && nowJiaoHuNPC.BigLevel == levelType)
			{
				list.Add(keyValuePair.Key);
			}
			if (keyValuePair.Value == 3 && nowJiaoHuNPC.BigLevel > levelType)
			{
				list.Add(keyValuePair.Key);
			}
		}
		int num;
		if (nowJiaoHuNPC.IsNingZhouNPC)
		{
			num = PlayerEx.GetNingZhouShengWangLevel();
		}
		else
		{
			num = PlayerEx.GetSeaShengWangLevel();
		}
		List<int> list2 = new List<int>();
		foreach (int num2 in list)
		{
			if (CmdGetShouCiJiaoTan._ShengWangDict[num2] == num)
			{
				list2.Add(num2);
			}
		}
		List<int> list3 = new List<int>();
		foreach (int num3 in list2)
		{
			if (CmdGetShouCiJiaoTan._XingGeDict[num3] == nowJiaoHuNPC.XingGe)
			{
				list3.Add(num3);
			}
		}
		if (list3.Count > 0)
		{
			flowchart.SetStringVariable("TmpTalkString", CmdGetShouCiJiaoTan._FirstTalkDict[list3[0]].ReplaceTalkWord(nowJiaoHuNPC));
			flowchart.SetIntegerVariable("TmpValue", CmdGetShouCiJiaoTan._FavorDict[list3[0]]);
			int num4 = CmdGetShouCiJiaoTan._FavorDict[list3[0]];
		}
		else
		{
			flowchart.SetStringVariable("TmpTalkString", "读取失败，没有获取到文本1");
			flowchart.SetIntegerVariable("TmpValue", 0);
		}
		this.Continue();
	}

	// Token: 0x04001079 RID: 4217
	private static bool isInited;

	// Token: 0x0400107A RID: 4218
	private static Dictionary<int, int> _JingJieDict = new Dictionary<int, int>();

	// Token: 0x0400107B RID: 4219
	private static Dictionary<int, int> _ShengWangDict = new Dictionary<int, int>();

	// Token: 0x0400107C RID: 4220
	private static Dictionary<int, int> _XingGeDict = new Dictionary<int, int>();

	// Token: 0x0400107D RID: 4221
	private static Dictionary<int, int> _FavorDict = new Dictionary<int, int>();

	// Token: 0x0400107E RID: 4222
	private static Dictionary<int, string> _FirstTalkDict = new Dictionary<int, string>();
}
