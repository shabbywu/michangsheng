using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x02000234 RID: 564
[CommandInfo("YSNPCJiaoHu", "获取后续聊天文本", "获取当前NPC的后续聊天内容，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetHouXuJiaoTan : Command
{
	// Token: 0x0600160A RID: 5642 RVA: 0x0009536C File Offset: 0x0009356C
	private static void Init()
	{
		if (!CmdGetHouXuJiaoTan.isInited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkHouXuJiaoTanData.list)
			{
				int i = jsonobject["id"].I;
				CmdGetHouXuJiaoTan._JingJieDict.Add(i, jsonobject["JingJie"].I);
				CmdGetHouXuJiaoTan._XingGeDict.Add(i, jsonobject["XingGe"].I);
				CmdGetHouXuJiaoTan._FavorDict.Add(i, jsonobject["HaoGanDu"].I);
				CmdGetHouXuJiaoTan._TalkDict.Add(i, jsonobject["OtherTalk"].Str);
			}
			CmdGetHouXuJiaoTan.isInited = true;
		}
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x00095454 File Offset: 0x00093654
	public override void OnEnter()
	{
		CmdGetHouXuJiaoTan.Init();
		Flowchart flowchart = this.GetFlowchart();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int>();
		int levelType = player.getLevelType();
		foreach (KeyValuePair<int, int> keyValuePair in CmdGetHouXuJiaoTan._JingJieDict)
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
		List<int> list2 = new List<int>();
		foreach (int num in list)
		{
			if (CmdGetHouXuJiaoTan._XingGeDict[num] == nowJiaoHuNPC.XingGe)
			{
				list2.Add(num);
			}
		}
		List<int> list3 = new List<int>();
		foreach (int num2 in list2)
		{
			if (CmdGetHouXuJiaoTan._FavorDict[num2] == nowJiaoHuNPC.FavorLevel)
			{
				list3.Add(num2);
			}
		}
		if (list3.Count > 0)
		{
			flowchart.SetStringVariable("TmpTalkString", CmdGetHouXuJiaoTan._TalkDict[list3[0]].ReplaceTalkWord(nowJiaoHuNPC));
		}
		else
		{
			flowchart.SetStringVariable("TmpTalkString", "读取失败，没有获取到文本2");
		}
		this.Continue();
	}

	// Token: 0x0400105F RID: 4191
	private static bool isInited;

	// Token: 0x04001060 RID: 4192
	private static Dictionary<int, int> _JingJieDict = new Dictionary<int, int>();

	// Token: 0x04001061 RID: 4193
	private static Dictionary<int, int> _XingGeDict = new Dictionary<int, int>();

	// Token: 0x04001062 RID: 4194
	private static Dictionary<int, int> _FavorDict = new Dictionary<int, int>();

	// Token: 0x04001063 RID: 4195
	private static Dictionary<int, string> _TalkDict = new Dictionary<int, string>();
}
