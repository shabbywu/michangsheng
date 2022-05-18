using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x02000357 RID: 855
[CommandInfo("YSNPCJiaoHu", "获取其他交互文本", "获取当前NPC的其他交互文本，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetOtherJiaoHu : Command
{
	// Token: 0x060018D4 RID: 6356 RVA: 0x000DE1B0 File Offset: 0x000DC3B0
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

	// Token: 0x060018D5 RID: 6357 RVA: 0x000DE2DC File Offset: 0x000DC4DC
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

	// Token: 0x040013C8 RID: 5064
	private static bool isInited;

	// Token: 0x040013C9 RID: 5065
	private static Dictionary<int, int> _JingJieDict = new Dictionary<int, int>();

	// Token: 0x040013CA RID: 5066
	private static Dictionary<int, int> _XingGeDict = new Dictionary<int, int>();

	// Token: 0x040013CB RID: 5067
	private static Dictionary<int, Dictionary<string, string>> _TalkDict = new Dictionary<int, Dictionary<string, string>>();

	// Token: 0x040013CC RID: 5068
	[Tooltip("以Talk开头的key，获取对应的文本")]
	[SerializeField]
	protected string textKey;
}
