using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x0200023B RID: 571
[CommandInfo("YSNPCJiaoHu", "获取其他交互文本", "获取当前NPC的其他交互文本，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetOtherJiaoHu : Command
{
	// Token: 0x0600161C RID: 5660 RVA: 0x00095B98 File Offset: 0x00093D98
	private static void Init()
	{
		if (!CmdGetOtherJiaoHu.isInited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkQiTaJiaoHuData.list)
			{
				int i = jsonobject["id"].I;
				CmdGetOtherJiaoHu._JingJieDict.Add(i, jsonobject["JingJie"].I);
				CmdGetOtherJiaoHu._XingGeDict.Add(i, jsonobject["XingGe"].I);
				CmdGetOtherJiaoHu._TalkDict.Add(i, new Dictionary<string, string>());
				foreach (string text in jsonobject.keys)
				{
					if (text.StartsWith("Talk"))
					{
						CmdGetOtherJiaoHu._TalkDict[i].Add(text, jsonobject[text].Str);
					}
				}
			}
			CmdGetOtherJiaoHu.isInited = true;
		}
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x00095CC4 File Offset: 0x00093EC4
	public override void OnEnter()
	{
		CmdGetOtherJiaoHu.Init();
		Flowchart flowchart = this.GetFlowchart();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int>();
		int levelType = player.getLevelType();
		foreach (KeyValuePair<int, int> keyValuePair in CmdGetOtherJiaoHu._JingJieDict)
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
			if (CmdGetOtherJiaoHu._XingGeDict[num] == nowJiaoHuNPC.XingGe)
			{
				list2.Add(num);
			}
		}
		if (list2.Count > 0)
		{
			if (CmdGetOtherJiaoHu._TalkDict[list2[0]].ContainsKey(this.textKey))
			{
				flowchart.SetStringVariable("TmpTalkString", CmdGetOtherJiaoHu._TalkDict[list2[0]][this.textKey].ReplaceTalkWord(nowJiaoHuNPC));
			}
			else
			{
				flowchart.SetStringVariable("TmpTalkString", string.Format("读取失败，没有获取到文本(key{0}不存在)", list2[0]));
			}
		}
		else
		{
			flowchart.SetStringVariable("TmpTalkString", "读取失败，没有获取到文本3");
		}
		this.Continue();
	}

	// Token: 0x04001070 RID: 4208
	private static bool isInited;

	// Token: 0x04001071 RID: 4209
	private static Dictionary<int, int> _JingJieDict = new Dictionary<int, int>();

	// Token: 0x04001072 RID: 4210
	private static Dictionary<int, int> _XingGeDict = new Dictionary<int, int>();

	// Token: 0x04001073 RID: 4211
	private static Dictionary<int, Dictionary<string, string>> _TalkDict = new Dictionary<int, Dictionary<string, string>>();

	// Token: 0x04001074 RID: 4212
	[Tooltip("以Talk开头的key，获取对应的文本")]
	[SerializeField]
	protected string textKey;
}
