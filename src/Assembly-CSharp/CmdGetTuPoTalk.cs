using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x0200035F RID: 863
[CommandInfo("YSNPCJiaoHu", "获取关于突破关联文本", "获取关于突破关联文本，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetTuPoTalk : Command
{
	// Token: 0x060018E8 RID: 6376 RVA: 0x000DE904 File Offset: 0x000DCB04
	private static void Init()
	{
		if (!CmdGetTuPoTalk.isInited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkGuanYuTuPoData.list)
			{
				int i = jsonobject["id"].I;
				CmdGetTuPoTalk._PlayerJingJieDict.Add(i, jsonobject["WanJiaJingJie"].I);
				CmdGetTuPoTalk._JingJieDict.Add(i, jsonobject["JingJie"].I);
				CmdGetTuPoTalk._XingGeDict.Add(i, jsonobject["XingGe"].I);
				CmdGetTuPoTalk._TalkDict.Add(i, jsonobject["TuPoTalk"].Str);
			}
			CmdGetTuPoTalk.isInited = true;
		}
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x000DE9EC File Offset: 0x000DCBEC
	public override void OnEnter()
	{
		CmdGetTuPoTalk.Init();
		Flowchart flowchart = this.GetFlowchart();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int>();
		int levelType = player.getLevelType();
		foreach (KeyValuePair<int, int> keyValuePair in CmdGetTuPoTalk._PlayerJingJieDict)
		{
			if (keyValuePair.Value == levelType)
			{
				list.Add(keyValuePair.Key);
			}
		}
		List<int> list2 = new List<int>();
		foreach (int num in list)
		{
			if (CmdGetTuPoTalk._JingJieDict[num] == 1 && nowJiaoHuNPC.BigLevel < levelType)
			{
				list2.Add(num);
			}
			if (CmdGetTuPoTalk._JingJieDict[num] == 2 && nowJiaoHuNPC.BigLevel == levelType)
			{
				list2.Add(num);
			}
			if (CmdGetTuPoTalk._JingJieDict[num] == 3 && nowJiaoHuNPC.BigLevel > levelType)
			{
				list2.Add(num);
			}
		}
		List<int> list3 = new List<int>();
		foreach (int num2 in list2)
		{
			if (CmdGetTuPoTalk._XingGeDict[num2] == nowJiaoHuNPC.XingGe)
			{
				list3.Add(num2);
			}
		}
		if (list3.Count > 0)
		{
			flowchart.SetStringVariable("TmpTalkString", CmdGetTuPoTalk._TalkDict[list3[0]].ReplaceTalkWord(nowJiaoHuNPC));
		}
		else
		{
			flowchart.SetStringVariable("TmpTalkString", "读取失败，没有获取到文本2");
		}
		this.Continue();
	}

	// Token: 0x040013D7 RID: 5079
	private static bool isInited;

	// Token: 0x040013D8 RID: 5080
	private static Dictionary<int, int> _JingJieDict = new Dictionary<int, int>();

	// Token: 0x040013D9 RID: 5081
	private static Dictionary<int, int> _XingGeDict = new Dictionary<int, int>();

	// Token: 0x040013DA RID: 5082
	private static Dictionary<int, int> _PlayerJingJieDict = new Dictionary<int, int>();

	// Token: 0x040013DB RID: 5083
	private static Dictionary<int, string> _TalkDict = new Dictionary<int, string>();
}
