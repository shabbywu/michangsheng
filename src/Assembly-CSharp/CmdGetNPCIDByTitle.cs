using System;
using Fungus;
using UnityEngine;

// Token: 0x02000354 RID: 852
[CommandInfo("YSNPCJiaoHu", "通过称号获取NPC", "通过称号获取NPC，赋值到TmpValue，如果找不到会赋值0", 0)]
[AddComponentMenu("")]
public class CmdGetNPCIDByTitle : Command
{
	// Token: 0x060018CE RID: 6350 RVA: 0x000DDED8 File Offset: 0x000DC0D8
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int value = 0;
		foreach (JSONObject jsonobject in jsonData.instance.AvatarJsonData.list)
		{
			int i = jsonobject["id"].I;
			if (i >= 20000 && jsonobject["Title"].Str == this.Title)
			{
				value = i;
				break;
			}
		}
		flowchart.SetIntegerVariable("TmpValue", value);
		this.Continue();
	}

	// Token: 0x040013C1 RID: 5057
	[SerializeField]
	protected string Title;
}
